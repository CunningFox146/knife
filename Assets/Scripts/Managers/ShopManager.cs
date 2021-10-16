using KnifeGame.Knife;
using KnifeGame.Shop;
using KnifeGame.Util;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KnifeGame.Managers
{
    public class ShopManager : Singleton<ShopManager>
    {
        public event Action<int> OnCoinsChanged;

        [SerializeField] private ShopList _itemList;
        [SerializeField] private GameObject _coinPrefab;

        private int _coinsCount;

        public int CoinsCount
        {
            get => _coinsCount;
            private set
            {
                if (_coinsCount != value)
                {
                    OnCoinsChanged?.Invoke(value);
                }
                _coinsCount = value;
            }
        }

        private void Start()
        {
            ScoreManager.Inst.OnKnifeHit += OnKnifeFlipHandler;
        }

        private void OnKnifeFlipHandler(KnifeController knife, int points)
        {
            if (points <= 0) return;

            CoinsCount += points;

            float startAngle = -Mathf.PI * 0.5f;
            for (int i = 0; i < points; i++)
            {
                float angle = i / (float)points * Mathf.PI + startAngle;
                Vector3 direction = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));
                var coin = Instantiate(_coinPrefab);

                coin.transform.position = knife.transform.position + Vector3.up + direction * 0.5f;
                coin.GetComponent<Rigidbody>().AddForce(direction * Random.Range(2f, 3f), ForceMode.Impulse);
            }
        }
    }
}