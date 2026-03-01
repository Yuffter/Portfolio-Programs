using System.Collections.Generic;
using UnityEngine;

namespace Score
{
    public interface IFacade
    {
        /// <summary>
        /// 現在のターンでのスコアを取得する
        /// <returns></returns>
        /// </summary>
        float GetCurrentTurnScore();

        /// <summary>
        /// 現在のターンでのスコアを保存する
        /// </summary>
        void SaveCurrentTurnScore();

        /// <summary>
        /// 各ターンのスコアを取得する
        /// </summary>
        /// <returns></returns>
        List<float> GetEachTurnScores();

        void OnDisable();
    }
}