using KnifeGame.Knife;
using KnifeGame.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KnifeGame.Managers.ModeManagers
{
    // .Inst in children will always be ModeManager
    // If you want to get ref to your class, use .Self
    public abstract class ModeManager<T> : Singleton<T> where T : Component
    {
        protected KnifeController _knife;

        protected virtual void Start()
        {
            GameManager.Inst.OnKnifeChanged += OnKnifeChangedHandler;
        }

        protected virtual void OnDestroy()
        {
            GameManager.Inst.OnKnifeChanged -= OnKnifeChangedHandler;

            if (_knife != null)
            {
                DisableEvents(_knife);
            }
        }

        protected abstract void OnKnifeHitHandler(KnifeController knife, int flips);
        protected virtual void OnKnifeMissHandler(KnifeController knife)
        {
            SaveManager.SaveCurrent();
        }

        protected abstract void OnKnifeFlipHandler(KnifeController knife, int flips);


        protected void OnKnifeChangedHandler(KnifeController oldKnife, KnifeController newKnife)
        {
            if (oldKnife != null)
            {
                DisableEvents(oldKnife);
            }
            EnableEvents(newKnife);
        }

        private void EnableEvents(KnifeController knife)
        {
            _knife = knife;

            knife.OnKnifeFlip += OnKnifeFlipHandler;
            knife.OnKnifeMiss += OnKnifeMissHandler;
            knife.OnKnifeHit += OnKnifeHitHandler;
        }

        private void DisableEvents(KnifeController knife)
        {
            _knife = knife;

            knife.OnKnifeFlip -= OnKnifeFlipHandler;
            knife.OnKnifeMiss -= OnKnifeMissHandler;
            knife.OnKnifeHit -= OnKnifeHitHandler;
        }
    }
}
