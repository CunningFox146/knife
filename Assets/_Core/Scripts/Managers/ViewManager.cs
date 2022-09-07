using KnifeGame.UI.Views;
using KnifeGame.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace KnifeGame.Managers
{
    public class ViewManager : Singleton<ViewManager>
    {
        public event Action<View> OnViewShown;

        [SerializeField] private List<View> _views;

        public static View GetView<T>() where T : View
        {
            var view = Inst._views.Where(v => v is T).First();
            Inst.OnViewShown?.Invoke(view);
            return view;
        }

        public static View ShowView<T>() where T : View
        {
            return ShowView(GetView<T>());
        }

        public static View ShowView(View view)
        {
            view.Show();
            return view;
        }

        public static View HideView<T>() where T : View
        {
            return HideView(GetView<T>());
        }

        public static View HideView(View view)
        {
            view.Hide();
            return view;
        }

        public static void HideAllViews()
        {
            Inst._views.ForEach((view) => HideView(view));
        }

        public static bool IsViewVisible<T>() where T : View
        {
            var view = GetView<T>();
            return view != null && view.GetIsActive();
        }
    }
}
