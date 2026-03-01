using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Guide
{
    /// <summary>
    /// インゲームの操作説明を制御するクラス
    /// </summary>
    public class GuideController : MonoBehaviour
    {
        [SerializeField] private SO.EventHub.GuideEventHub _guideEventHub;
        [SerializeField] private GuideAnimator _guideAnimator;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            SetEvents();
        }

        private void SetEvents()
        {
            _guideEventHub.OverviewEvent.Subscribe(OnOverview);
            _guideEventHub.FocusEvent.Subscribe(OnFocus);
            _guideEventHub.FlickedEvent.Subscribe(OnFlicked);
        }

        private void OnOverview()
        {
            _guideAnimator.ShowGuidePanel().Forget();
        }

        private void OnFocus()
        {
            _guideAnimator.ShowFocusPanel().Forget();
        }

        private void OnFlicked()
        {
            _guideAnimator.HideGuidePanel().Forget();
        }

        private void OnDestroy()
        {
            _guideEventHub.OverviewEvent.Unsubscribe(OnOverview);
            _guideEventHub.FocusEvent.Unsubscribe(OnFocus);
            _guideEventHub.FlickedEvent.Unsubscribe(OnFlicked);
        }
    }
}