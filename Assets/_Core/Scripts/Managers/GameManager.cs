using KnifeGame.Knife;
using KnifeGame.Managers.ModeManagers;
using KnifeGame.Shop;
using KnifeGame.Util;
using System;
using UnityEngine;

namespace KnifeGame.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public event Action OnGameStart;
        public event Action<KnifeController, KnifeController> OnKnifeChanged;

        [SerializeField] private ModeSettings _settings;
        [SerializeField] private Vector3 _startPos;

        private KnifeController _knife;

        public KnifeController Knife
        {
            get => _knife;
            private set
            {
                if (_knife != value)
                {
                    OnKnifeChanged?.Invoke(_knife, value);
                }
                _knife = value;
            }
        }

        public ModeSettings Settings { get => _settings; private set => _settings = value; }

        private void Start()
        {
            OnItemChangedHandler(ShopManager.Inst.SelectedItem);

            ShopManager.Inst.OnItemChanged += OnItemChangedHandler;
            SwipeManager.Inst.OnSwipeStart += OnSwipeStartHandler;
        }

        private void OnItemChangedHandler(ShopItem selectedItem)
        {
            StatsManager.Inst.ResetScore();

            if (Knife != null)
            {
                Destroy(Knife.gameObject);
            }
            Knife = Instantiate(selectedItem.prefab).GetComponent<KnifeController>();
            Knife.transform.position = _startPos;
            Knife.info = selectedItem;
        }

        private void OnSwipeStartHandler(Vector2 obj)
        {
            OnGameStart?.Invoke();
        }
    }
}
