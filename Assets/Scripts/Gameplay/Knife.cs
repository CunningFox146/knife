using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KnifeGame.Gameplay
{
    public class Knife : MonoBehaviour
    {
        private Rigidbody _rb;
        private BoxCollider _collider;

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _collider = GetComponent<BoxCollider>();
        }

        private void Start()
        {
            
        }
    }
}