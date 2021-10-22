using KnifeGame.Knife;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KnifeGame.Managers.ModeManagers
{
    public class ArcadeModeManager : ModeManager<ArcadeModeManager>
    {
        [SerializeField] private GameObject _coinPrefab;
        [SerializeField] private float _scorePerPoint = 2f;

        protected override void OnKnifeFlipHandler(KnifeController knife, int score)
        {
            StatsManager.Inst.KnifeFlip(knife, score);
        }

        protected override void OnKnifeHitHandler(KnifeController knife, int flips)
        {
            var stats = StatsManager.Inst;

            stats.KnifeHit(knife, flips);

            int score = Mathf.CeilToInt(knife.transform.position.y / _scorePerPoint);
            stats.CurrentScore = Mathf.Max(score, stats.CurrentScore);
            
            if (stats.CurrentScore > stats.BestScore)
            {
                stats.BestScore = stats.CurrentScore;
            }
        }

        protected override void OnKnifeMissHandler(KnifeController knife)
        {
            base.OnKnifeMissHandler(knife);

            StatsManager.Inst.CurrentScore = 0;
            StatsManager.Inst.KnifeMiss(knife);
        }
    }
}
