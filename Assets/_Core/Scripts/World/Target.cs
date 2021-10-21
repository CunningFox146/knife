using DG.Tweening;
using KnifeGame.Managers;
using KnifeGame.Managers.ModeManagers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using KnifeGame.Util;

namespace KnifeGame.World
{
    public class Target : MonoBehaviour
    {
        [SerializeField] private float _maxHeight = 10f;

        private Tween _tween;

        private void Start()
        {
            Move();
            StatsManager.Inst.OnKnifeHit += (knife, flip) => Move(1f);
            StatsManager.Inst.OnKnifeMiss += (knife) => Move(1f);
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
                this.DelayAction(delay, Callback);
            }
            else
            {
                Callback();
            }
        }
    }
}
