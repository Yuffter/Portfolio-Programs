using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

namespace State
{
    public abstract class StateBase : IState, IDisposable
    {
        public virtual void OnEnter()
        {

        }
        public virtual void OnExit()
        {

        }
        public virtual void OnUpdate()
        {

        }

        public virtual void Dispose()
        {

        }
    }
}
