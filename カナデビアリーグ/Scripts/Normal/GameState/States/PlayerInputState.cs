using UnityEngine;
using VContainer;
using MinutesGame.Common.Arrow;
using MinutesGame.Common.Input;
using UniRx;
using System;
using MinutesGame.Common.Score;
using MinutesGame.Common.Timer;
using MinutesGame.Common.Typewrite;
using MinutesGame.Common.Factory;

namespace MinutesGame.Normal.GameState.States
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
        private Common.Factory.FloatingTextFactory _floatingTextFactory;
        private const string SCORE_OBJ_NAME = "Score_key";
        private int _currentArrowIndex;
        private CompositeDisposable _disposables = new CompositeDisposable();
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
            // プレイヤー入力の購読を開始
            _input.OnDownKeyPressed
            .Throttle(TimeSpan.FromSeconds(0.005f))  // Joyconlibの連続入力対策として、購読を0.005秒間隔に制限
            .Subscribe(_ =>
            {
                if (_arrowContainer.ArrowControllerQueue.Peek().ArrowType == ArrowType.Down)
                {
                    FocusNextArrow();
                }
                else
                {
                    MissArrow();
                }
            }).AddTo(_disposables);

            _input.OnUpKeyPressed
            .Throttle(TimeSpan.FromSeconds(0.005f))
            .Subscribe(_ =>
            {
                if (_arrowContainer.ArrowControllerQueue.Peek().ArrowType == ArrowType.Up)
                {
                    FocusNextArrow();
                }
                else
                {
                    MissArrow();
                }
            }).AddTo(_disposables);

            _input.OnLeftKeyPressed
            .Throttle(TimeSpan.FromSeconds(0.005f))
            .Subscribe(_ =>
            {
                if (_arrowContainer.ArrowControllerQueue.Peek().ArrowType == ArrowType.Left)
                {
                    FocusNextArrow();
                }
                else
                {
                    MissArrow();
                }
            }).AddTo(_disposables);

            _input.OnRightKeyPressed
            .Throttle(TimeSpan.FromSeconds(0.005f))
            .Subscribe(_ =>
            {
                if (_arrowContainer.ArrowControllerQueue.Peek().ArrowType == ArrowType.Right)
                {
                    FocusNextArrow();
                }
                else
                {
                    MissArrow();
                }
            }).AddTo(_disposables);

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
            // プレイヤー入力の処理をここに追加
        }

        public override void Exit()
        {
            _disposables.Dispose();
            _input.DisableInput();
        }

        private void FocusNextArrow()
        {
            _scorePresenter.AddScore(_gameSetting.ScorePerHitNormal);
            Common.SE.SEManager.Instance.Play(Common.SE.SEName.Correct);

            Common.FloatingText.FloatingTextController floatingTextController = _floatingTextFactory.Create();
            Vector3 position = GameObject.Find(SCORE_OBJ_NAME).GetComponent<RectTransform>().localPosition;
            floatingTextController.ShowFloatingText($"+ {_gameSetting.ScorePerHitNormal}", position + new Vector3(-100 + UnityEngine.Random.Range(-50f, 50f), 0, 0));

            // 入力待ちキューから押されたキーを削除して詰める
            _arrowContainer.ArrowControllerQueue.Dequeue();
            _currentArrowIndex++;
            _arrowContainer.UnfocusAll();

            _typewritePresenter.StartTypewriteAnimation();

            if (_currentArrowIndex >= _arrowContainer.ArrowControllerList.Count)
            {
                // 全ての矢印を処理した場合、再び生成状態へ遷移
                _objectResolver.Resolve<Common.GameState.GameStateMachine>().ChangeState(Common.GameState.GameStateType.Generating);
                return;
            }
            _arrowContainer.ArrowControllerList[_currentArrowIndex].Focus();
        }

        private void MissArrow()
        {
            _arrowContainer.ArrowControllerList[_currentArrowIndex].Miss();
            Common.SE.SEManager.Instance.Play(Common.SE.SEName.Incorrect);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
