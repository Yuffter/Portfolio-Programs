using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Guide
{
    public class GuideAnimator : MonoBehaviour
    {
        [SerializeField] private RectTransform _guidePanel;
        [SerializeField] private TMP_Text _guideText;
        [SerializeField] private SO.GuideTextDatabase _guideTextDatabase;
        [SerializeField] private RectTransform _videoPanel;

        private Sequence _sequence;

        void Awake()
        {
            _guidePanel.anchoredPosition = Vector2.zero;
            _videoPanel.GetComponent<RawImage>().color = new Color(1, 1, 1, 0);
        }

        public async UniTask ShowGuidePanel()
        {
            _guideText.text = _guideTextDatabase.OverViewText;
            _sequence?.Kill(true);
            _sequence = DOTween.Sequence();
            await _sequence
                .Append(_videoPanel.DOAnchorPosY(50, 0.5f))
                .Join(_videoPanel.GetComponent<RawImage>().DOFade(0,0.5f).SetEase(Ease.OutCubic))
                .Join(_guidePanel.DOAnchorPosY(-120, 0.5f).From(new Vector2(0, 0)).SetEase(Ease.OutBack)).AsyncWaitForCompletion();
        }

        public async UniTask ShowFocusPanel()
        {
            _guideText.text = _guideTextDatabase.FocusText;
            _sequence?.Kill(true);
            _sequence = DOTween.Sequence();
            await _sequence
                .Append(_guidePanel.DOAnchorPosY(-120, 0.5f).From(new Vector2(0, 0)).SetEase(Ease.OutBack))
                .Append(_videoPanel.DOAnchorPosY(0, 0.2f))
                .Join(_videoPanel.GetComponent<RawImage>().DOFade(1,0.2f)).AsyncWaitForCompletion();
        }

        public async UniTask HideGuidePanel()
        {
            _sequence?.Kill(true);
            _sequence = DOTween.Sequence();
            await _sequence
                .Append(_guidePanel.DOAnchorPosY(0, 0.5f).SetEase(Ease.InBack))
                .Join(_videoPanel.GetComponent<RawImage>().DOFade(0,0.5f).SetEase(Ease.OutCubic)).AsyncWaitForCompletion();
        }
    }
}