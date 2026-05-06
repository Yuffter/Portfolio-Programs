using R3;
using UnityEngine;

namespace Project.Main.GameSystems.Models
{
    public class ScoreModel
    {
        private ReactiveProperty<int> _score = new ReactiveProperty<int>(0);
        /// <summary>
        /// ゲーム内のスコア
        /// </summary>
        public ReadOnlyReactiveProperty<int> Score => _score.ToReadOnlyReactiveProperty();

        /// <summary>
        /// スコアを加算する
        /// </summary>
        /// <param name="value"></param>
        public void AddScore(int value)
        {
            _score.Value += value;
        }
    }
}