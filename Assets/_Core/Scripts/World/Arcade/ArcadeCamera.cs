using DG.Tweening;
using KnifeGame.Knife;
using KnifeGame.Managers;
using KnifeGame.Util;
using UnityEngine;

namespace KnifeGame.World
{
    public class ArcadeCamera : MonoBehaviour
    {
        [SerializeField] private float _spacing = -3f;

        private Vector3 _startPos;
        private Tween _moveTween;
        private Coroutine _resetcoroutine;

        private void Start()
        {
            _startPos = transform.position;

            StatsManager.Inst.OnKnifeHit += OnKnifeHitHandler;
            StatsManager.Inst.OnKnifeMiss += OnKnifeMissHandler;
        }

        private void OnDestroy()
        {
            StatsManager.Inst.OnKnifeHit -= OnKnifeHitHandler;
            StatsManager.Inst.OnKnifeMiss -= OnKnifeMissHandler;

            if (_resetcoroutine != null)
            {
                StopCoroutine(_resetcoroutine);
            }
        }

        private void OnKnifeMissHandler(KnifeController knife)
        {
            if (_resetcoroutine != null)
            {
                StopCoroutine(_resetcoroutine);
            }
            _resetcoroutine = this.DelayAction(1f, () => Move(0f));
        }

        private void OnKnifeHitHandler(KnifeController knife, int flips)
        {
            Move(knife.transform.position.y + _spacing);
        }

        private void Move(float y)
        {
            if ((transform.position.y + y) < _startPos.y) return;
            _moveTween?.Kill();
            _moveTween = transform.DOMoveY(_startPos.y + y, 0.25f)
                .SetEase(Ease.InOutSine);
        }

    }
}
