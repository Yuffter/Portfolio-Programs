using System;
using NUnit.Framework;
using UnityEngine;

namespace SO.Event
{
    public abstract class GameEvent<T> : ScriptableObject
    {
        [SerializeField, Header("このイベントの説明")] private string _eventDescription;
        /// <summary>
        /// このイベントの説明
        /// </summary>
        public string EventDescription => _eventDescription;

        private event Action<T> _onRaise;
        public void Raise(T arg)
        {
            _onRaise?.Invoke(arg);
        }

        public void Subscribe(Action<T> action)
        {
            _onRaise += action;
        }

        public void Unsubscribe(Action<T> action)
        {
            _onRaise -= action;
        }
    }

    public abstract class GameEvent : ScriptableObject
    {
        [SerializeField, Header("このイベントの説明")] private string _eventDescription;
        /// <summary>
        /// このイベントの説明
        /// </summary>
        public string EventDescription => _eventDescription;

        private event Action _onRaise;
        public void Raise()
        {
            _onRaise?.Invoke();
        }

        public void Subscribe(Action action)
        {
            _onRaise += action;
        }

        public void Unsubscribe(Action action)
        {
            _onRaise -= action;
        }
    }
}