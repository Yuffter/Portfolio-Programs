using UnityEngine;
using VContainer.Unity;
using VContainer;

namespace MinutesGame.Common.Installer
{
    public class FactoryInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField, Header("TalkのPrefab")]
        private GameObject _talkPrefab;
        public void Install(IContainerBuilder builder)
        {
            builder.Register<Factory.ArrowFactory>(Lifetime.Singleton);
            builder.Register<Factory.TalkFactory>(Lifetime.Singleton);
            builder.RegisterInstance(_talkPrefab).As<GameObject>();
        }
    }
}