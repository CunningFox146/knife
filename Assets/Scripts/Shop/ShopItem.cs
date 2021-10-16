using System.Collections;
using UnityEngine;

namespace KnifeGame.Shop
{
    [CreateAssetMenu(fileName = "ShopItem", menuName = "Scriptable Objects/ShopItem")]
    public class ShopItem : ScriptableObject
    {
        public string itemName;
        public int itemPrice;
        public ShopItemType type;
        public GameObject prefab;
    }
}