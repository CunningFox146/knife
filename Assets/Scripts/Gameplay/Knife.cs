using DG.Tweening;
using UnityEngine;
using KnifeGame.Util;

namespace KnifeGame.Gameplay
{
    public class Knife : MonoBehaviour
    {
        [SerializeField] private float _maxAngle = 45f;

        private Rigidbody _rb;
        private BoxCollider _collider;

        private Vector3 _startPos;
        private bool _isLaunched = false;

        private Tween _rotationTween;
        private Coroutine _resetCoroutine;


        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _collider = GetComponent<BoxCollider>();
        }

        private void Start()
        {
            _startPos = transform.position;
        }

        private void OnCollisionEnter(Collision collision)
        {
            // If we're not falling
            if (!_isLaunched || _resetCoroutine != null || _rb.velocity.y > 0f) return;

            _rotationTween.Kill();
            _rotationTween = null;

            float angle = transform.eulerAngles.z;

            Debug.Log($"[Knife]: Hit detected! Angle: {angle} (max: {_maxAngle})");

            if (!collision.gameObject.CompareTag("Platform") || angle > _maxAngle || angle < -_maxAngle)
            {
                Debug.Log("Miss.");
                _resetCoroutine = this.DelayAction(0.5f, () =>
                {
                    _isLaunched = false;
                    _rb.isKinematic = true;
                    transform.position = _startPos;
                    transform.rotation = new Quaternion();

                    _resetCoroutine = null;
                });
                return;
            }
            
            _rb.velocity = Vector3.zero;
            _rb.isKinematic = true;
            
            transform.DOMoveY(transform.position.y - 0.5f, 0.1f)
                .OnComplete(()=> _isLaunched = false);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !_isLaunched)
            {
                Launch(Vector3.up * 10f);
            }
        }

        public void Launch(Vector3 force)
        {
            _isLaunched = true;

            if (_rotationTween == null)
            {
                _rotationTween = transform.DORotate(new Vector3(0f, 0f, 360f), 0.5f, RotateMode.FastBeyond360)
                    .SetLoops(-1)
                    .SetRelative(true);
            }

            _rb.isKinematic = false;
            _rb.AddForce(force, ForceMode.Impulse);
        }
    }
}