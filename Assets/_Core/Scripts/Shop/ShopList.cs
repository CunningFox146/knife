using System.Collections.Generic;
using UnityEngine;

namespace KnifeGame.Shop
{
    [CreateAssetMenu(fileName = "ShopList", menuName = "Scriptable Objects/ShopList")]
    public class ShopList : ScriptableObject
    {
        public ShopItemType defaultItem = ShopItemType.Default;
        public List<ShopItem> items;

        public ShopItem GetItem(ShopItemType type) => items.Find(item => item.type == type);
        public ShopItem Default => GetItem(defaultItem);
    }
}