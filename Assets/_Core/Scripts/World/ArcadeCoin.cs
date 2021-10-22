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
        private Rigidbody _rb;
        private Tween _removeTween;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void OnTriggerEnter(Collider collider)
        {
            StatsManager.Inst.CoinsCount++;

            _rb.constraints = RigidbodyConstraints.None;

            _removeTween = transform.DOScale(Vector3.zero, 1f)
                .SetEase(Ease.InBack)
                .OnComplete(()=>Destroy(gameObject));
        }
    }
}
