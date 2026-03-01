using UnityEngine;

namespace PopupText
{
    public class MainController : MonoBehaviour
    {
        [SerializeField] private SO.EventHub.PopupTextEventHub _popupTextEventHub;
        [SerializeField] private PopupPool _popupPool;
        void Awake()
        {
            _popupTextEventHub.PopupRequestedEvent.Subscribe(OnPopupTextRequested);
        }

        void OnDestroy()
        {
            _popupTextEventHub.PopupRequestedEvent.Unsubscribe(OnPopupTextRequested);
        }

        private void OnPopupTextRequested(SO.Event.PopupRequest request)
        {
            if (_popupPool == null) return;

            PopupAnimator popup = _popupPool.Get();
            if (popup == null) return;

            popup.OnAnimationComplete = () =>
            {
                if (_popupPool != null && popup != null)
                {
                    _popupPool.Release(popup);
                }
            };
            popup.AnimatePopup(request);
        }
    }
}
