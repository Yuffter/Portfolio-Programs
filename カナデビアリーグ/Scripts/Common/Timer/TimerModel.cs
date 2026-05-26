using System;
using UniRx;
using UnityEngine;

namespace MinutesGame.Common.Timer
{
    public class TimerModel
    {
        private float elapsedTime;

        /// <summary>
        /// 経過時間を取得します
        /// </summary>
        public float ElapsedTime => elapsedTime;

        private float _maxTime;
        public float RemainingTime => Mathf.Max(0f, _maxTime - elapsedTime);

        private Subject<Unit> _onTimerCompleted = new Subject<Unit>();
        public IObservable<Unit> OnTimerCompleted => _onTimerCompleted;

        public void SetMaxTime(float maxTime)
        {
            _maxTime = maxTime;
        }

        /// <summary>
        /// タイマーを更新します
        /// </summary>
        /// <param name="deltaTime">差分時間(s)</param>
        public void UpdateTimer(float deltaTime)
        {
            elapsedTime += deltaTime;
            if (RemainingTime <= 0f)
            {
                _onTimerCompleted.OnNext(Unit.Default);
            }
        }

        /// <summary>
        /// タイマーをリセットします
        /// </summary>
        public void ResetTimer()
        {
            elapsedTime = 0f;
        }
    }
}