using UniRx;
using UnityEngine;
using System;
using VContainer;

namespace MinutesGame.Common.Input
{
    public class MainInput : MonoBehaviour, IInput
    {
        private Subject<Unit> _onLeftKeyPressed = new Subject<Unit>();
        public IObservable<Unit> OnLeftKeyPressed => _onLeftKeyPressed;

        private Subject<Unit> _onRightKeyPressed = new Subject<Unit>();
        public IObservable<Unit> OnRightKeyPressed => _onRightKeyPressed;

        private Subject<Unit> _onUpKeyPressed = new Subject<Unit>();
        public IObservable<Unit> OnUpKeyPressed => _onUpKeyPressed;

        private Subject<Unit> _onDownKeyPressed = new Subject<Unit>();
        public IObservable<Unit> OnDownKeyPressed => _onDownKeyPressed;

        private CompositeDisposable _disposables = new CompositeDisposable();

        private Joycon _assignedJoycon;

        /// <summary>
        /// 右キーが押され続けているかどうか
        /// </summary>
        public bool IsHoldingRight
        {
            get
            {
                // 左Joyconの場合
                if (_assignedJoycon != null && _assignedJoycon.isLeft)
                {
                    return _assignedJoycon != null && _assignedJoycon.GetButton(Joycon.Button.DPAD_DOWN);
                }
                // 右Joyconの場合
                return _assignedJoycon != null && _assignedJoycon.GetButton(Joycon.Button.DPAD_UP);
            }
        }


        /// <summary>
        /// 入力を受け取るJoyconを割り当てます
        /// </summary>
        /// <param name="joycon"></param>
        public void AssignJoycon(Joycon joycon)
        {
            _assignedJoycon = joycon;
            if (_assignedJoycon == null)
            {
                Debug.LogError("MainInput: Joyconが割り当てられていません！");
            }
            else
            {
                Debug.Log("MainInput: Joyconが正常に割り当てられました。");
            }
        }

        public void EnableInput()
        {
            _disposables = new CompositeDisposable();
            Observable.EveryUpdate()
            .Subscribe(_ =>
            {
                if (_assignedJoycon.isLeft)
                {
                    DoLeftJoyconProcess();
                }
                else
                {
                    DoRightJoyconProcess();
                }
            }).AddTo(_disposables);
        }

        /// <summary>
        /// 左Joyconの入力受付
        /// </summary>
        private void DoLeftJoyconProcess()
        {
            if (_assignedJoycon.GetButtonDown(Joycon.Button.DPAD_UP))
            {
                _onLeftKeyPressed.OnNext(Unit.Default);
            }
            if (_assignedJoycon.GetButtonDown(Joycon.Button.DPAD_DOWN))
            {
                _onRightKeyPressed.OnNext(Unit.Default);
            }
            if (_assignedJoycon.GetButtonDown(Joycon.Button.DPAD_RIGHT))
            {
                _onUpKeyPressed.OnNext(Unit.Default);
            }
            if (_assignedJoycon.GetButtonDown(Joycon.Button.DPAD_LEFT))
            {
                _onDownKeyPressed.OnNext(Unit.Default);
            }
        }

        /// <summary>
        /// 右Joyconの入力受付
        /// </summary>
        private void DoRightJoyconProcess()
        {
            if (_assignedJoycon.GetButtonDown(Joycon.Button.DPAD_DOWN))
            {
                _onLeftKeyPressed.OnNext(Unit.Default);
            }
            if (_assignedJoycon.GetButtonDown(Joycon.Button.DPAD_UP))
            {
                _onRightKeyPressed.OnNext(Unit.Default);
            }
            if (_assignedJoycon.GetButtonDown(Joycon.Button.DPAD_LEFT))
            {
                _onUpKeyPressed.OnNext(Unit.Default);
            }
            if (_assignedJoycon.GetButtonDown(Joycon.Button.DPAD_RIGHT))
            {
                _onDownKeyPressed.OnNext(Unit.Default);
            }
        }
        public void DisableInput()
        {
            _disposables.Dispose();
        }
    }
}
