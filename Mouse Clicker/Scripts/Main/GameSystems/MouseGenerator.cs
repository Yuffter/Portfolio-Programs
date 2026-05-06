using System.Collections.Generic;
using System.Linq;
using Project.Main.GameSystems.Actors;
using Project.Main.GameSystems.ScriptableObjects;
using R3;
using UnityEngine;

namespace Project.Main.GameSystems
{
    public class MouseGenerator
    {
        private List<IBox> _boxes;
        private GameRule _gameRule;
        private float _currentTime = 0f;
        private float _lastSpawnTime = 0f;
        private CompositeDisposable _updateDisposable = new CompositeDisposable();

        public MouseGenerator(List<IBox> boxes, GameRule gameRule)
        {
            _boxes = boxes;
            _gameRule = gameRule;
        }

        /// <summary>
        /// マウスの生成を開始する
        /// </summary>
        public void StartGenerating()
        {
            Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    _currentTime += Time.deltaTime;
                    
                    // 現在の時間の進行率を計算（0.0～1.0）
                    float timeProgress = _currentTime / _gameRule.TimeLimit;
                    
                    // AnimationCurveから現在の生成間隔倍率を取得
                    float spawnRateMultiplier = _gameRule.MouseSpawnRateCurve.Evaluate(timeProgress);
                    
                    // 実際の生成間隔を計算（倍率が高いほど間隔が短くなる）
                    float currentSpawnInterval = _gameRule.InitialMouseSpawnRate / spawnRateMultiplier;
                    
                    // 前回の生成から十分時間が経過したらマウスを生成
                    if (_currentTime - _lastSpawnTime >= currentSpawnInterval)
                    {
                        SpawnMouse();
                        _lastSpawnTime = _currentTime;
                    }
                }).AddTo(_updateDisposable);
        }

        public void StopGenerating()
        {
            _updateDisposable.Dispose();
        }

        /// <summary>
        /// マウスを生成する
        /// </summary>
        private async void SpawnMouse()
        {
            // 空いているボックス（Normal状態）を見つけてマウスを生成
            var availableBoxes = _boxes.Where(box =>
            {
                if (box is Project.Main.Box.BoxController boxController)
                {
                    return boxController.BoxType == Project.Main.GameSystems.Actors.BoxType.Normal;
                }
                return false;
            }).ToList();

            if (availableBoxes.Count > 0)
            {
                var randomBox = availableBoxes[UnityEngine.Random.Range(0, availableBoxes.Count)];
                await randomBox.SpawnMouseAsync();
            }
        }
    }
}