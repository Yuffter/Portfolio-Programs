using UnityEngine;

namespace Project.Main.GameSystems.Presentation
{
    /// <summary>
    /// タイマーのプレゼンテーションインターフェース
    /// </summary>
    public interface ITimerPresentation
    {
        /// <summary>
        /// タイマーのテキストを更新する
        /// </summary>
        /// <param name="time">残り時間</param>
        void UpdateTimerText(int time);
    }
}