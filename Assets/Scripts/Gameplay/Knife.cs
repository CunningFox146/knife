using KnifeGame.Managers;
using KnifeGame.Util;
using UnityEngine;

namespace KnifeGame.Gameplay
{
    public class Knife : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 20f;

        private Rigidbody _rb;
        private Vector3 _startPos;

        private bool _isLaunched = false;
        private float _launchStart = 999;
        private Vector3 _swipeStart;

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

            SwipeManager.Inst.OnSwipeStart += OnSwipeStartHandler;
            SwipeManager.Inst.OnSwipeEnd += OnSwipeEndHandler;
        }

        private void OnSwipeEndHandler(Vector2 endPos)
        {
            Vector2 force = Camera.main.ScreenToViewportPoint(endPos) - _swipeStart;
            Launch(force * 20f);
        }

        private void OnSwipeStartHandler(Vector2 startPos)
        {
            _swipeStart = Camera.main.ScreenToViewportPoint(startPos);
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
            Debug.Log($"Miss: {collision.gameObject.name}");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!_isLaunched || _resetCoroutine != null || !CanCollide || !other.CompareTag("Platform")) return;

            _launchStart = 999;
            _rb.isKinematic = true;
            _isLaunched = false;
            Debug.Log("Hit");
        }

        private void Update()
        {
            //if (Input.GetMouseButtonDown(0))
            //{
            //    Launch(Vector3.up * 10f);
            //}
        }

        private void ResetKnife()
        {
            transform.position = _startPos;
            transform.rotation = new Quaternion();

            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }

        public void Launch(Vector3 force)
        {
            if (!CanLaunch) return;

            Debug.Log($"Launch: {force}");

            _launchStart = Time.time;
            _isLaunched = true;

            _rb.isKinematic = false;
            _rb.AddForce(force, ForceMode.Impulse);
            _rb.AddTorque(new Vector3(0f, 0f, _rotationSpeed), ForceMode.Impulse);
        }
    }
}