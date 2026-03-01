using System.Collections.Generic;
using R3;
using UnityEngine;
using AudioManager.SE;
using SO;
using AudioManager.BGM;
using Cysharp.Threading.Tasks;

namespace State
{
    public class StageSelectState : StateBase
    {
        private CompositeDisposable _disposables = new CompositeDisposable();
        public override async void OnEnter()
        {
            await UniTask.DelayFrame(1);
            BGMManager.Instance.Play(BGMName.StageSelectFirst, 1f, false, false, BGMName.StageSelectLoop);
            Debug.Log("StageSelectStateが開始されました");
            _disposables = new CompositeDisposable();

            // ステージデータベースを基にステージパネルを生成
            StageSelect.StagePanel.Factory.Factory factory = ServiceLocator.Resolve<StageSelect.StagePanel.Factory.Factory>();
            var stagePanels = factory.Create();

            // ステージパネルの管理クラスを初期化
            StageSelect.StagePanel.Manager stagePanelManager = ServiceLocator.Resolve<StageSelect.StagePanel.Manager>();
            StageSelect.StagePanel.IndexCalculator indexCalculator = ServiceLocator.Resolve<StageSelect.StagePanel.IndexCalculator>();
            indexCalculator.Initialize(0, stagePanels.Count - 1);
            stagePanelManager.Initialize(stagePanels, indexCalculator);

            // ステージ切り替えのコネクタを起動
            StageSelect.Connector.StageChangeConnector connector = ServiceLocator.Resolve<StageSelect.Connector.StageChangeConnector>();
            connector.Boot(stagePanelManager, _disposables);

            // ステージ切り替えコントローラーを初期化
            StageSelect.StageChanger.Controller stageChangerController = ServiceLocator.Resolve<StageSelect.StageChanger.Controller>();
            stageChangerController.Initialize(indexCalculator);

            // スタートボタンの監視を開始
            StageSelect.Button.StartButtonCollection startButtonCollection = new StageSelect.Button.StartButtonCollection();
            startButtonCollection.StartObserve(() =>
            {
                SEManager.Instance.Play(SEName.StageStart);
                // 選択中のステージのインデックスを取得
                int index = ServiceLocator.Resolve<StageSelect.StagePanel.IndexCalculator>().CurrentIndex.CurrentValue;
                var stageDatabase = ServiceLocator.Resolve<SO.StageDatabase>();
                BGMManager.Instance.Stop(true);
                GameStateMachine.Instance.ChangeStateWaitUntilSceneLoaded(GameState.GameStart, stageDatabase.Stages[index].StageSceneName);
            });
        }

        public override void OnExit()
        {
            // 選択中のステージのインデックスを取得
            int index = ServiceLocator.Resolve<StageSelect.StagePanel.IndexCalculator>().CurrentIndex.CurrentValue;

            // 選択されたステージ情報を登録する
            var currentStageData = ServiceLocator.Resolve<SO.CurrentStageData>();
            var stageDatabase = ServiceLocator.Resolve<SO.StageDatabase>();
            currentStageData.SetData(index, stageDatabase.Stages[index]);

            _disposables.Dispose();

            Core.AddictiveSceneManager.Instance.LoadUnloadScene("StageSelect", currentStageData.Data.StageSceneName);
        }
    }
}
