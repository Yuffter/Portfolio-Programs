using System;
using UniRx;
using UnityEngine;
using VContainer;

namespace MinutesGame.Common.Score
{
    public class ScorePresenter : IDisposable
    {
        private readonly ScoreModel _scoreModel;
        private readonly ScoreView _scoreView;
        private CompositeDisposable _disposables = new CompositeDisposable();
        /// <summary>
        /// 現在のスコアを取得します
        /// </summary>
        /// <returns></returns>

        public int GetCurrentScore() => _scoreModel.Score.Value;

        [Inject]
        public ScorePresenter(ScoreModel scoreModel, ScoreView scoreView)
        {
            _scoreModel = scoreModel;
            _scoreView = scoreView;
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }

        /// <summary>
        /// スコア表示を初期化します
        /// </summary>
        public void Boot()
        {
            _scoreModel.Score.Subscribe(score =>
            {
                _scoreView.SetScoreText(score);
            }).AddTo(_disposables);
        }

        /// <summary>
        /// スコアを加算します
        /// </summary>
        /// <param name="amount">スコア量</param>
        public void AddScore(int amount)
        {
            _scoreModel.AddScore(amount);
        }
    }
}