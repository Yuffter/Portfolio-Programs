using UnityEngine;

namespace Project.Main.GameSystems.Presentation
{
    public interface IScorePresentation
    {
        /// <summary>
        /// スコアを更新する
        /// </summary>
        /// <param name="score">スコア</param>
        void UpdateText(int score);
    }
}