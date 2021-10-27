using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeGame.World
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private float _destroyTime = 1f;
        private SoundSystem.SoundsEmitter _sound;

        private void Start()
        {
            _sound = GetComponent<SoundSystem.SoundsEmitter>();
            _sound.Play("CoinDrop");

            if (_destroyTime <= 0f) return;

            transform.DOScale(Vector3.zero, 0.5f)
                .SetEase(Ease.InBack)
                .SetDelay(_destroyTime)
                .OnComplete(() =>Destroy(gameObject));
        }

        private void OnCollisionEnter(Collision collision)
        {
            _sound.Play("CoinDrop");
        }
    }
}