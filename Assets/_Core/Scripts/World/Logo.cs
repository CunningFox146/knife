using DG.Tweening;
using KnifeGame.Managers;
using UnityEngine;

namespace KnifeGame.World
{
    public class Logo : MonoBehaviour
    {
        private Tween _showTween;

        private void Start()
        {
            SwipeManager.Inst.OnSwipeStart += OnSwipeStartHandler;

            var pos = transform.position;
            transform.position = pos + Vector3.up * 4f;
            _showTween = transform.DOMove(pos, 2f).SetEase(Ease.OutElastic);
        }

        private void OnSwipeStartHandler(Vector2 obj)
        {
            SwipeManager.Inst.OnSwipeStart -= OnSwipeStartHandler;

            _showTween?.Kill();

            DOTween.Sequence()
            .Append(transform.DOMove(Vector3.down, 0.25f)
                .SetEase(Ease.Linear)
                .SetRelative(true))
            .Append(transform.DOMove(Vector3.up * 5f, 0.5f)
                .SetEase(Ease.InCubic)
                .SetRelative(true))
            .OnComplete(() => Destroy(gameObject));
        }
    }
}