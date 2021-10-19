using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnifeGame.Shop;
using KnifeGame.Managers;
using KnifeGame.Util;

namespace KnifeGame.Scripts.UI.Shop
{
    public class ShopItemTile : MonoBehaviour
    {
        [SerializeField] private GameObject _disabledLayer;
        [SerializeField] private RectTransform _itemContainer;

        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        public void Init(ShopItem item)
        {
            var model = Instantiate(item.shopModel, _itemContainer);
            model.transform.localScale = new Vector3(item.scale.x, item.scale.y, 1f);
            model.transform.localPosition = item.pos;
            model.transform.eulerAngles = item.rotation;
            model.transform.SetLayerInChildren(LayerMask.NameToLayer("UI"));

            if (ShopManager.Inst.IsItemOwned(item.type))
            {
                _disabledLayer.SetActive(false);
            }

        }
    }
}
