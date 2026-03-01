using SO.Event;
using UnityEngine;

namespace SO.EventHub
{
    [CreateAssetMenu(menuName = "ScriptableObject/Create ConsumablesEvent")]
    public class ConsumablesEventHub : ScriptableObject
    {
        [SerializeField, Header("消耗品有効化イベント")] private SimpleGameEvent _enableConsumablesEvent;
        public SimpleGameEvent EnableConsumablesEvent => _enableConsumablesEvent;
        [SerializeField, Header("消耗品無効化イベント")] private SimpleGameEvent _disableConsumablesEvent;
        public SimpleGameEvent DisableConsumablesEvent => _disableConsumablesEvent;
        [SerializeField, Header("消耗品初期化イベント")] private InitializeConsumablesEvent _initializeConsumablesEvent;
        public InitializeConsumablesEvent InitializeConsumablesEvent => _initializeConsumablesEvent;
    }
}
