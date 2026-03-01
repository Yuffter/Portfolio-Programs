using System;
using UnityEngine;
using R3;

namespace StageSelect.Button
{
    /// <summary>
    /// スタートボタンをまとめて管理するクラス
    /// </summary>
    public class StartButtonCollection : IDisposable
    {
        private StartButtonObserver[] _startButtons;
        private CompositeDisposable _disposables = new CompositeDisposable();
        public StartButtonCollection()
        {
            _startButtons = GameObject.FindObjectsByType<StartButtonObserver>(FindObjectsSortMode.None);
        }

        public void StartObserve(Action onClick)
        {
            _disposables = new CompositeDisposable();
            foreach (var button in _startButtons)
            {
                button.OnClick
                .Subscribe(_ => {
                    onClick();
                    Dispose();
                })
                .AddTo(_disposables);
            }
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
