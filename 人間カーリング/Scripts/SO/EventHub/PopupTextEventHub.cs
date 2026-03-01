using System;
using UnityEngine;
using SO.Event;

namespace SO.EventHub
{
    [CreateAssetMenu(menuName = "ScriptableObject/Create PopupTextEventHub")]
    public class PopupTextEventHub : ScriptableObject
    {
        [SerializeField, Header("ポップアップ表示要求イベント")] private GameEvent<PopupRequest> _popupRequestedEvent;
        public GameEvent<PopupRequest> PopupRequestedEvent => _popupRequestedEvent;
    }
}
