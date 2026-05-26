using KanadeviaProject.GameCores.MainSystems;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using MinutesGame.Common.Input;
namespace MinutesGame.DX
{

    public class MinutesDXMiniGameSystemFacade : MonoBehaviour, IMiniGameSystemFacade
    {
        public Subject<int> OnFinishMiniGameSystem { get; } = new();

        private EntryPoint.GameEntryPoint _gameEntryPoint;
        private Common.Score.ScorePresenter _scorePresenter;

        [SerializeField]
        private MainInput _mainInput;
        [Inject]
        public void Construct(EntryPoint.GameEntryPoint gameEntryPoint, Common.Score.ScorePresenter scorePresenter)
        {
            _gameEntryPoint = gameEntryPoint;
            _scorePresenter = scorePresenter;
        }

        public void StartMiniGameSystem(Joycon assignedJoycon)
        {
            _mainInput.AssignJoycon(assignedJoycon);
            _gameEntryPoint.Initialize();
        }

        public void FinishMiniGameSystem()
        {
            int finalScore = _scorePresenter.GetCurrentScore();
            OnFinishMiniGameSystem.OnNext(finalScore);
        }
    }
}
