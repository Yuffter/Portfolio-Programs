using System;
using R3;
using SO.Event;
using UnityEngine;

namespace SO.EventHub
{
    [CreateAssetMenu(menuName = "ScriptableObject/Create GuideEvent")]
    public class GuideEventHub : ScriptableObject
    {
        [SerializeField, Header("俯瞰視点イベント")] private SimpleGameEvent _overviewEvent;
        public SimpleGameEvent OverviewEvent => _overviewEvent;
        [SerializeField, Header("フォーカスイベント")] private SimpleGameEvent _focusEvent;
        public SimpleGameEvent FocusEvent => _focusEvent;
        [SerializeField, Header("弾きイベント")] private SimpleGameEvent _flickedEvent;
        public SimpleGameEvent FlickedEvent => _flickedEvent;
    }
}