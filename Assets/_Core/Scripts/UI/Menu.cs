using UnityEngine;
using System.Collections.Generic;
using KnifeGame.Managers;
using KnifeGame.UI.Views;

namespace KnifeGame.UI
{
    public class Menu : MonoBehaviour
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
            ViewManager.HideAllViews();
            ViewManager.ShowView<ShopView>();
        }

        public void ShowModes()
        {
            ViewManager.HideAllViews();
            ViewManager.ShowView<ShopView>();
        }

        public void ShowOptions()
        {
            ViewManager.HideAllViews();
            ViewManager.ShowView<MainView>();
        }
    }
}