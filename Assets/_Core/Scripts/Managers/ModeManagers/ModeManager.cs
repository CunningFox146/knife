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
        protected virtual void Start()
        {
            GameManager.Inst.OnKnifeChanged += OnKnifeChangedHandler;
        }

        protected abstract void OnKnifeHitHandler(KnifeController knife, int flips);
        protected abstract void OnKnifeMissHandler(KnifeController knife);
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
            knife.OnKnifeFlip += OnKnifeFlipHandler;
            knife.OnKnifeMiss += OnKnifeMissHandler;
            knife.OnKnifeHit += OnKnifeHitHandler;
        }

        private void DisableEvents(KnifeController knife)
        {
            knife.OnKnifeFlip -= OnKnifeFlipHandler;
            knife.OnKnifeMiss -= OnKnifeMissHandler;
            knife.OnKnifeHit -= OnKnifeHitHandler;
        }
    }
}
