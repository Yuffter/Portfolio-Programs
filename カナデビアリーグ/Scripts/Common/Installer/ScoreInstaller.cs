using UnityEngine;
using VContainer.Unity;
using VContainer;

namespace MinutesGame.Common.Installer
{
    public class ScoreInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField]
        private Score.ScoreView _scoreView;
        public void Install(IContainerBuilder builder)
        {
            builder.Register<Score.ScoreModel>(Lifetime.Singleton);
            builder.RegisterInstance(_scoreView).As<Score.ScoreView>();
            builder.Register<Score.ScorePresenter>(Lifetime.Singleton);
        }
    }
}