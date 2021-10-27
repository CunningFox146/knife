using KnifeGame.Managers;
using KnifeGame.Shop;
using KnifeGame.Util;
using System;
using System.Collections;
using UnityEngine;

namespace KnifeGame.Knife
{
    public class KnifeController : MonoBehaviour
    {
        public event Action<KnifeController, int> OnKnifeFlip;
        public event Action<KnifeController, int> OnKnifeHit;
        public event Action<KnifeController> OnKnifeMiss;

        [SerializeField] private float _launchSpeed = 20f;
        [SerializeField] private bool _isPlayingHit = true;
        [SerializeField] private TrailRenderer _trail;
        [SerializeField] private bool _isCheating = false;

        public ShopItem info;

        private Rigidbody _rb;
        private Animator _animator;
        private int _hitHash;

        private int _flipsCount = 0;
        private bool _isLaunched = false;
        private float _launchStart = 999;

        private Coroutine _resetCoroutine;

        private bool CanLaunch => _resetCoroutine == null && !_isLaunched;
        private bool CanCollide => (Time.time - _launchStart) > 0.1f; // Ignore collision at the start of the launch

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _animator = GetComponent<Animator>();

            _hitHash = Animator.StringToHash("Hit");
        }

        private void Start()
        {
            SwipeManager.Inst.OnSwipe += OnSwipeHandler;
        }

        private void OnDestroy()
        {
            SwipeManager.Inst.OnSwipe -= OnSwipeHandler;
            StopAllCoroutines();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!_isLaunched || !CanCollide) return;

            if (_isCheating)
            {
                KnifeHit(collision.transform.rotation);
            }
            else
            {
                KnifeMiss();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isLaunched || _resetCoroutine != null || !CanCollide || !other.CompareTag("Platform")) return;

            KnifeHit(other.transform.rotation);
        }

        private void KnifeMiss()
        {
            _trail.emitting = false;

            _isLaunched = false;
            _flipsCount = 0;

            ResetKnife();

            OnKnifeMiss?.Invoke(this);
        }

        private void KnifeHit(Quaternion rot)
        {
            OnKnifeHit(this, _flipsCount);

            _launchStart = 999;
            _rb.isKinematic = true;
            _isLaunched = false;

            _trail.emitting = false;

            if (GameManager.Inst.Settings.resetOnHit)
            {
                ResetKnife();
            }

            _flipsCount = 0;

            if (_isPlayingHit)
            {
                _animator.SetTrigger(_hitHash);
            }
            SpawnHitEffect(rot);
        }

        private void SpawnHitEffect(Quaternion rot)
        {
            var fx = Instantiate(GameManager.Inst.Settings.hitEffect);
            fx.transform.position = transform.position;
            fx.transform.rotation = rot;
            Destroy(fx, 1.5f);
        }


        private void OnSwipeHandler(Vector3 direction)
        {
            Launch(direction);
        }

        private void ResetKnife()
        {
            _resetCoroutine = this.DelayAction(1f, () =>
            {
                transform.position = GameManager.Inst.StartPos;
                transform.rotation = new Quaternion();

                _rb.velocity = Vector3.zero;
                _rb.angularVelocity = Vector3.zero;

                _resetCoroutine = null;
            });
        }

        private IEnumerator RotationCoroutine()
        {
            float totalRot = 0f;
            var lastUp = transform.up;

            while (_isLaunched)
            {
                var rotDelta = Vector3.SignedAngle(transform.up, lastUp, transform.right);

                totalRot += Mathf.Abs(rotDelta);

                if (totalRot >= 360f)
                {
                    totalRot = 0f;
                    _flipsCount++;

                    OnKnifeFlip(this, info.perFlip);
                }

                lastUp = transform.up;

                yield return null;
            }
        }

        public void Launch(Vector3 direction)
        {
            if (!CanLaunch || direction.y <= 0f || direction.magnitude < 0.05f) return;

            Debug.Log($"Launch: {direction}");

            float rotDir = direction.x <= 0f ? 1f : -1f;
            float rotation = info.RotationSpeed * rotDir;
            Debug.Log($"Rotation: {rotation}");

            _launchStart = Time.time;
            _isLaunched = true;

            _trail.emitting = true;

            StartCoroutine(RotationCoroutine());

            _rb.isKinematic = false;
            _rb.AddForce(direction * _launchSpeed, ForceMode.Impulse);
            _rb.AddTorque(0f, 0f, rotation, ForceMode.Impulse);
        }
    }
}