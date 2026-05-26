using UnityEngine;
using VContainer.Unity;
using VContainer;

namespace MinutesGame.Common.Installer
{
    public class TypewriteInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField]
        private Typewrite.TypewriteView _typewriteView;
        public void Install(IContainerBuilder builder)
        {
            builder.Register<Typewrite.TypewriteLogic>(Lifetime.Singleton);
            builder.RegisterInstance(_typewriteView).As<Typewrite.TypewriteView>();
            builder.Register<Typewrite.TypewritePresenter>(Lifetime.Singleton);
        }
    }
}