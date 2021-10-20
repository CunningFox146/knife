using KnifeGame.Managers;
using KnifeGame.Shop;
using KnifeGame.Util;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace KnifeGame.Scripts.UI.Shop
{
    public class ShopItemTile : MonoBehaviour
    {
        [SerializeField] private GameObject _disabledLayer;
        [SerializeField] private RectTransform _itemContainer;

        private Button _button;
        private ShopItem _shopItem;
        private bool _isOwned = false;

        public ShopItem ShopItem { get => _shopItem; private set => _shopItem = value; }
        public bool IsOwned
        {
            get => _isOwned;
            private set
            {
                _isOwned = value;
                _disabledLayer.SetActive(!_isOwned);
            }
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        public void Init(ShopItem item, Action onClick)
        {
            ShopItem = item;

            var model = Instantiate(item.shopModel, _itemContainer);
            model.transform.localScale = new Vector3(item.scale.x, item.scale.y, 1f);
            model.transform.localPosition = item.pos;
            model.transform.eulerAngles = item.rotation;
            model.transform.SetLayerInChildren(LayerMask.NameToLayer("UI"));

            UpdateIsOwned();

            _button.onClick.AddListener(()=> onClick.Invoke());
        }

        public void UpdateIsOwned()
        {
            IsOwned = ShopManager.Inst.IsItemOwned(ShopItem.itemID);
        }
    }
}
