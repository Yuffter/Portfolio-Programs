using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public abstract class StateMachineBase<T> where T : Enum
    {
        public StateBase _currentState { get; protected set; }
        private Dictionary<Type, StateBase> _states = new Dictionary<Type, StateBase>();
        private Dictionary<StateTransition, StateBase> _transitions = new Dictionary<StateTransition, StateBase>();

        /// <summary>
        /// 初期ステートを設定する
        /// </summary>
        /// <param name="initialState"></param>
        public void SetInitialState(StateBase initialState)
        {
            _currentState = initialState;
            _currentState?.Enter();
        }

        /// <summary>
        /// enumだけでステートを変更する
        /// </summary>
        /// <param name="condition"></param>
        public void ChangeState(T condition)
        {
            if (_currentState != null)
            {
                _currentState.Exit();
            }

            foreach (var transition in _transitions)
            {
                bool fromMatches = transition.Key.From == _currentState?.GetType();
                bool conditionMatches = transition.Key.Condition != null && 
                                      transition.Key.Condition.Equals(condition);

                if (fromMatches && conditionMatches)
                {
                    _currentState = transition.Value;
                    break;
                }
            }

            _currentState?.Enter();
        }

        public virtual void Update()
        {
            _currentState?.Update();
        }

        /// <summary>
        /// ステートを登録する
        /// </summary>
        /// <param name="state"></param>
        public virtual void RegisterState(StateBase state)
        {
            var type = state.GetType();
            if (!_states.ContainsKey(type))
            {
                _states[type] = state;
            }
        }

        /// <summary>
        /// ステート遷移を追加する
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="condition"></param>
        public virtual void AddTransition(Type from, Type to, T condition)
        {
            var transition = new StateTransition(from, to, condition);
            _transitions[transition] = _states[to];
        }
    }
}