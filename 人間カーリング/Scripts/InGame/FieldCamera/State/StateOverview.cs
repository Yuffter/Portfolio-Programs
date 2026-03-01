using Common;
using UnityEngine;
using R3;

namespace FieldCamera.State
{
    public partial class StateMachine
    {
        public class StateOverview : StateBase
        {
            private readonly StateMachine _stateMachine;
            private CompositeDisposable _disposables = new CompositeDisposable();

            public StateOverview(StateMachine stateMachine)
            {
                _stateMachine = stateMachine;
            }

            public override void Enter()
            {
                _disposables = new CompositeDisposable();

                // カメラ操作を有効化
                _stateMachine.DependencyContainer.OverviewCameraController.EnableInput();

                // 入力受け取りを開始
                _stateMachine.DependencyContainer.InputProvider.StartHandling();

                // WASD入力を購読し、カメラを移動させる
                _stateMachine.DependencyContainer.InputProvider.CameraMoveInput.Subscribe(input =>
                {
                    _stateMachine.DependencyContainer.OverviewCameraController.Move(input);
                }).AddTo(_disposables);

                // クリック入力を購読し、フォーカス状態に遷移
                _stateMachine.DependencyContainer.InputProvider.ClickInput.Subscribe(_ =>
                {
                    // クリックされたプレイヤーが存在しなかった場合には何もしない
                    GameObject clickedObject = _stateMachine.DependencyContainer.ClickObjectProvider.GetClickedObject();
                    if (clickedObject == null)
                    {
                        return;
                    }
                    
                    _stateMachine.ChangeState(FieldCameraState.FocusObject);
                }).AddTo(_disposables);

                // マウスホイール入力を購読し、カメラをズームさせる
                _stateMachine.DependencyContainer.InputProvider.MouseWheelInput.Subscribe(delta =>
                {
                    _stateMachine.DependencyContainer.OverviewCameraController.Zoom(delta);
                }).AddTo(_disposables);

                // ガイドテキスト表示イベントを発行
                _stateMachine.DependencyContainer.GuideEventHub.OverviewEvent.Raise();
            }

            public override void Exit()
            {
                // カメラ操作を無効化
                _stateMachine.DependencyContainer.OverviewCameraController.DisableInput();

                // 入力受け取りを停止
                _stateMachine.DependencyContainer.InputProvider.Dispose();
                _disposables.Dispose();
            }
        }
    }
}
