using KnifeGame.Knife;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KnifeGame.Managers.ModeManagers
{
    public class TargetModeManager : ModeManager<TargetModeManager>
    {
        [SerializeField] private GameObject _coinPrefab;
        [Space]
        [SerializeField] private Transform _target;
        [SerializeField] private float _targetSize = 2.5f;
        [SerializeField] private float _maxScore = 6;

        protected override void OnKnifeFlipHandler(KnifeController knife, int score)
        {
            StatsManager.Inst.KnifeFlip(knife, score);
        }

        protected override void OnKnifeHitHandler(KnifeController knife, int flips)
        {
            var stats = StatsManager.Inst;

            stats.KnifeHit(knife, flips);


            int score = CalculateScore(knife);

            stats.CurrentScore += score;
            stats.CoinsCount += score;
            if (stats.CurrentScore > stats.BestScore)
            {
                stats.BestScore = stats.CurrentScore;
            }

            SpawnCoins(knife, score);
        }

        protected override void OnKnifeMissHandler(KnifeController knife)
        {
            base.OnKnifeMissHandler(knife);

            StatsManager.Inst.CurrentScore = 0;
            StatsManager.Inst.KnifeMiss(knife);
        }

        private int CalculateScore(KnifeController knife)
        {
            float center = _target.position.y + _targetSize * 0.5f;
            float distance = Mathf.Abs(center - knife.transform.position.y);
            float percent = 1f - distance / (_targetSize * 0.5f);

            return Mathf.Max(Mathf.CeilToInt(_maxScore * percent), 1) * knife.info.perFlip;
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

                coin.transform.position = knife.transform.position + knife.transform.up + direction * 0.5f;
                coin.GetComponent<Rigidbody>().AddForce(direction * Random.Range(2f, 3f), ForceMode.Impulse);
            }
        }
    }
}
