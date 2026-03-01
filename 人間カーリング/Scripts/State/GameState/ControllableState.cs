using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace State
{
    public class ControllableState : StateBase
    {
        private FieldCamera.Controller _fieldCameraController;
        private PlayerStopManager _playerStopManager;
        private Player.HoverEffectFacade _hoverEffectFacade;
        private PullPlayer _pullPlayer;
        public override void OnUpdate()
        {
            // プレイヤーが弾かれたらWaiting状態に遷移
            if (_playerStopManager.isStopped == false)
            {
                GameStateMachine.Instance.ChangeState(GameState.Waiting);
            }
        }

        public override async void OnEnter()
        {
            _fieldCameraController = ServiceLocator.Resolve<FieldCamera.Controller>();
            _playerStopManager = ServiceLocator.Resolve<PlayerStopManager>();
            _hoverEffectFacade = ServiceLocator.Resolve<Player.HoverEffectFacade>();
            _pullPlayer = ServiceLocator.Resolve<PullPlayer>();
            // フィールドカメラをOverview状態に遷移させる
            _fieldCameraController.ChangeOverviewState();
            _hoverEffectFacade.EnableHoverEffects();
            _pullPlayer.canPull = true;

            // 消耗品を使用可能にする
            ServiceLocator.Resolve<SO.EventHub.ConsumablesEventHub>().EnableConsumablesEvent.Raise();
        }

        public override void OnExit()
        {
            ServiceLocator.Resolve<SO.EventHub.ConsumablesEventHub>().DisableConsumablesEvent.Raise();
        }
    }
}
