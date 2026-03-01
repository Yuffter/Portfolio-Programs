using SO.Event;
using UnityEngine;

namespace SO.EventHub
{
    [CreateAssetMenu(menuName = "ScriptableObject/Create DescriptionViewerEvent")]
    public class DescriptionViewerEventHub : ScriptableObject
    {
        [SerializeField, Header("詳細ビュー有効化イベント")] private SimpleGameEvent _enableDescriptionViewerEvent;
        public SimpleGameEvent EnableDescriptionViewerEvent => _enableDescriptionViewerEvent;
        [SerializeField, Header("詳細ビュー無効化イベント")] private SimpleGameEvent _disableDescriptionViewerEvent;
        public SimpleGameEvent DisableDescriptionViewerEvent => _disableDescriptionViewerEvent;
    }
}
