using System;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MinutesGame.Common.Timer 
{
    public class TimerPresenter : IDisposable
    {
        private readonly TimerModel _timerModel;
        private readonly ITimerView _timerView;
        private readonly Common.SO.GameSetting _gameSetting;
        public IObservable<Unit> OnTimerCompleted => _timerModel.OnTimerCompleted;
        private CompositeDisposable _disposables = new CompositeDisposable();

        [Inject]
        public TimerPresenter(TimerModel timerModel, ITimerView timerView, Common.SO.GameSetting gameSetting)
        {
            _timerModel = timerModel;
            _timerView = timerView;
            _gameSetting = gameSetting;

            _timerModel.ResetTimer();
            _timerView.SetTimerText(_gameSetting.MaxTimerDuration);
            _timerModel.SetMaxTime(_gameSetting.MaxTimerDuration);
        }

        /// <summary>
        /// タイマーを開始します
        /// </summary>
        public void StartTimer()
        {
            _disposables = new();
            _timerView.Initialize(_gameSetting.MaxTimerDuration);
            Observable.EveryUpdate()
            .Subscribe(_ =>
            {
                _timerModel.UpdateTimer(Time.deltaTime);
                _timerView.SetTimerText(_timerModel.RemainingTime);
            })
            .AddTo(_disposables);

            OnTimerCompleted
                .Subscribe(_ =>
                {
                    StopTimer();
                })
                .AddTo(_disposables);
        }

        /// <summary>
        /// タイマーを停止します
        /// </summary>
        public void StopTimer()
        {
            _disposables.Dispose();
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}