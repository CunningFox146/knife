using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using KnifeGame.Managers;
using KnifeGame.Scripts.UI.Shop;
using KnifeGame.UI.Shop;
using UnityEngine;
using UnityEngine.UI;

namespace KnifeGame.UI.Views
{
    public class ShopView : AnimatedView
    {
        [SerializeField] private Text _itemName;
        [SerializeField] private Text _itemPrice;
        [SerializeField] private KnifeDisplay _display;
        [Space]
        [SerializeField] private StatsContainer _weightContainer;
        [SerializeField] private StatsContainer _scoreContainer;
        [Space]
        [SerializeField] private GameObject _itemPrefab;
        [SerializeField] private RectTransform _itemContainer;

        private void Start()
        {
            var items = ShopManager.Inst.ItemList;

            foreach (var item in items.items)
            {
                var tile = Instantiate(_itemPrefab, _itemContainer);
                tile.GetComponent<ShopItemTile>().Init(item);
            }
        }
    }
}
