using KnifeGame.Knife;
using KnifeGame.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeGame.World
{
    public class ArcadeWorldGeneration : MonoBehaviour
    {
        [SerializeField] private GameObject _coinPrefab;
        [SerializeField] private GameObject[] _branches;
        [Space]
        [SerializeField] private float _partSize = 10f;
        [SerializeField] private float _minX = -5f;
        [SerializeField] private float _maxX = 5f;

        private float _lastY = 0f;

        private List<Transform> _items;

        private void Awake()
        {
            _items = new List<Transform>();
        }

        private void Start()
        {
        }

        private void Update()
        {
            
        }
    }
}
