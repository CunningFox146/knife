using KnifeGame.Knife;
using KnifeGame.Shop;
using KnifeGame.Util;
using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace KnifeGame.Managers
{
    public class ShopManager : Singleton<ShopManager>
    {
        public event Action<int> OnCoinsChanged;
        public event Action<ShopItem> OnItemChanged;

        [SerializeField] private ShopList _itemList;
        [SerializeField] private GameObject _coinPrefab;

        private ShopItem _selectedItem;
        private int _coinsCount;
        private List<ShopItemType> _ownedItems;

        public ShopList ItemList => _itemList;

        public ShopItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (_selectedItem != value)
                {
                    OnItemChanged?.Invoke(value);
                }
                _selectedItem = value;
            }
        }
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

        protected override void Awake()
        {
            base.Awake();

            _ownedItems = new List<ShopItemType>();
            // TODO: Load owned items
            _ownedItems.Add(_itemList.defaultItem);

            // TODO: Load instead of picking default prefab
            _selectedItem = _itemList.Default;
        }

        private void Start()
        {
            ScoreManager.Inst.OnKnifeHit += OnKnifeFlipHandler;
        }

        public bool IsItemOwned(ShopItemType type) => _ownedItems.Contains(type);

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