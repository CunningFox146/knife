using KnifeGame.Knife;
using KnifeGame.Util;
using System;
using UnityEngine;

namespace KnifeGame.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public event Action OnGameStart;

        [SerializeField] private Vector3 _startPos;

        private KnifeController _knife;

        public KnifeController Knife { get => _knife; private set => _knife = value; }

        private void Start()
        {
            Knife = Instantiate(ShopManager.Inst.SelectedItem.prefab).GetComponent<KnifeController>();
            Knife.transform.position = _startPos;

            SwipeManager.Inst.OnSwipeStart += OnSwipeStartHandler;
        }

        private void OnSwipeStartHandler(Vector2 obj)
        {
            OnGameStart?.Invoke();
        }
    }
}
