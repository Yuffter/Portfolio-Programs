using System;
using Project.Main.GameSystems.Presentation;
using R3;
using UnityEngine;

namespace Project.Main.GameSystems
{
    public class TimerSystem : IDisposable
    {
        private int _remainingTime;
        private readonly ITimerPresentation _timerPresentation;
        private CompositeDisposable _disposables = new CompositeDisposable();
        private event Action _onTimerFinished;
        public event Action OnTimerFinished
        {
            add => _onTimerFinished += value;
            remove => _onTimerFinished -= value;
        }

        public TimerSystem(ITimerPresentation timerPresentation)
        {
            _timerPresentation = timerPresentation;
        }

        public void StartTimer(int initialTime)
        {
            _remainingTime = initialTime;

            Observable.Interval(TimeSpan.FromSeconds(1))
                .Subscribe(_ =>
                {
                    _remainingTime--;
                    _timerPresentation.UpdateTimerText(_remainingTime);

                    if (_remainingTime == 0)
                    {
                        Debug.Log("Timer finished");
                        _onTimerFinished?.Invoke();
                        _disposables.Dispose();
                    }
                })
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}