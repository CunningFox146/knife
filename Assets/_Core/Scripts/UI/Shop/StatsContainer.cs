using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KnifeGame.UI.Shop
{
    public class StatsContainer : MonoBehaviour
    {
        [SerializeField] private List<Image> _items;

        public void SetCount(int count)
        {
            for (int i = 0; i < count; i++)
            {
                _items[i].color = Color.white;
            }
            for (int i = count; i < _items.Count; i++)
            {
                _items[i].color = new Color(1f, 1f, 1f, 0.5f);
            }
        }
    }
}
