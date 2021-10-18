using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeGame.UI.Shop
{
    public class KnifeDisplay : MonoBehaviour
    {
        [SerializeField] private RectTransform _model;

        public void Start()
        {
            _model.DORotate(Vector3.up * 360f, 5f, RotateMode.FastBeyond360).SetLoops(-1);
        }
    }
}
