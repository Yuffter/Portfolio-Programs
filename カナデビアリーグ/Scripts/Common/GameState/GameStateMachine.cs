using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace MinutesGame.Common.GameState
{
    public class GameStateMachine
    {
        private GameStateBase _currentState;

        private Dictionary<Type, GameStateBase> _stateMapping = new();
        
        [Inject]
        public GameStateMachine(IEnumerable<GameStateBase> allStates)
        {
            CreateStateMapping(allStates);
            foreach (var state in allStates)
            {
                Debug.Log($"登録されたステート: {state.GetType()}");
            }
            Debug.Log("ゲームステートマシンが初期化されました");
        }

        /// <summary>
        /// ステートの型とインスタンスのマッピングを作成します
        /// </summary>
        /// <param name="allStates"></param>
        private void CreateStateMapping(IEnumerable<GameStateBase> allStates)
        {
            foreach (var state in allStates)
            {
                var stateType = state.GetType();
                if (!_stateMapping.ContainsKey(stateType))
                {
                    _stateMapping[stateType] = state;
                    state.Initialize();
                }
                else
                {
                    Debug.LogError($"同じ型のステートが既に登録されています: {stateType}");
                }
            }
        }

        /// <summary>
        /// 初期ステートを設定します
        /// </summary>
        /// <param name="initialStateType">初期ステートの型</param>
        public void SetInitialState(Type initialStateType)
        {
            if (_stateMapping.TryGetValue(initialStateType, out var initialState))
            {
                _currentState = initialState;
                _currentState.Enter();
            }
            else
            {
                Debug.LogError($"初期ステートの型がマッピングに存在しません: {initialStateType}");
            }
        }

        /// <summary>
        /// ステートを変更します
        /// </summary>
        /// <param name="newState">新しいステート</param>
        public void ChangeState(GameStateType newState)
        {
            _currentState?.Exit();
            // 次のステートの型を取得し、マッピングからインスタンスを取得して現在のステートに設定
            Type t = _currentState.GetNextStateFromType(newState);
            _currentState = _stateMapping[t];
            _currentState?.Enter();
        }
        

        /// <summary>
        /// 現在のステートの毎フレーム処理を呼び出します
        /// </summary>
        public void Update()
        {
            _currentState?.Update();
            // Debug.Log("<color=red>ゲームステートマシンのUpdateが呼ばれました</color>");
        }
    }
}