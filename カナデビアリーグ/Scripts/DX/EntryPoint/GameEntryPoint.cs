using UnityEngine;
using VContainer;
using VContainer.Unity;
namespace MinutesGame.DX.EntryPoint
{
    public class GameEntryPoint : ITickable
    {
        [Inject]
        private readonly Common.GameState.GameStateMachine _gameStateMachine;
        private bool _isStarted = false;

        /// <summary>
        /// MiniGameSystemFacadeから呼ばれます
        /// </summary>
        public void Initialize()
        {
            if (_isStarted) return;

            _isStarted = true;

            // ゲーム開始ステートに遷移
            _gameStateMachine.SetInitialState(typeof(GameState.States.GameStartState));
        }

        public void Tick()
        {
            if (!_isStarted) return;

            // 毎フレームステートマシンを更新
            _gameStateMachine.Update();
        }
    }
}