using UnityEngine;
using VContainer;
using MinutesGame.Common.Arrow;
using MinutesGame.Common.Input;
using UniRx;
using System;
using MinutesGame.Common.Score;
using MinutesGame.Common.Timer;
using MinutesGame.Common.Typewrite;
using Pottery.Significant;

namespace MinutesGame.DX.GameState.States
{
    public class PlayerInputState : Common.GameState.GameStateBase, IDisposable
    {
        [Inject]
        private ArrowContainer _arrowContainer;
        [Inject]
        private IInput _input;
        [Inject]
        private IObjectResolver _objectResolver;
        [Inject]
        private ScorePresenter _scorePresenter;
        [Inject]
        private Common.SO.GameSetting _gameSetting;
        [Inject]
        private TimerPresenter _timerPresenter;
        [Inject]
        private TypewritePresenter _typewritePresenter;
        [Inject]
        private Common.Factory.TalkFactory _talkFactory;
        [Inject]
        private Common.Factory.FloatingTextFactory _floatingTextFactory;
        [Inject]
        private DX.Auto.AutoView _autoView;
        private const string SCORE_OBJ_NAME = "Score_key";

        private int _currentArrowIndex;
        private CompositeDisposable _disposables = new CompositeDisposable();
        private float _timeSinceLastDeletion = 0f;
        public override void Initialize()
        {
            AddTransition<GeneratingState>(Common.GameState.GameStateType.Generating);
            AddTransition<GameEndState>(Common.GameState.GameStateType.GameEnd);
        }

        public override void Enter()
        {
            _disposables = new CompositeDisposable();
            _currentArrowIndex = 0;
            _input.EnableInput();

            // タイマー完了時の処理
            _timerPresenter.OnTimerCompleted
            .Subscribe(_ =>
            {
                // タイマーが完了したらゲーム終了状態へ遷移
                _objectResolver.Resolve<Common.GameState.GameStateMachine>().ChangeState(Common.GameState.GameStateType.GameEnd);
            }).AddTo(_disposables);
        }

        public override void Update()
        {
            if (_input.IsHoldingRight)
            {
                // 長押し時間を計測
                _timeSinceLastDeletion += Time.deltaTime;
                // 一定時間経過したら矢印を削除
                if (_timeSinceLastDeletion >= _gameSetting.DxArrowDeletionInterval)
                {
                    FocusNextArrow();
                    _timeSinceLastDeletion = 0f;
                }

                _autoView.StartFlashing();
            }
            else
            {
                _autoView.StopFlashing();
            }
        }

        public override void Exit()
        {
            _disposables.Dispose();
            _input.DisableInput();
        }

        private void FocusNextArrow()
        {
            _scorePresenter.AddScore(_gameSetting.ScorePerHitDX);

            Common.FloatingText.FloatingTextController floatingTextController = _floatingTextFactory.Create();
            Vector3 position = GameObject.Find(SCORE_OBJ_NAME).GetComponent<RectTransform>().localPosition;
            floatingTextController.ShowFloatingText($"+{_gameSetting.ScorePerHitDX}", position + new Vector3(-100 + UnityEngine.Random.Range(-50f, 50f), 0, 0));

            // 入力待ちキューから押されたキーを削除して詰める
            _arrowContainer.ArrowControllerQueue.Dequeue();
            _currentArrowIndex++;
            _arrowContainer.UnfocusAll();

            _typewritePresenter.StartTypewriteAnimation();

            if (_currentArrowIndex >= _arrowContainer.ArrowControllerList.Count)
            {
                // 全ての矢印を処理した場合、再び生成状態へ遷移
                _objectResolver.Resolve<Common.GameState.GameStateMachine>().ChangeState(Common.GameState.GameStateType.Generating);
                Common.SE.SEManager.Instance.Play(Common.SE.SEName.Correct);
                return;
            }
            _arrowContainer.ArrowControllerList[_currentArrowIndex].Focus();
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
