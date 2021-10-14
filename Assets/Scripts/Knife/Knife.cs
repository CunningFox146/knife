using KnifeGame.Managers;
using KnifeGame.Util;
using System.Collections;
using UnityEngine;

namespace KnifeGame.Knife
{
    public class Knife : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 20f;
        [SerializeField] private float _launchSpeed = 20f;
        [SerializeField] private bool _isPlayingHit = true;

        private Rigidbody _rb;
        private Animator _animator;
        private int _hitHash;
        private Vector3 _startPos;

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
            _startPos = transform.position;

            SwipeManager.Inst.OnSwipe += OnSwipeHandler;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!_isLaunched || !CanCollide) return;

            _isLaunched = false;
            _flipsCount = 0;
            _resetCoroutine = this.DelayAction(1f, () =>
            {
                ResetKnife();
                _resetCoroutine = null;
            });

            ScoreManager.Inst.KnifeMiss();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isLaunched || _resetCoroutine != null || !CanCollide || !other.CompareTag("Platform")) return;

            _launchStart = 999;
            _rb.isKinematic = true;
            _isLaunched = false;

            ScoreManager.Inst.KnifeHit(_flipsCount);
            _flipsCount = 0;

            if (_isPlayingHit)
            {
                _animator.SetTrigger(_hitHash);
            }
        }

        private void OnSwipeHandler(Vector3 direction)
        {
            Launch(direction);
        }
        
        private void ResetKnife()
        {
            transform.position = _startPos;
            transform.rotation = new Quaternion();

            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }

        // When angle is > 180 it becomes negative.
        // So when it changes to neg or back, we count it as a half of a point
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
                    ScoreManager.Inst.KnifeFlip(gameObject);
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

            _launchStart = Time.time;
            _isLaunched = true;

            StartCoroutine(RotationCoroutine());

            _rb.isKinematic = false;
            _rb.AddForce(direction * _launchSpeed, ForceMode.Impulse);
            _rb.AddTorque(0f, 0f, _rotationSpeed * rotDir, ForceMode.Impulse);
        }
    }
}