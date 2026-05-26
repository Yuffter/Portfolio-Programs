using UnityEngine;
using VContainer.Unity;
using VContainer;
using MinutesGame.Common.Timer;

namespace MinutesGame.Common.Installer
{
    public class TimerInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField]
        private TimerView _timerView;
        public void Install(IContainerBuilder builder)
        {
            builder.Register<TimerModel>(Lifetime.Singleton);
            builder.RegisterInstance(_timerView).As<ITimerView>();
            builder.Register<TimerPresenter>(Lifetime.Singleton);
        }
    }
}