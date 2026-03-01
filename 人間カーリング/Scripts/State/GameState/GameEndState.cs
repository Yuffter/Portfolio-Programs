using System;
using Cysharp.Threading.Tasks;
using AudioManager.SE;

namespace State
{
    public class GameEndState : StateBase
    {
        public override async void OnEnter()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            var gameFinishAnimator = ServiceLocator.Resolve<Animation.GameFinishAnimator>();
            SEManager.Instance.Play(SEName.GameEnd);
            await gameFinishAnimator.PlayAsync();
            GameStateMachine.Instance.ChangeState(GameState.Result);
        }
    }
}
