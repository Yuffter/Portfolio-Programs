using Common;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;
using DG.Tweening;
using AudioManager.SE;
using System;

namespace Core
{
    public class AddictiveSceneManager : SingletonMonoBehaviour<AddictiveSceneManager>
    {
        [SerializeField, Header("遷移アニメーションコントローラ")]
        private TransitionProgressController _transitionProgressController;
        /// <summary>
        /// 非同期でシーンをアンロードし、別のシーンをAdditiveでロードする
        /// </summary>
        /// <param name="unloadSceneName">アンロードするシーン名</param>
        /// <param name="loadSceneName">ロードするシーン名</param>
        public async void LoadUnloadScene(string unloadSceneName, string loadSceneName)
        {
            SEManager.Instance.Play(SEName.Transition_1);
            await DOTween.To(() => _transitionProgressController.progress, x => _transitionProgressController.progress = x, 1f, 1f).SetEase(Ease.InOutSine).AsyncWaitForCompletion();
            await SceneManager.UnloadSceneAsync(unloadSceneName).ToUniTask();
            await SceneManager.LoadSceneAsync(loadSceneName, LoadSceneMode.Additive).ToUniTask();
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(loadSceneName));
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            SEManager.Instance.Play(SEName.Transition_2);
            await DOTween.To(() => _transitionProgressController.progress, x => _transitionProgressController.progress = x, 0f, 1f).SetEase(Ease.InOutSine).AsyncWaitForCompletion();
        }

        /// <summary>
        /// 非同期でシーンをアンロードし、別のシーンをAdditiveでロードする
        /// </summary>
        /// <param name="unloadSceneId">アンロードするシーンID</param>
        /// <param name="loadSceneId">ロードするシーンID</param>

        public async void LoadUnloadScene(int unloadSceneId, int loadSceneId)
        {
            await SceneManager.UnloadSceneAsync(unloadSceneId).ToUniTask();
            await SceneManager.LoadSceneAsync(loadSceneId, LoadSceneMode.Additive).ToUniTask();
        }

        /// <summary>
        /// 非同期でシーンをAdditiveでロードする
        /// </summary>
        /// <param name="sceneName"></param>
        public async void LoadSceneAdditive(string sceneName)
        {
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).ToUniTask();
        }

        /// <summary>
        /// 非同期でシーンをアンロードする
        /// </summary>
        /// <param name="sceneName"></param>
        public async void UnloadScene(string sceneName)
        {
            await SceneManager.UnloadSceneAsync(sceneName).ToUniTask();
        }
    }
}
