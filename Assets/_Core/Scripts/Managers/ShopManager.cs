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
        public event Action<ShopItem> OnItemChanged;

        [SerializeField] private ShopList _itemList;

        private ShopItem _selectedItem;
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
        
        protected override void Awake()
        {
            base.Awake();

            _ownedItems = new List<ShopItemType>();
            // TODO: Load owned items
            _ownedItems.Add(_itemList.defaultItem);

            // TODO: Load instead of picking default prefab
            _selectedItem = _itemList.Default;
        }


        public bool IsItemOwned(ShopItemType type) => _ownedItems.Contains(type);

        public bool BuyItem(ShopItem item)
        {
            if (item.itemPrice > StatsManager.Inst.CoinsCount) return false;

            StatsManager.Inst.CoinsCount -= item.itemPrice;
            _ownedItems.Add(item.type);

            return true;
        }
    }
}