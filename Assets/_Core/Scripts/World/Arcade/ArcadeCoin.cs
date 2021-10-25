using DG.Tweening;
using KnifeGame.Managers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KnifeGame.World
{
    public class ArcadeCoin: MonoBehaviour
    {
        private Sequence _removeSequence;

        private void OnDestroy()
        {
           _removeSequence?.Kill();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (_removeSequence != null) return;

            StatsManager.Inst.CoinsCount++;

            _removeSequence = DOTween.Sequence()
                .Append(transform.DOScale(Vector3.zero, 1f).SetEase(Ease.InBack))
                .Join(transform.DORotate(Vector3.up * 360f, 1f, RotateMode.FastBeyond360).SetRelative(true))
                .OnComplete(() => Destroy(gameObject));
        }
    }
}
