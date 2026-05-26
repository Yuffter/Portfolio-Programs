using KanadeviaProject.GameCores.MainSystems;
using MinutesGame.Common.Input;
using MinutesGame.Common.Score;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace MinutesGame.Normal
{
    public class MinutesNormalMiniGameSystemFacade : MonoBehaviour, IMiniGameSystemFacade
    {
        public Subject<int> OnFinishMiniGameSystem { get; } = new();

        private EntryPoint.GameEntryPoint _gameEntryPoint;
        private ScorePresenter _scorePresenter;

        [SerializeField]
        private MainInput _mainInput;
        [Inject]
        public void Construct(EntryPoint.GameEntryPoint gameEntryPoint, ScorePresenter scorePresenter)
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
