using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace State
{
    public class WaitingState : StateBase
    {
        private FieldCamera.Controller _fieldCameraController;
        private PlayerStopManager _playerStopManager;
        private Player.HoverEffectFacade _hoverEffectFacade;
        private PullPlayer _pullPlayer;
        private Score.IFacade _scoreFacade;
        private TurnEndEventManager _turnEndEventManager;
        private TurnEndEventBus _turnEndEventBus;
        public override void OnEnter()
        {
            _fieldCameraController = ServiceLocator.Resolve<FieldCamera.Controller>();
            _playerStopManager = ServiceLocator.Resolve<PlayerStopManager>();
            _hoverEffectFacade = ServiceLocator.Resolve<Player.HoverEffectFacade>();
            _pullPlayer = ServiceLocator.Resolve<PullPlayer>();
            _scoreFacade = ServiceLocator.Resolve<Score.IFacade>();
            _turnEndEventManager = ServiceLocator.Resolve<TurnEndEventManager>();
            _turnEndEventBus = ServiceLocator.Resolve<TurnEndEventBus>();

            // フィールドカメラをWaiting状態に遷移させる
            _fieldCameraController.ChangeWaitingState();
            _hoverEffectFacade.DisableHoverEffects();
            _pullPlayer.canPull = false;
            ServiceLocator.Resolve<SO.EventHub.GuideEventHub>().FlickedEvent.Raise();
            Debug.Log("WaitingStateに入りました");
        }

        public override void OnUpdate()
        {
            Debug.Log("止まるの待ち中");
            if (_playerStopManager.isStopped)   // プレイヤーが停止したらターン開始状態に遷移
            {

                //プレイヤーが停止したらターン開始状態に遷移
                GameStateMachine.Instance.ChangeStateAsync(GameState.TurnStart, TurnEndEventsAsync);
            }
        }

        public override async void OnExit()
        {
            // ターン終了を通知　アイテム削除等の処理が走る
              _turnEndEventBus.NotifyTurnEnd();

        }

        private async UniTask TurnEndEventsAsync()
        {
            Debug.Log("WaitingState: ターン終了イベントの実行を開始"+Time.time);
            if(_turnEndEventManager != null)
            {
                await _turnEndEventManager.ExecuteAsync(); // ターン終了イベントを非同期に実行
                Debug.Log("WaitingState: ターン終了イベントの実行が完了"+Time.time);
            }

            _turnEndEventManager?.ResetTurnEndEvents(); // ターン終了イベントをクリア
            _scoreFacade.SaveCurrentTurnScore(); // スコアを保存
        }
    }
}
