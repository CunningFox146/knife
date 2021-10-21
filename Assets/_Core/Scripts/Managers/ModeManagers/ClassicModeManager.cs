using KnifeGame.Knife;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KnifeGame.Managers.ModeManagers
{
    public class ClassicModeManager : ModeManager<ClassicModeManager>
    {
        [SerializeField] private GameObject _coinPrefab;

        protected override void Init()
        {
            OnKnifeHit += StatsManager.Inst.KnifeHit;
            OnKnifeFlip += OnKnifeFlipHandler;
            OnKnifeMiss += StatsManager.Inst.KnifeMiss;

            OnKnifeFlip += SpawnCoins;
            //ResetScore(); KnifeMiss
            // Knife hit
            /*
            
            */
        }

        private void OnKnifeFlipHandler(KnifeController knife, int flips)
        {
            var stats = StatsManager.Inst;

            stats.KnifeFlip(knife, flips);
            stats.CurrentScore += (flips + 1) * knife.info.perFlip;
            if (stats.CurrentScore > stats.BestScore)
            {
                stats.BestScore = stats.CurrentScore;
            }
        }

        private void SpawnCoins(KnifeController knife, int points)
        {
            if (points <= 0) return;

            StatsManager.Inst.CoinsCount += points;

            float startAngle = -Mathf.PI * 0.5f;
            for (int i = 0; i < points; i++)
            {
                float angle = i / (float)points * Mathf.PI * 2 + startAngle;
                Vector3 direction = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));
                var coin = Instantiate(_coinPrefab);

                coin.transform.position = knife.transform.position + Vector3.up + direction * 0.5f;
                coin.GetComponent<Rigidbody>().AddForce(direction * Random.Range(2f, 3f), ForceMode.Impulse);
            }
        }
    }
}
