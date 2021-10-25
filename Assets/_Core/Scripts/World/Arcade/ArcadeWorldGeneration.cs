using KnifeGame.Knife;
using KnifeGame.Managers;
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

        private Queue<Transform> _items;

        private void Awake()
        {
            _items = new Queue<Transform>();
        }

        private void Start()
        {
            GeneratePart(0f);
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
            _items.Enqueue(part.transform);
        }
    }
}
