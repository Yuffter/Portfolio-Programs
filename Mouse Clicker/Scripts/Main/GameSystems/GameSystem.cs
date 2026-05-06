using System;
using Cysharp.Threading.Tasks;
using KanKikuchi.AudioManager;
using Project.Main.GameSystems.Actors;
using Project.Main.GameSystems.Presentation;
using Project.Main.GameSystems.Presenters;
using Project.Main.GameSystems.Sequences;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Main.GameSystems
{
    public class GameSystem
    {
        private readonly MainSequence _mainSequence;
        private readonly ExplanationSequence _explanationSequence;
        private readonly CountDownSequence _countDownSequence;
        private readonly ResultSequence _resultSequence;
        private readonly ICursor _cursor;
        private readonly ScorePresenter _scorePresenter;
        private readonly ITransitionPresentation _transitionPresentation;

        public GameSystem(MainSequence mainSequence, ExplanationSequence explanationSequence, CountDownSequence countDownSequence, ResultSequence resultSequence, ICursor cursor, ScorePresenter scorePresenter, ITransitionPresentation transitionPresentation)
        {
            _mainSequence = mainSequence;
            _explanationSequence = explanationSequence;
            _cursor = cursor;
            _countDownSequence = countDownSequence;
            _resultSequence = resultSequence;
            _scorePresenter = scorePresenter;
            _transitionPresentation = transitionPresentation;
        }

        public async UniTask Initialize()
        {
            await _explanationSequence.StartSequenceAsync(1.0f);
            await _countDownSequence.StartCountDownAsync(3.0f);
            _cursor.Initialize(Vector2Int.zero);
            _scorePresenter.Initialize();
            await _mainSequence.StartSequenceAsync();
            await UniTask.WaitUntil(() => !_mainSequence.IsTimerActive);
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            BGMManager.Instance.Stop();
            await _resultSequence.StartSequenceAsync();
            await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
            SEManager.Instance.Play(SEPath.DECISION);
            await _transitionPresentation.FadeInAsync(1);
            SceneManager.LoadScene("Main");
        }
    }
}