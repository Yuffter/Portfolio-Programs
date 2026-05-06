using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Project.Main.Box;
using Project.Main.GameSystems.Actors;
using Project.Main.GameSystems.Presentation;
using Project.Main.GameSystems.ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Main.GameSystems.Sequences
{
    public class MainSequence
    {
        private readonly TimerSystem _timerSystem;
        private readonly IFinishPresentation _finishPresentation;
        private readonly GameRule _gameRule;
        private readonly MouseGenerator _mouseGenerator;
        private readonly ICursor _cursor;
        private bool _isTimerActive = true;
        public bool IsTimerActive => _isTimerActive;

        public MainSequence(TimerSystem timerSystem, IFinishPresentation finishPresentation, GameRule gameRule, MouseGenerator mouseGenerator, ICursor cursor)
        {
            _timerSystem = timerSystem;
            _finishPresentation = finishPresentation;
            _gameRule = gameRule;
            _mouseGenerator = mouseGenerator;
            _cursor = cursor;
        }

        /// <summary>
        /// ゲームを開始する
        /// </summary>
        /// <returns></returns>
        public async UniTask StartSequenceAsync()
        {
            _timerSystem.StartTimer(_gameRule.TimeLimit);
            _timerSystem.OnTimerFinished += OnTimerFinished;
            _mouseGenerator.StartGenerating();
        }

        private void OnTimerFinished()
        {
            UniTask.Void(async () =>
            {
                _mouseGenerator.StopGenerating();
                _cursor.Stop();
                await _finishPresentation.ShowFinishTextAsync();

                _isTimerActive = false;
            });
        }
    }
}