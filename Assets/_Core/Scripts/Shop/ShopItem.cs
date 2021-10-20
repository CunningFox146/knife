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
        [Range(1, 3)]
        public int weight = 1;
        [Range(1, 3)]
        public int perFlip = 1;
        [Space]
        public GameObject shopModel;
        public Vector2 pos = new Vector2(-100f, 0f);
        public Vector2 scale = Vector2.one * 65f;
        public Vector3 rotation = Vector3.forward * 45f;
    }
}