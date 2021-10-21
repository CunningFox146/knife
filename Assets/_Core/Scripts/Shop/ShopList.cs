using System.Collections.Generic;
using UnityEngine;

namespace KnifeGame.Shop
{
    [CreateAssetMenu(fileName = "ShopList", menuName = "Scriptable Objects/ShopList")]
    public class ShopList : ScriptableObject
    {
        public int defaultItem = 0;
        public List<ShopItem> items;

        public ShopItem GetItem(int id) => items.Find(item => item.itemId == id);
        public ShopItem Default => GetItem(defaultItem);
    }
}