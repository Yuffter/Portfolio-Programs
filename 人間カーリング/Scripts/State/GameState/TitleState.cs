using UnityEngine;
using UnityEngine.SceneManagement;
using AudioManager.BGM;
using Core;
using Cysharp.Threading.Tasks;

namespace State
{
    public class TitleState : StateBase
    {
        public override async void OnEnter()
        {
            BGMManager.Instance.Play(BGMName.Title, 0.5f, false, true);

            // バージョンが異なる場合はハイスコアを初期化する
            if (ServiceLocator.Resolve<SO.StageDatabase>().Version != PlayerPrefs.GetInt("Version", -1))
            {
                ServiceLocator.Resolve<SO.StageDatabase>().ResetHighScores();
                PlayerPrefs.SetInt("Version", ServiceLocator.Resolve<SO.StageDatabase>().Version);
                PlayerPrefs.Save();
            }
            ServiceLocator.Resolve<SO.StageDatabase>().LoadHighScores();
            ServiceLocator.Resolve<Title.MainAnimator>().Initialize();
            await ServiceLocator.Resolve<Title.MainAnimator>().PlayOpeningAnimationAsync();
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
            await ServiceLocator.Resolve<Title.MainAnimator>().PlayTransitionAnimationAsync();
            BGMManager.Instance.Stop(true);
            GameStateMachine.Instance.ChangeStateWaitUntilSceneLoaded(GameState.StageSelect, "StageSelect");
            AddictiveSceneManager.Instance.LoadUnloadScene("Title", "StageSelect");
        }
    }
}
