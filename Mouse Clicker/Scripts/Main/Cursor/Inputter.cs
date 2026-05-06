using System;
using R3;
using UnityEngine;

namespace Project.Main.Cursor
{
    public class Inputter : IDisposable
    {
        private CompositeDisposable _disposables;

        private Subject<float> _verticalInput;
        private Subject<float> _horizontalInput;
        /// <summary>
        /// 垂直方向(W, S or 上下矢印キー)
        /// </summary>
        public Observable<float> VerticalInput => _verticalInput;
        /// <summary>
        /// 水平方向(A, D or 左右矢印キー)
        /// </summary>
        public Observable<float> HorizontalInput => _horizontalInput;
        private Subject<Unit> _pullOutInput;
        /// <summary>
        /// 引っ張り出し入力
        /// </summary>
        public Observable<Unit> PullOutInput => _pullOutInput;

        public Inputter()
        {
            // ReactivePropertyを初期化
            _verticalInput = new Subject<float>();
            _horizontalInput = new Subject<float>();
            _pullOutInput = new Subject<Unit>();
        }

        public void StartHandler()
        {
            _disposables = new CompositeDisposable();
            Observable.EveryUpdate()
            .Select(val => Input.GetAxisRaw("Vertical"))
            .DistinctUntilChanged()
            .Subscribe(val => _verticalInput.OnNext(val))
            .AddTo(_disposables);

            Observable.EveryUpdate()
            .Select(val => Input.GetAxisRaw("Horizontal"))
            .DistinctUntilChanged()
            .Subscribe(val => _horizontalInput.OnNext(val))
            .AddTo(_disposables);

            Observable.EveryUpdate()
            .Where(_ => Input.GetKeyDown(KeyCode.Z))
            .Subscribe(_ => _pullOutInput.OnNext(Unit.Default))
            .AddTo(_disposables);
        }

        public void Dispose()
        {
            // Clean up resources
            _disposables?.Dispose();
            _verticalInput?.Dispose();
            _horizontalInput?.Dispose();
        }
    }
}