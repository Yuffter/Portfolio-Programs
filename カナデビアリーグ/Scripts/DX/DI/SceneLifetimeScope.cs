using VContainer;
using VContainer.Unity;
using UnityEngine;
using Alchemy.Inspector;
using KanadeviaProject.GameCores.MainSystems;
using System.Collections.Generic;
using MinutesGame.Common.Arrow;
using Unity.VisualScripting;

namespace MinutesGame.DX.DI
{
    public class SceneLifetimeScope : LifetimeScope
    {
        [Title("Installer")]
        [SerializeField,Header("ゲームステート関連の登録を行うインストーラ")]
        private Installer.GameStateInstaller _gameStateInstaller;

        [SerializeField,Header("タイマー関連の登録を行うインストーラ")]
        private Common.Installer.TimerInstaller _timerInstaller;

        [SerializeField, Header("スコア関連の登録を行うインストーラ")]
        private Common.Installer.ScoreInstaller _scoreInstaller;
        [SerializeField, Header("ファクトリー関連の登録を行うインストーラ")]
        private Common.Installer.FactoryInstaller _factoryInstaller;

        [SerializeField, Header("タイプライター関連の登録を行うインストーラ")]
        private Common.Installer.TypewriteInstaller _typewriteInstaller;
        [SerializeField, Header("矢印オブジェクト4つ")]
        private List<ArrowController> _arrowControllers;

        [SerializeField, Header("ゲーム設定")]
        private Common.SO.GameSetting _gameSetting;
        [SerializeField, Header("矢印スプライト設定")]
        private Common.SO.ArrowSprites _arrowSprites;

        [SerializeField, Header("本番用入力受け取りクラス")]
        private Common.Input.MainInput _mainInput;

        [SerializeField, Header("浮遊文字コントローラープレハブ")]
        private Common.Factory.FloatingTextFactory _floatingTextFactory;

        [SerializeField, Header("説明文表示用オブジェクト")]
        private Common.Explanation.ExplanationView _explanationView;

        [SerializeField, Header("ダイアログ表示用オブジェクト")]
        private Common.DialogBox.DialogBoxController _dialogBoxController;

        [SerializeField, Header("RECコントローラー")]
        private REC.RECController _recController;

        [SerializeField, Header("リアクションアニメーター")]
        private Animator _reactionAnimator;

        [SerializeField, Header("ゲーム終了ビュー")]
        private Common.FinishText.FinishTextView _finishTextView;
        [SerializeField, Header("オートビュー")]
        private Auto.AutoView _autoView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<MinutesGame.DX.EntryPoint.GameEntryPoint>(Lifetime.Singleton)
                .AsSelf();
            builder.RegisterComponentInHierarchy<MinutesDXMiniGameSystemFacade>().AsImplementedInterfaces().AsSelf();

            _gameStateInstaller.Install(builder);
            _timerInstaller.Install(builder);
            _scoreInstaller.Install(builder);
            _factoryInstaller.Install(builder);
            _typewriteInstaller.Install(builder);

            builder.RegisterInstance(_gameSetting).As<Common.SO.GameSetting>();
            builder.RegisterInstance(_arrowSprites).As<Common.SO.ArrowSprites>();

            // 入力モッククラスの登録
             builder.Register<Common.Input.IInput, Common.Input.MockInput>(Lifetime.Singleton);
            // builder.RegisterInstance(_mainInput).As<Common.Input.IInput>();

            // 矢印オブジェクトの登録
            builder.RegisterInstance(_arrowControllers).As<List<ArrowController>>();
            builder.Register<ArrowContainer>(Lifetime.Singleton);

            // 浮遊文字コントローラープレハブの登録
            builder.RegisterInstance(_floatingTextFactory).As<Common.Factory.FloatingTextFactory>();

            // 説明文表示用オブジェクトの登録
            builder.RegisterInstance(_explanationView).As<Common.Explanation.ExplanationView>();

            // ダイアログ表示用オブジェクトの登録
            builder.RegisterInstance(_dialogBoxController).As<Common.DialogBox.DialogBoxController>();

            // RECコントローラーの登録
            builder.RegisterInstance(_recController).As<REC.RECController>();

            // リアクションアニメーターの登録
            builder.RegisterInstance(_reactionAnimator).As<Animator>();

            // ゲーム終了ビューの登録
            builder.RegisterInstance(_finishTextView).As<Common.FinishText.FinishTextView>();

            // オートビューの登録
            builder.RegisterInstance(_autoView).As<Auto.AutoView>();
        }
    }
}
