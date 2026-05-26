using UnityEngine;
using VContainer;

namespace MinutesGame.DX.GameState.States
{
    public class GameEndState : Common.GameState.GameStateBase
    {
        [Inject]
        private readonly IObjectResolver _objectResolver;
        [Inject]
        private readonly Common.FinishText.FinishTextView _finishTextView;
        public override void Initialize()
        {

        }

        public override async void Enter()
        {
            await _finishTextView.ShowFinishTextAsync();
            _objectResolver.Resolve<MinutesDXMiniGameSystemFacade>().FinishMiniGameSystem();
        }
    }
}
