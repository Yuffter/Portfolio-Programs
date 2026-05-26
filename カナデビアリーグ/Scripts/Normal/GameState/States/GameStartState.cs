using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using Pottery.Significant;

namespace MinutesGame.Normal.GameState.States
{
    public class GameStartState : Common.GameState.GameStateBase
    {
        [Inject]
        private readonly Common.Timer.TimerPresenter _timerPresenter;
        [Inject]
        private readonly Common.Score.ScorePresenter _scorePresenter;
        [Inject]
        private readonly Common.Explanation.ExplanationView _explanationView;
        [Inject]
        private readonly IObjectResolver _objectResolver;

        public override void Initialize()
        {
            AddTransition<GeneratingState>(Common.GameState.GameStateType.Generating);
        }
        public override async void Enter()
        {
            await _explanationView.ShowExplanationTextAsync();
            _timerPresenter.StartTimer();
            _scorePresenter.Boot();

            TimerEventHub.PublishTimerStart();
            _objectResolver.Resolve<Common.GameState.GameStateMachine>().ChangeState(Common.GameState.GameStateType.Generating);
        }
    }
}
