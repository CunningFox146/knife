using UnityEngine;
using System.Collections.Generic;


namespace KnifeGame.UI
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private List<MenuItem> _items;

        private MenuItem _selected;

        public void SelectItem(MenuItem item)
        {
            _selected?.DeselectItem();
            _selected = item;
            _selected.SelectItem();
        }
    }
}