﻿using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KnifeGame.World.Arcade
{
    public class ArcadeWorldPart : MonoBehaviour
    {
        [SerializeField] private GameObject _coinPrefab;
        [SerializeField] private GameObject[] _treePrefabs;
        [Space]
        [SerializeField] private Vector2 _size = Vector2.one * 10f;
        [SerializeField] private float _coinsDencity = 2f;
        [SerializeField] private float _coinsChance = 0.5f;

        private List<Transform> _items;

        private Sequence _appearSequence;

        public Vector2 Size => _size;
        private GameObject TreePrefab => _treePrefabs[Random.Range(0, _treePrefabs.Length)];

        private void Start()
        {
            Generate();
        }

        private void OnDestroy()
        {
            _appearSequence?.Kill();
        }

        public void Generate()
        {
            RemoveItems();

            // Create branches (one for each side)
            CreateBranch(new Vector3(_size.x * 0.5f - 1f, Random.Range(0f, _size.y * 0.5f), 0f)).Rotate(0f, 90f, 0f);
            CreateBranch(new Vector3(_size.x * -0.5f + 1f, Random.Range(0f, _size.y * -0.5f), 0f)).Rotate(0f, -90f, 0f);

            // Create coins (with some % for each 1 y point)
            float maxX = 2f;
            for (float y = 0f; y < _size.y; y+=_coinsDencity)
            {
                if (Random.Range(0f, 1f) > _coinsChance) continue;

                var coin = Instantiate(_coinPrefab, transform);


                coin.transform.localPosition = new Vector3(Random.Range(-maxX, maxX), y, 0);
            }

            var pos = transform.position;
            //transform.localScale = Vector3.one * 0.5f;
            transform.position = pos + Vector3.down;

            _appearSequence?.Kill();
            _appearSequence = DOTween.Sequence()
            //.Append(transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack))
            .Append(transform.DOMove(pos, 0.5f).SetEase(Ease.OutSine));
        }

        private Transform CreateBranch(Vector3 pos)
        {
            var branch = Instantiate(TreePrefab, transform);
            branch.transform.localPosition = pos;
            _items.Add(branch.transform);

            return branch.transform;
        }

        private void RemoveItems()
        {
            _items?.ForEach((item) => Destroy(item.gameObject));
            _items = new List<Transform>();
        }
    }
}
