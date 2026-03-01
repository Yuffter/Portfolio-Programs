using Common;
using UnityEngine;

namespace FieldCamera.State
{
    public partial class StateMachine
    {
        public class StateWaiting : StateBase
        {
            private readonly StateMachine _stateMachine;

            public StateWaiting(StateMachine stateMachine)
            {
                _stateMachine = stateMachine;
            }
        }
    }
}