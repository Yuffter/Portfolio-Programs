using UnityEngine;
using SO;
using UnityEngine.SceneManagement;

namespace State {
    public class GameStartState : StateBase
    {
        public override async void OnEnter()
        {
            PullPlayer pullPlayer = ServiceLocator.Resolve<PullPlayer>();
            pullPlayer.canPull = false;

            // 機能群シーンのロードを行う
            var currentStageData = ServiceLocator.Resolve<CurrentStageData>();
            foreach (var feature in currentStageData.Data.FeatureScenes)
            {
                if (feature.ShouldLoadAdditively == false) continue;
                await SceneManager.LoadSceneAsync(feature.SceneName, LoadSceneMode.Additive);
            }

            // 消耗品をセットする
            var consumablesEventHub = ServiceLocator.Resolve<SO.EventHub.ConsumablesEventHub>();
            consumablesEventHub.InitializeConsumablesEvent.Raise(currentStageData.Data.StageConsumables);
            var gameStartAnimator = ServiceLocator.Resolve<Animation.GameStartAnimator>();
            await gameStartAnimator.PlayAsync();
            GameStateMachine.Instance.ChangeState(GameState.TurnStart);
        }
    }
}
