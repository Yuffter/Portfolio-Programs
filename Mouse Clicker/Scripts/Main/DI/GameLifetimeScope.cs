using VContainer;
using VContainer.Unity;
using Project.Main.GameSystems;
using Project.Main.GameSystems.Sequences;
using Project.Main.GameSystems.Presentation;
using Project.Main.HUD;
using UnityEngine;
using Project.Main.GameSystems.ScriptableObjects;
using System.Collections.Generic;
using Project.Main.Box;
using Project.Main.GameSystems.Actors;
using System.Linq;
using Project.Main.Cursor;
using Project.Main.Tests;
using Project.Main.Camera;
using Project.Main.GameSystems.Models;
using Project.Main.GameSystems.Presenters;
using Project.Main.Factory;
using TNRD;

namespace Project.Main.DI
{
    public class GameLifetimeScope : LifetimeScope
    {
        [Header("Views")]
        [SerializeField] private SerializableInterface<ITransitionPresentation> _transitionPresentation;
        [SerializeField] private SerializableInterface<IExplanationPresentation> _explanationPresentation;
        [SerializeField] private SerializableInterface<ICountDownPresentation> _countDownPresentation;
        [SerializeField] private SerializableInterface<ITimerPresentation> _timerPresentation;
        [SerializeField] private SerializableInterface<IFinishPresentation> _gameFinishPresentation;
        [SerializeField] private SerializableInterface<IScorePresentation> _scorePresentation;
        [SerializeField] private SerializableInterface<IResultPresentation> _resultPresentation;

        [Header("Parameters")]
        [SerializeField] private List<BoxController> _boxControllers;
        [SerializeField] private CursorSetting _cursorSetting;
        [SerializeField] private GameRule _gameRule;

        [Header("Controllers")]
        [SerializeField] private CursorController _cursorController;
        [SerializeField] private CameraController _cameraController;

        [Header("Factories")]
        [SerializeField] private MissTextFactory _missTextFactory;
        protected override void Configure(IContainerBuilder builder)
        {
            // エントリーポイントの登録
            builder.RegisterEntryPoint<GameEntryPoint>();

            // ゲームシステムの登録
            builder.Register<GameSystem>(Lifetime.Singleton);

            // シーケンスの登録
            builder.Register<ExplanationSequence>(Lifetime.Singleton);
            builder.Register<MainSequence>(Lifetime.Singleton);
            builder.Register<CountDownSequence>(Lifetime.Singleton);
            builder.Register<ResultSequence>(Lifetime.Singleton);

            // ビューの登録
            builder.RegisterComponent(_transitionPresentation.Value).As<ITransitionPresentation>();
            builder.RegisterComponent(_explanationPresentation.Value).As<IExplanationPresentation>();
            builder.RegisterComponent(_countDownPresentation.Value).As<ICountDownPresentation>();
            builder.RegisterComponent(_timerPresentation.Value).As<ITimerPresentation>();
            builder.RegisterComponent(_gameFinishPresentation.Value).As<IFinishPresentation>();
            builder.RegisterComponent(_scorePresentation.Value).As<IScorePresentation>();
            builder.RegisterComponent(_resultPresentation.Value).As<IResultPresentation>();

            // プロパティの登録
            builder.RegisterInstance(_cursorSetting).As<CursorSetting>().AsSelf();
            builder.RegisterInstance(_gameRule).As<GameRule>().AsSelf();
            builder.Register<ScoreModel>(Lifetime.Singleton);

            // ロジックの登録
            builder.Register<BoxCollection>(Lifetime.Singleton);
            builder.Register<TimerSystem>(Lifetime.Singleton);
            builder.Register<MouseGenerator>(Lifetime.Singleton);

            // BoxControllerのリストをコンクリート型とインターフェース型の両方で登録
            builder.RegisterInstance(_boxControllers).As<List<BoxController>>().AsSelf();
            builder.RegisterInstance(_boxControllers.Cast<IBox>().ToList()).As<List<IBox>>();

            // BoxControllerにGameRuleを初期化
            foreach (var boxController in _boxControllers)
            {
                boxController.Initialize(_gameRule);
            }

            // カーソル関連の登録
            builder.RegisterComponent(_cursorController).AsImplementedInterfaces().AsSelf();

            // カメラ関連の登録
            builder.RegisterComponent(_cameraController).AsImplementedInterfaces().AsSelf();

            // プレゼンターの登録
            builder.Register<ScorePresenter>(Lifetime.Singleton);
            // テストの登録
            // builder.RegisterEntryPoint<BoxCollectionTest>();
            // builder.RegisterEntryPoint<MissTextFactoryTest>();


            builder.RegisterInstance(_missTextFactory).AsSelf();
        }
    }
}