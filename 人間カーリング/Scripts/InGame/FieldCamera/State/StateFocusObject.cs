using Common;
using UnityEngine;
using R3;
using AudioManager.SE;

namespace FieldCamera.State
{
    public partial class StateMachine
    {
        /// <summary>
        /// オブジェクトにフォーカスしているときのステート
        /// </summary>
        public class StateFocusObject : StateBase
        {
            private readonly StateMachine _stateMachine;
            private CompositeDisposable _disposables = new CompositeDisposable();

            public StateFocusObject(StateMachine stateMachine)
            {
                _stateMachine = stateMachine;
            }

            public override void Enter()
            {
                _disposables = new CompositeDisposable();

                // SEを再生
                SEManager.Instance.Play(SEName.CharacterSelect);

                // クリックされたオブジェクトを取得
                GameObject clickedObject = _stateMachine.DependencyContainer.ClickObjectProvider.GetClickedObject();
                if (clickedObject == null)
                {
                    // クリックされたオブジェクトがない場合はOverview状態に遷移
                    _stateMachine.ChangeState(FieldCameraState.Overview);
                    return;
                }

                // フォーカス前のカメラ位置を保存して、クリックされたオブジェクトにフォーカス
                // _stateMachine.DependencyContainer.BeforeFocusPosition = _stateMachine.DependencyContainer.MainCamera.transform.position;
                _stateMachine.DependencyContainer.OverviewCameraController.FocusPosition(clickedObject.transform.position);

                // 入力受け取りを開始
                _stateMachine.DependencyContainer.InputProvider.StartHandling();

                // Qキー入力を購読してOverview状態に遷移
                _stateMachine.DependencyContainer.InputProvider.QKeyInput.Subscribe(_ =>
                {
                    _stateMachine.ChangeState(FieldCameraState.Overview);
                }).AddTo(_disposables);

                // ガイドテキスト表示イベントを発行
                _stateMachine.DependencyContainer.GuideEventHub.FocusEvent.Raise();

                // 詳細ビュー有効化イベントを発行
                _stateMachine.DependencyContainer.DescriptionViewerEventHub.DisableDescriptionViewerEvent.Raise();
            }

            public override void Exit()
            {
                // 入力受け取りを停止
                _stateMachine.DependencyContainer.InputProvider.Dispose();
                _disposables.Dispose();

                // SEを再生
                SEManager.Instance.Play(SEName.CharacterBack);

                // フォーカス前の位置にカメラをリセット
                ResetCameraPosition();

                // 詳細ビュー無効化イベントを発行
                _stateMachine.DependencyContainer.DescriptionViewerEventHub.EnableDescriptionViewerEvent.Raise();
            }

            private void ResetCameraPosition()
            {
                if (_stateMachine.DependencyContainer.BeforeFocusPosition != Vector3.zero)
                {
                    _stateMachine.DependencyContainer.OverviewCameraController.ResetFocus(_stateMachine.DependencyContainer.BeforeFocusPosition);
                }
                else
                {
                    _stateMachine.DependencyContainer.OverviewCameraController.ResetFocus(_stateMachine.DependencyContainer.OverviewCameraController.GetPosition());
                }
            }
        }
    }
}
