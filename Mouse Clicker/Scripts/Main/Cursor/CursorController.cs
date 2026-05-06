using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using KanKikuchi.AudioManager;
using Project.Main.Factory;
using Project.Main.GameSystems;
using Project.Main.GameSystems.Actors;
using Project.Main.GameSystems.Models;
using Project.Main.GameSystems.ScriptableObjects;
using R3;
using UnityEngine;
using VContainer;

namespace Project.Main.Cursor {
    public class CursorController : MonoBehaviour, ICursor
    {
        [Inject]
        private CursorSetting _cursorSetting;
        [Inject] 
        private BoxCollection _boxCollection;
        [Inject]
        private ICamera _camera;
        [Inject]
        private ScoreModel _scoreModel;
        [Inject]
        private MissTextFactory _missTextFactory;
        private Mover _mover;
        private Inputter _inputter;
        private CancellationTokenSource _cts;
        private CancellationToken _ct;
        private Vector2Int _currentPosition;

        /// <summary>
        /// 現在のカーソル位置
        /// </summary>
        public Vector2Int CurrentPosition => _currentPosition;

        public void Initialize(Vector2Int position)
        {
            Debug.Log("CursorController Initialize called");
            _currentPosition = position;

            // null チェック
            if (_cursorSetting == null)
            {
                Debug.LogError("CursorSetting is not injected!");
                return;
            }

            _cts = new CancellationTokenSource();
            _ct = CancellationTokenSource.CreateLinkedTokenSource(_cts.Token, this.GetCancellationTokenOnDestroy()).Token;
            _mover = new Mover(transform, _cursorSetting);
            _inputter = new Inputter();

            _inputter.StartHandler();

            _mover.Move(_currentPosition, Vector2Int.zero); // 初期位置にカーソルを設定

            // null チェックを追加
            if (_inputter.VerticalInput != null)
            {
                _inputter.VerticalInput
                .Subscribe(val =>
                {
                    _currentPosition = _mover.Move(_currentPosition, new Vector2Int(0, ((int)val)));
                }).AddTo(_ct);
            }

            if (_inputter.HorizontalInput != null)
            {
                _inputter.HorizontalInput.Subscribe(val =>
                {
                    _currentPosition = _mover.Move(_currentPosition, new Vector2Int(((int)val), 0));
                }).AddTo(_ct);
            }

            if (_inputter.PullOutInput != null)
            {
                _inputter.PullOutInput.Subscribe(_ =>
                {
                    // 引っ張り出し処理を実行
                    if (_boxCollection.GetBox(_currentPosition.x, _currentPosition.y).BoxType == BoxType.MouseSpawning)
                    {
                        _boxCollection.GetBox(_currentPosition.x, _currentPosition.y).PullOutAsync();
                        _camera.PlayPullOutAnimation();
                        if (UnityEngine.Random.Range(1, 3) == 1) SEManager.Instance.Play(SEPath.PULL_OUT_1);
                        else SEManager.Instance.Play(SEPath.PULL_OUT_2);
                        _scoreModel.AddScore(100);
                    }
                    // ギリギリ引けなかったとき
                    if (_boxCollection.GetBox(_currentPosition.x, _currentPosition.y).BoxType == BoxType.MouseDespawn)
                    {
                        _missTextFactory.CreateMissText(transform.position);
                    }
                }).AddTo(_ct);
            }
        }

        public void Stop()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _inputter?.Dispose();
        }
    }
}