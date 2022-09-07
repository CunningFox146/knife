using KnifeGame.Managers;
using KnifeGame.UI.Views;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KnifeGame.UI
{
    public class BackButton : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        void Start()
        {
            _button.onClick.AddListener(() =>
            {
                ViewManager.HideAllViews();
                ViewManager.ShowView<MainView>();

                Menu.Inst.SelectItem(null);
            });
        }
    }
}