using KnifeGame.Knife;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KnifeGame.Managers.ModeManagers
{
    public class ClassicModeManager : ModeManager<ClassicModeManager>
    {
        [SerializeField] private GameObject _coinPrefab;

        protected override void OnKnifeFlipHandler(KnifeController knife, int score)
        {
            StatsManager.Inst.KnifeFlip(knife, score);
        }

        protected override void OnKnifeHitHandler(KnifeController knife, int flips)
        {
            var stats = StatsManager.Inst;

            stats.KnifeHit(knife, flips);
            stats.CurrentScore += (flips + 1) * knife.info.perFlip;
            stats.CoinsCount += flips;
            if (stats.CurrentScore > stats.BestScore)
            {
                stats.BestScore = stats.CurrentScore;
            }
                        
            SpawnCoins(knife, flips);
        }

        protected override void OnKnifeMissHandler(KnifeController knife)
        {
            StatsManager.Inst.CurrentScore = 0;
            StatsManager.Inst.KnifeMiss(knife);
        }

        private void SpawnCoins(KnifeController knife, int points)
        {
            if (points <= 0) return;

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
