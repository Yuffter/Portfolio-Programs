using System;
using UnityEngine;

namespace Common
{
    public struct StateTransition
    {
        public Type From;
        public Type To;
        public Enum Condition;
        
        public StateTransition(Type from, Type to, Enum condition)
        {
            From = from;
            To = to;
            Condition = condition;
        }
    }
}