using UnityEngine;
using VContainer.Unity;
using VContainer;

namespace MinutesGame.DX.Installer
{
    public class FactoryInstaller : MonoBehaviour, IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<Common.Factory.ArrowFactory>(Lifetime.Singleton);
        }
    }
}