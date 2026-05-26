using DG.Tweening;
using MinutesGame.Common.Arrow;
using MinutesGame.Common.Factory;
using UnityEngine;
using VContainer;
using Cysharp.Threading.Tasks;
using System;
using MinutesGame.Common.Timer;
using UniRx;

namespace MinutesGame.DX.GameState.States
{
    public class GeneratingState : Common.GameState.GameStateBase, IDisposable
    {
        [Inject]
        private readonly IObjectResolver _objectResolver;
        [Inject]
        private readonly ArrowFactory _arrowFactory;
        [Inject]
        private readonly Common.SO.ArrowSprites _arrowSprites;
        [Inject]
        private readonly ArrowContainer _arrowContainer;
        [Inject]
        private readonly TimerPresenter _timerPresenter;
        [Inject]
        private readonly Common.DialogBox.DialogBoxController _dialogBoxController;
        [Inject]
        private readonly Animator _reactionAnimator;
        private CompositeDisposable _disposables = new CompositeDisposable();
        public override void Initialize()
        {
            AddTransition<PlayerInputState>(Common.GameState.GameStateType.PlayerInput);
            AddTransition<GameEndState>(Common.GameState.GameStateType.GameEnd);
        }
        public override async void Enter()
        {
            _disposables = new CompositeDisposable();
            _timerPresenter.OnTimerCompleted
            .Subscribe(_ =>
            {
                // タイマーが終了したらゲーム終了状態へ遷移
                _objectResolver.Resolve<Common.GameState.GameStateMachine>().ChangeState(Common.GameState.GameStateType.GameEnd);
            }).AddTo(_disposables);

            // 矢印を生成してコンテナに追加
            for (int i = 0;i < _arrowContainer.ArrowsObjList.Count;i++)
            {
                Sprite arrowSprite = _arrowFactory.Create();
                ArrowType arrowType = _arrowSprites.GetArrowTypeFromSprite(arrowSprite);
                _arrowContainer.ArrowControllerList[i].SetArrowSpriteAndType(arrowSprite, arrowType);
                _arrowContainer.ArrowControllerQueue.Enqueue(_arrowContainer.ArrowControllerList[i]);
                _arrowContainer.ArrowControllerList[i].Unfocus();
            }

            for (int i = 0; i < _arrowContainer.ArrowsObjList.Count; i++)
            {
                _arrowContainer.ArrowControllerList[i].ThrowInAsync(1000 + i * 250, -1050 + i * 250).Forget();
            }

            _reactionAnimator.SetTrigger("Flash");
            _dialogBoxController.OpenDialogBoxAsync();
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            _arrowContainer.ArrowControllerList[0].Focus();

            // 生成が完了したら次の状態へ遷移
            _objectResolver.Resolve<Common.GameState.GameStateMachine>().ChangeState(Common.GameState.GameStateType.PlayerInput);
        }

        public override void Update()
        {
            // 必要に応じて更新処理を追加
        }

        public override void Exit()
        {
            _disposables.Dispose();
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
