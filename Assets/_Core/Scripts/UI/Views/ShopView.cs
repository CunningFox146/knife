using KnifeGame.Managers;
using KnifeGame.Scripts.UI.Shop;
using KnifeGame.Shop;
using KnifeGame.UI.Shop;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KnifeGame.UI.Views
{
    public class ShopView : AnimatedView
    {
        [SerializeField] private Text _itemPrice;
        [SerializeField] private Button _buyBtn;
        [SerializeField] private Button _playBtn;
        [SerializeField] private Text _itemName;
        [SerializeField] private KnifeDisplay _display;
        [Space]
        [SerializeField] private StatsContainer _weightContainer;
        [SerializeField] private StatsContainer _scoreContainer;
        [Space]
        [SerializeField] private GameObject _itemPrefab;
        [SerializeField] private RectTransform _itemContainer;

        private ShopItem _selectedItem;
        private List<ShopItemTile> _tiles;

        public ShopItem SelectedItem
        {
            get => _selectedItem;
            private set
            {
                _selectedItem = value;
                UpdateSelectedItem();
            }
        }

        private void Start()
        {
            var items = ShopManager.Inst.ItemList;

            _buyBtn.onClick.AddListener(BuySelectedItem);
            _playBtn.onClick.AddListener(PlayGame);

            _tiles = new List<ShopItemTile>();
            foreach (ShopItem item in items.items)
            {
                var tile = Instantiate(_itemPrefab, _itemContainer);
                var shopItem = tile.GetComponent<ShopItemTile>();
                shopItem.Init(item, () => SelectItem(item));
                _tiles.Add(shopItem);
            }
        }

        private void PlayGame()
        {
            ShopManager.Inst.SelectedItem = SelectedItem;

            ViewManager.HideAllViews();
            ViewManager.ShowView<MainView>();
            Menu.Inst.SelectItem(null);
        }

        private void BuySelectedItem()
        {
            if (ShopManager.Inst.BuyItem(SelectedItem))
            {
                //TODO Effects and Sound
                UpdateButtons(true);
                _tiles.ForEach((item) => item.UpdateIsOwned());
            }
        }

        private void SelectItem(ShopItem item)
        {
            SelectedItem = item;

            UpdateButtons(ShopManager.Inst.IsItemOwned(item.itemID));
        }

        private void UpdateButtons(bool isOwned)
        {
            _playBtn.gameObject.SetActive(isOwned);
            _buyBtn.gameObject.SetActive(!isOwned);
        }

        public override void Show()
        {
            base.Show();

            SelectItem(ShopManager.Inst.SelectedItem);
        }

        private void UpdateSelectedItem()
        {
            _display.SetModel(SelectedItem.shopModel);

            _weightContainer.SetCount(SelectedItem.weight);
            _scoreContainer.SetCount(SelectedItem.perFlip);

            _itemPrice.text = SelectedItem.itemPrice.ToString();
            _itemName.text = SelectedItem.itemName;
        }
    }
}
