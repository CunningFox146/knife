using UnityEngine;
using System.Collections.Generic;
using KnifeGame.Managers;
using KnifeGame.UI.Views;
using KnifeGame.Util;

namespace KnifeGame.UI
{
    public class Menu : Singleton<Menu>
    {   
        [SerializeField] private List<MenuItem> _items;
        [Space]
        [SerializeField] private Color _selectedColor;
        [SerializeField] private Color _deselectedColor;

        private MenuItem _selected;

        public void SelectItem(MenuItem item)
        {
            _selected?.DeselectItem(_deselectedColor);

            if (item != null)
            {
                _selected = item;
                _selected.SelectItem(_selectedColor);
            }
        }

        public void ShowShop()
        {
            if (ViewManager.IsViewVisible<ShopView>()) return;

            ViewManager.HideAllViews();
            ViewManager.ShowView<ShopView>();
        }

        public void ShowModes()
        {
            if (ViewManager.IsViewVisible<GameModesView>()) return;

            ViewManager.HideAllViews();
            ViewManager.ShowView<GameModesView>();
        }

        public void ShowOptions()
        {
            if (ViewManager.IsViewVisible<OptionsView>()) return;

            ViewManager.HideAllViews();
            ViewManager.ShowView<OptionsView>();
        }
    }
}