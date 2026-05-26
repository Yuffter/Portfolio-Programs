using UniRx;
using UnityEngine;

namespace MinutesGame.Common.Score
{
    public class ScoreModel
    {
        private ReactiveProperty<int> _score;
        public IReadOnlyReactiveProperty<int> Score => _score;

        public ScoreModel()
        {
            _score = new ReactiveProperty<int>(0);
        }

        /// <summary>
        /// スコアを加算します
        /// </summary>
        /// <param name="amount">スコア量</param>
        public void AddScore(int amount)
        {
            _score.Value += amount;
        }
    }
}