using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

namespace KnifeGame.World
{
    public class TargetTree : MonoBehaviour
    {
        [SerializeField] private List<Transform> _chains;

        private List<Sequence> _tweens;

        private void Awake()
        {
            _tweens = new List<Sequence>();
        }

        private void Start()
        {
            foreach (Transform chain in _chains)
            {
                float speed = 1f + Random.Range(0f, 1f);
                var squence = DOTween.Sequence()
                    .Append(chain.DORotate(Vector3.right * 2.5f, speed))
                    .Append(chain.DORotate(-Vector3.right * 2.5f, speed))
                    .SetLoops(-1)
                    .SetRelative(true)
                    .SetEase(Ease.InOutSine);

                _tweens.Add(squence);
            }
        }

        private void OnDestroy()
        {
            _tweens.ForEach((item) => item?.Kill());
        }
    }
}
