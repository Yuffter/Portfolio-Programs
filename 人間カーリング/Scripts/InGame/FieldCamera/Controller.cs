using FieldCamera.State;
using R3;
using SO;
using Unity.Cinemachine;
using UnityEngine;

namespace FieldCamera
{
    public class Controller : MonoBehaviour
    {
        [SerializeField, Header("依存関係コンテナ")]
        private DependencyContainer _dependencyContainer;
        private StateMachine _stateMachine;

        void Start()
        {
            Initialize();
        }
        public void Initialize()
        {
            _dependencyContainer.Initialize();
            _stateMachine = new StateMachine(_dependencyContainer);
            _stateMachine.SetInitialState(new StateMachine.StateWaiting(_stateMachine));
        }

        void Update()
        {
            _stateMachine.Update();
        }

        /// <summary>
        /// Overview状態に遷移する
        /// </summary>
        public void ChangeOverviewState()
        {
            _stateMachine.ChangeState(FieldCameraState.Overview);
        }

        /// <summary>
        /// Waiting状態に遷移する
        /// </summary>
        public void ChangeWaitingState()
        {
            _stateMachine.ChangeState(FieldCameraState.Waiting);
        }
    }
}