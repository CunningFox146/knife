using KnifeGame.Knife;
using KnifeGame.Util;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KnifeGame.Managers
{
    public class ShopManager : Singleton<ShopManager>
    {
        [SerializeField] private GameObject _coinPrefab;


        private void Start()
        {
            ScoreManager.Inst.OnKnifeHit += OnKnifeFlipHandler;
        }

        private void OnKnifeFlipHandler(KnifeController knife, int points)
        {
            int count = Mathf.Min(points - 1, 3);

            if (count <= 0) return;

            float startAngle = -Mathf.PI * 0.5f;
            for (int i = 0; i < count; i++)
            {
                float angle = i / (float)count * Mathf.PI + startAngle;
                Vector3 direction = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));
                var coin = Instantiate(_coinPrefab);

                coin.transform.position = knife.transform.position + Vector3.up;
                coin.GetComponent<Rigidbody>().AddForce(direction * Random.Range(2f, 3f), ForceMode.Impulse);
            }
        }
    }
}