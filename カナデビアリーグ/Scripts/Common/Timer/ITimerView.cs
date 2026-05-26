using System;
using UnityEngine;

namespace MinutesGame.Common.Timer
{
    public interface ITimerView
    {
        /// <summary>
        /// タイマーのテキストを設定します
        /// </summary>
        /// <param name="time">表示する時間</param>
        public void SetTimerText(float time);
        
        /// <summary>
        /// タイマーを初期化します
        /// </summary>
        /// <param name="maxTime">タイマーの最大時間</param>
        public void Initialize(float maxTime);
    }
}