using System;
using System.Collections.Generic;
using UnityEngine;

namespace MinutesGame.Common.GameState
{
    public abstract class GameStateBase : IGameState
    {
        private Dictionary<GameStateType, Type> _stateTypeMapping = new();

        /// <summary>
        /// 遷移の作成等の初期化を行います<br/>
        /// AddTransitionメソッドを使用して遷移先を登録してください
        /// </summary>
        public abstract void Initialize();
        /// <summary>
        /// ステートの遷移先を登録します
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stateType"></param>
        protected void AddTransition<T>(GameStateType stateType) where T : GameStateBase
        {
            _stateTypeMapping[stateType] = typeof(T);
        }
        public virtual void Enter()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Exit()
        {
        }

        /// <summary>
        /// 指定したステートタイプに遷移するステートの型を取得します<br/>
        /// 登録されていない場合はnullを返します<br/>
        /// この関数はGameStateMachineから使用されます
        /// </summary>
        /// <param name="stateType"></param>
        /// <returns></returns>
        public Type GetNextStateFromType(GameStateType stateType)
        {
            if (_stateTypeMapping.TryGetValue(stateType, out var nextStateType))
            {
                return nextStateType;
            }
            else
            {
                Debug.LogError($"遷移先のステートが登録されていません: {stateType}");
                return null;
            }
        }
    }
}