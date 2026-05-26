using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MinutesGame.Normal.Installer 
{
    public class GameStateInstaller : MonoBehaviour, IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            // ステートマシン本体を登録
            builder.Register<Common.GameState.GameStateMachine>(Lifetime.Singleton);

            // 各ゲームステートを登録
            builder.Register<GameState.States.GameStartState>(Lifetime.Singleton)
                .As<Common.GameState.GameStateBase>()
                .AsImplementedInterfaces()
                .AsSelf();
            builder.Register<GameState.States.GeneratingState>(Lifetime.Singleton)
                .As<Common.GameState.GameStateBase>()
                .AsImplementedInterfaces()
                .AsSelf();
            builder.Register<GameState.States.PlayerInputState>(Lifetime.Singleton)
                .As<Common.GameState.GameStateBase>()
                .AsImplementedInterfaces()
                .AsSelf();
            builder.Register<GameState.States.GameEndState>(Lifetime.Singleton)
                .As<Common.GameState.GameStateBase>()
                .AsImplementedInterfaces()
                .AsSelf();
        }
    }
}