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
        public event Action<KnifeController, int> OnKnifeHit;
        public event Action<KnifeController> OnKnifeMiss;
        public event Action<KnifeController, int> OnKnifeFlip;

        protected virtual void Start()
        {
            GameManager.Inst.OnKnifeChanged += OnKnifeChangedHandler;

            Init();
        }

        protected abstract void Init();

        public virtual void OnKnifeHitHandler(KnifeController knife, int flips) => OnKnifeHit?.Invoke(knife, flips);
        public virtual void OnKnifeMissHandler(KnifeController knife) => OnKnifeMiss?.Invoke(knife);
        public virtual void OnKnifeFlipHandler(KnifeController knife, int flips) => OnKnifeFlip?.Invoke(knife, flips);


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


        public virtual void ResetScore()
        {
            CurrentScore = 0;
        }

    }
}
