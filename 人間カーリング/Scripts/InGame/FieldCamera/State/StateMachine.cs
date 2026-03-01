using Common;
using JetBrains.Annotations;
using UnityEngine;

namespace FieldCamera.State
{
    public enum FieldCameraState
    {
        Overview,
        FocusObject,
        Waiting
    }
    public partial class StateMachine : StateMachineBase<FieldCameraState>
    {
        public DependencyContainer DependencyContainer { get; private set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dependencyContainer">依存関係を管理するコンテナ</param>
        public StateMachine(DependencyContainer dependencyContainer)
        {
            DependencyContainer = dependencyContainer;

            RegisterState(new StateOverview(this));
            RegisterState(new StateFocusObject(this));
            RegisterState(new StateWaiting(this));
            
            AddTransition(typeof(StateOverview), typeof(StateFocusObject), FieldCameraState.FocusObject);
            AddTransition(typeof(StateFocusObject), typeof(StateOverview), FieldCameraState.Overview);
            AddTransition(typeof(StateFocusObject), typeof(StateWaiting), FieldCameraState.Waiting);
            AddTransition(typeof(StateWaiting), typeof(StateOverview), FieldCameraState.Overview);
        }
    }
}