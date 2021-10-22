using KnifeGame.Shop;
using KnifeGame.Util;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeGame.Managers
{
    public class ShopManager : Singleton<ShopManager>
    {
        public event Action<ShopItem> OnItemChanged;

        [SerializeField] private ShopList _itemList;

        private ShopItem _selectedItem;
        private List<int> _ownedItems;

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

        private void Start()
        {
            _ownedItems = SaveManager.CurrentSave.ownedItems;
            if (!_ownedItems.Contains(_itemList.defaultItem))
            {
                _ownedItems.Add(_itemList.defaultItem);
            }

            int itemId = _itemList.defaultItem;
            if (SaveManager.CurrentSave.selectedItem != 0)
            {
                itemId = SaveManager.CurrentSave.selectedItem;
            }
            _selectedItem = _itemList.GetItem(itemId);
        }


        public bool IsItemOwned(int id) => _ownedItems.Contains(id);

        public bool BuyItem(ShopItem item)
        {
            if (item.itemPrice > StatsManager.Inst.CoinsCount) return false;

            StatsManager.Inst.CoinsCount -= item.itemPrice;
            _ownedItems.Add(item.itemId);
            SyncAndSave();

            return true;
        }

        public void SyncAndSave()
        {
            //SaveManager.CurrentSave.ownedItems = _ownedItems;
            SaveManager.CurrentSave.selectedItem = SelectedItem.itemId;
            SaveManager.SaveCurrent();
        }
    }
}