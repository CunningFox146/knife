using KnifeGame.Managers;
using KnifeGame.Util;
using UnityEngine;

namespace KnifeGame.Knife
{
    public class Knife : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 20f;
        [SerializeField] private float _launchSpeed = 20f;

        private Rigidbody _rb;
        private Vector3 _startPos;

        private bool _isLaunched = false;
        private float _launchStart = 999;

        private Coroutine _resetCoroutine;

        private bool CanLaunch => _resetCoroutine == null && !_isLaunched;
        private bool CanCollide => (Time.time - _launchStart) > 0.1f; // Ignore collision at the start of the launch

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
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

            ScoreManager.Inst.KnifeHit();
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

        public void Launch(Vector3 direction)
        {
            if (!CanLaunch || direction.y <= 0f) return;

            Debug.Log($"Launch: {direction}");

            float rotDir = direction.x <= 0f ? 1f : -1f;

            _launchStart = Time.time;
            _isLaunched = true;

            _rb.isKinematic = false;
            _rb.AddForce(direction * _launchSpeed, ForceMode.Impulse);
            _rb.AddTorque(0f, 0f, _rotationSpeed * rotDir, ForceMode.Impulse);
        }
    }
}