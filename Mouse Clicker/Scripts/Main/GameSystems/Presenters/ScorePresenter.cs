using Project.Main.GameSystems.Models;
using Project.Main.GameSystems.Presentation;
using R3;
using UnityEngine;

namespace Project.Main.GameSystems.Presenters
{
    public class ScorePresenter
    {
        private readonly ScoreModel _scoreModel;
        private readonly IScorePresentation _scorePresentation;

        public ScorePresenter(ScoreModel scoreModel, IScorePresentation scorePresentation)
        {
            _scoreModel = scoreModel;
            _scorePresentation = scorePresentation;
        }

        public void Initialize()
        {
            _scoreModel.Score
            .Subscribe(x =>
            {
                _scorePresentation.UpdateText(x);
            });
        }
    }
}