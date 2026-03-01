using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using R3;
using Core;
using UnityEngine.EventSystems;
using System.Linq;

namespace State
{
    public class ResultState : StateBase
    {
        private Result.IFacade _resultFacade;
        private Score.IFacade _scoreFacade;
        private Animations.BackToStageSelectButtonAnimator _backToStageSelectButtonAnimator;
        private CompositeDisposable _disposables = new CompositeDisposable();

        public override async void OnEnter()
        {
            _disposables = new CompositeDisposable();
            _resultFacade = ServiceLocator.Resolve<Result.IFacade>();
            _scoreFacade = ServiceLocator.Resolve<Score.IFacade>();
            _backToStageSelectButtonAnimator = ServiceLocator.Resolve<Animations.BackToStageSelectButtonAnimator>();
            // 結果画面に入ったときの処理をここに書く
            Debug.Log("Entered Result State");

            List<float> scores = _scoreFacade.GetEachTurnScores();
            // ハイスコアを計算し、現在のスコアと比較して、ハイスコア更新かどうかを判定
            int totalScore = scores.Sum(score => (int)score);
            await _resultFacade.ShowResultScreen(scores, isNewHighScore: totalScore > ServiceLocator.Resolve<SO.CurrentStageData>().Data.HighScore);

            // unityroomのランキングに送信する
            ServiceLocator.Resolve<SO.StageDatabase>().SubmitScoreToRanking();
            _backToStageSelectButtonAnimator.Play();
            _backToStageSelectButtonAnimator.OnClickAsObservable
                .Subscribe(_ => {
                    GameStateMachine.Instance.ChangeStateWaitUntilSceneLoaded(GameState.StageSelect, "StageSelect");

                    foreach (var feature in ServiceLocator.Resolve<SO.CurrentStageData>().Data.FeatureScenes)
                    {
                        if (feature.ShouldLoadAdditively == false) continue;
                        AddictiveSceneManager.Instance.UnloadScene(feature.SceneName);
                    }
                    AddictiveSceneManager.Instance.LoadUnloadScene(ServiceLocator.Resolve<SO.CurrentStageData>().Data.StageSceneName, "StageSelect");
                }).AddTo(_disposables);
        }

        public override void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
