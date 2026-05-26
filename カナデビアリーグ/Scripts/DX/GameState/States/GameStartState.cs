using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using Pottery.Significant;

namespace MinutesGame.DX.GameState.States
{
    public class GameStartState : Common.GameState.GameStateBase
    {
        [Inject]
        private readonly Common.Timer.TimerPresenter _timerPresenter;
        [Inject]
        private readonly Common.Score.ScorePresenter _scorePresenter;
        [Inject]
        private readonly IObjectResolver _objectResolver;
        [Inject]
        private readonly Common.Explanation.ExplanationView _explanationView;
        [Inject]
        private readonly REC.RECController _recController;

        public override void Initialize()
        {
            AddTransition<GeneratingState>(Common.GameState.GameStateType.Generating);
        }
        public override async void Enter()
        {
            _recController.MoveToMonitorPosition();
            await _explanationView.ShowExplanationTextAsync();
            _timerPresenter.StartTimer();
            _scorePresenter.Boot();

            TimerEventHub.PublishTimerStart();
            _objectResolver.Resolve<Common.GameState.GameStateMachine>().ChangeState(Common.GameState.GameStateType.Generating);
        }
    }
}
