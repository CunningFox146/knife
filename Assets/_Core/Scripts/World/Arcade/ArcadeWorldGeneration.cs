using KnifeGame.Knife;
using KnifeGame.Managers;
using KnifeGame.Util;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeGame.World
{
    public class ArcadeWorldGeneration : MonoBehaviour
    {
        [SerializeField] private GameObject _partPrefab;
        [Space]
        [SerializeField] private Vector3 _startPos = Vector3.up * 5f;
        [SerializeField] private float _partStep;

        private float _lastY = 0f;

        private List<Transform> _items;

        private void Awake()
        {
            _items = new List<Transform>();
        }

        private void Start()
        {
            GeneratePart(0f);

            StatsManager.Inst.OnKnifeMiss += OnKnifeMissHandler;
        }

        private void OnDestroy()
        {
            StatsManager.Inst.OnKnifeMiss -= OnKnifeMissHandler;
        }

        private void OnKnifeMissHandler(KnifeController obj)
        {
            this.DelayAction(1f, () =>
            {
                _items.ForEach((item) => Destroy(item.gameObject));
                _items = new List<Transform>();

                _lastY = 0f;
                GeneratePart(0f);
            });
        }

        private void Update()
        {
            var knife = GameManager.Inst.Knife;
            if (knife == null || knife.transform.position.y < _lastY) return;

            while (knife.transform.position.y > _lastY)
            {
                _lastY += _partStep;
            }

            GeneratePart(_lastY);
        }

        private void GeneratePart(float y)
        {
            var part = Instantiate(_partPrefab);
            part.transform.position = _startPos + Vector3.up * y;
            _items.Add(part.transform);
        }
    }
}
