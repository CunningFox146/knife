using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace KnifeGame.World
{
    public class ArcadeWorldGeneration : MonoBehaviour
    {
        [SerializeField] private GameObject _platformPrefab;
        [SerializeField] private int _startCount = 3;
        [SerializeField] private int _maxCount = 5;
        [SerializeField] private float _spacing = 5f;
        [SerializeField] private float _maxHeight = 5f;
        [SerializeField] private float _heightDelta = 2f;

        private List<Platform> _platforms;

        private void Awake()
        {
            _platforms = new List<Platform>();
        }


    }
}
