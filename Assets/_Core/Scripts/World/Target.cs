using DG.Tweening;
using KnifeGame.Managers;
using KnifeGame.Util;
using UnityEngine;

namespace KnifeGame.World
{
    public class Target : MonoBehaviour
    {
        [SerializeField] private float _maxHeight = 10f;

        private Tween _tween;
        private Coroutine _moveCoroutine;

        private void Start()
        {
            Move();
            StatsManager.Inst.OnKnifeHit += OnKnifeHitHandler;
            StatsManager.Inst.OnKnifeMiss += OnKnifeMissHandler;
        }

        private void OnDestroy()
        {
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
            }

            _tween?.Kill();


            StatsManager.Inst.OnKnifeHit -= OnKnifeHitHandler;
            StatsManager.Inst.OnKnifeMiss -= OnKnifeMissHandler;
        }

        private void OnKnifeMissHandler(Knife.KnifeController obj)
        {
            Move(1f);
        }

        private void OnKnifeHitHandler(Knife.KnifeController arg1, int arg2)
        {
            Move(1f);
        }

        private void Move(float delay = 0f)
        {
            void Callback()
            {
                _tween?.Kill();
                transform.DOMoveY(Random.Range(0f, _maxHeight), 0.5f).SetEase(Ease.OutBack);
            }

            if (delay > 0f)
            {
                _moveCoroutine = this.DelayAction(delay, Callback);
            }
            else
            {
                Callback();
            }
        }
    }
}
