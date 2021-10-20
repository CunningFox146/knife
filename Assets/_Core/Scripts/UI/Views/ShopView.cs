using KnifeGame.Managers;
using KnifeGame.Scripts.UI.Shop;
using KnifeGame.Shop;
using KnifeGame.UI.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace KnifeGame.UI.Views
{
    public class ShopView : AnimatedView
    {
        [SerializeField] private Text _itemPrice;
        [SerializeField] private GameObject _buyBtn;
        [SerializeField] private GameObject _playBtn;
        [SerializeField] private Text _itemName;
        [SerializeField] private KnifeDisplay _display;
        [Space]
        [SerializeField] private StatsContainer _weightContainer;
        [SerializeField] private StatsContainer _scoreContainer;
        [Space]
        [SerializeField] private GameObject _itemPrefab;
        [SerializeField] private RectTransform _itemContainer;

        private ShopItem _selectedItem;

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

            foreach (ShopItem item in items.items)
            {
                var tile = Instantiate(_itemPrefab, _itemContainer);
                var shopItem = tile.GetComponent<ShopItemTile>();
                shopItem.Init(item, () => OnItemClicked(item));
            }
        }

        private void OnItemClicked(ShopItem item)
        {
            SelectedItem = item;

            bool isOwned = ShopManager.Inst.IsItemOwned(item.type);

            _playBtn.SetActive(isOwned);
            _buyBtn.SetActive(!isOwned);
        }

        public override void Show()
        {
            base.Show();

            SelectedItem = ShopManager.Inst.SelectedItem;
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
