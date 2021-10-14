using DG.Tweening;
using KnifeGame.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class CoinDisplay : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0f;
        }

        private void Start()
        {
            GameManager.Inst.OnGameStart += OnGameStartHandler;
        }

        private void OnGameStartHandler()
        {
            GameManager.Inst.OnGameStart -= OnGameStartHandler;
            _canvasGroup.DOFade(1f, 0.5f).SetEase(Ease.OutCubic);
        }
    }
}
