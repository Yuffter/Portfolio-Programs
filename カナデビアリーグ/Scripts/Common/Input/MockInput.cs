using UniRx;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace MinutesGame.Common.Input
{
    public class MockInput : IInput, IDisposable
    {
        private Subject<Unit> _onLeftKeyPressed = new Subject<Unit>();
        public IObservable<Unit> OnLeftKeyPressed => _onLeftKeyPressed;

        private Subject<Unit> _onRightKeyPressed = new Subject<Unit>();
        public IObservable<Unit> OnRightKeyPressed => _onRightKeyPressed;

        private Subject<Unit> _onUpKeyPressed = new Subject<Unit>();
        public IObservable<Unit> OnUpKeyPressed => _onUpKeyPressed;

        private Subject<Unit> _onDownKeyPressed = new Subject<Unit>();
        public IObservable<Unit> OnDownKeyPressed => _onDownKeyPressed;

        public bool IsHoldingRight => true;

        private MockInputAction _inputActions;

        public void EnableInput()
        {
            // モック用のInputActionを使用します
            _inputActions = new MockInputAction();
            _inputActions.Enable();

            _inputActions.Arrow.Left.performed += ctx => _onLeftKeyPressed.OnNext(Unit.Default);
            _inputActions.Arrow.Right.performed += ctx => _onRightKeyPressed.OnNext(Unit.Default);
            _inputActions.Arrow.Up.performed += ctx => _onUpKeyPressed.OnNext(Unit.Default);
            _inputActions.Arrow.Down.performed += ctx => _onDownKeyPressed.OnNext(Unit.Default);
        }

        public void DisableInput()
        {
            _inputActions.Disable();
        }

        public void Dispose()
        {
            _onLeftKeyPressed?.Dispose();
            _onRightKeyPressed?.Dispose();
            _onUpKeyPressed?.Dispose();
            _onDownKeyPressed?.Dispose();
            _inputActions?.Dispose();
        }
    }
}