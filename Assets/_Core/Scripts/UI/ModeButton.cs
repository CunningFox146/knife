using KnifeGame.Managers;
using KnifeGame.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace KnifeGame.UI
{
    public class ModeButton : MonoBehaviour
    {
        [SerializeField] GameModes _mode;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(()=>
            {
                ScenesManager.Inst.SetGameMode(_mode);
                ViewManager.HideAllViews();
                ViewManager.ShowView<MainView>();
            });
        }
    }
}
