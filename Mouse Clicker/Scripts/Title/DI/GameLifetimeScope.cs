using VContainer;
using VContainer.Unity;
using UnityEngine;
using Project.Title.GameSystems.Presentation;
using Project.Title.Test;
using Project.Title.HUD;
using Project.Title.GameSystems.Sequences;
using Project.Title.GameSystems;
using TNRD;

namespace Project.Title.DI
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private SerializableInterface<IFlashTextPresentation> _flashTextPresentation;
        [SerializeField] private SerializableInterface<ITransitionPresentation> _transitionPresentation;
        protected override void Configure(IContainerBuilder builder)
        {
            // Viewの登録
            builder.RegisterInstance(_flashTextPresentation.Value).As<IFlashTextPresentation>();
            builder.RegisterInstance(_transitionPresentation.Value).As<ITransitionPresentation>();

            // Sequenceの登録
            builder.Register<MainSequence>(Lifetime.Singleton);
            builder.Register<BeginningSequence>(Lifetime.Singleton);

            // エントリーポイントの登録
            builder.RegisterEntryPoint<GameEntryPoint>(Lifetime.Singleton);
            builder.Register<GameSystem>(Lifetime.Singleton);

            // Testの登録
            // builder.RegisterEntryPoint<FlashTextViewTest>(Lifetime.Singleton);
        }
    }
}