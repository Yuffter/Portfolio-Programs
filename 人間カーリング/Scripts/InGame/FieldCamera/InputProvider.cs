using System;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FieldCamera
{
    /// <summary>
    /// カメラに関する入力を提供するクラス
    /// </summary>
    public class InputProvider : IDisposable
    {
        // カメラ移動有効キー名
        private const string CAMERA_MOVE_ACTION_NAME = "Move";
        // クリックアクション名
        private const string CLICK_ACTION_NAME = "Click";
        private const string MOUSE_WHEEL_ACTION_NAME = "ScrollWheel";

        private InputAction _cameraMoveAction;
        private InputAction _clickAction;
        private InputAction _mouseWheelAction;

        private Subject<Vector2> _cameraMoveInput = new Subject<Vector2>();
        public Observable<Vector2> CameraMoveInput => _cameraMoveInput;

        private Subject<Unit> _clickInput = new Subject<Unit>();
        public Observable<Unit> ClickInput => _clickInput;

        private Subject<float> _mouseWheelInput = new Subject<float>();
        public Observable<float> MouseWheelInput => _mouseWheelInput;

        private Subject<Unit> _qKeyInput = new Subject<Unit>();
        public Observable<Unit> QKeyInput => _qKeyInput;

        private CompositeDisposable _disposables = new CompositeDisposable();

        public InputProvider()
        {
            // InputSystemからアクションを取得
            _cameraMoveAction = InputSystem.actions.FindAction(CAMERA_MOVE_ACTION_NAME);
            _clickAction = InputSystem.actions.FindAction(CLICK_ACTION_NAME);
            _mouseWheelAction = InputSystem.actions.FindAction(MOUSE_WHEEL_ACTION_NAME);
            _disposables = new CompositeDisposable();

            _cameraMoveInput = new Subject<Vector2>();
            _clickInput = new Subject<Unit>();
            _mouseWheelInput = new Subject<float>();
            _qKeyInput = new Subject<Unit>();
        }

        /// <summary>
        /// 入力受け取りを開始する
        /// </summary>
        public void StartHandling()
        {
            // 既存の購読を破棄してから新規作成（重複購読を防ぐ）
            _disposables?.Dispose();
            _disposables = new CompositeDisposable();
            
            // 毎フレーム、カメラ移動入力を監視して通知
            Observable.EveryUpdate()
            .Select(_ => _cameraMoveAction.ReadValue<Vector2>())
            .Where(input => input != Vector2.zero)
            .Subscribe(input =>
            {
                _cameraMoveInput.OnNext(input);
            })
            .AddTo(_disposables);

            // 毎フレーム、マウスホイール入力を監視して通知
            Observable.EveryUpdate()
            .Select(_ => _mouseWheelAction.ReadValue<Vector2>().y)
            .Where(y => y != 0)
            .Subscribe(y =>
            {
                _mouseWheelInput.OnNext(y);
            })
            .AddTo(_disposables);

            // Qキー入力を監視して通知
            Observable.EveryUpdate()
            .Where(_ => Keyboard.current != null && Keyboard.current.qKey.wasPressedThisFrame)
            .Subscribe(_ =>
            {
                _qKeyInput.OnNext(Unit.Default);
            })
            .AddTo(_disposables);

            // クリック入力を監視して通知（Disposableに追加しないため手動管理）
            _clickAction.performed += OnClickPerformed;
        }

        private void OnClickPerformed(InputAction.CallbackContext ctx)
        {
            if (ctx.ReadValueAsButton())
            {
                _clickInput.OnNext(Unit.Default);
            }
        }

        public void Dispose()
        {
            _disposables?.Dispose();
            if (_clickAction != null)
            {
                _clickAction.performed -= OnClickPerformed;
            }
        }
    }
}