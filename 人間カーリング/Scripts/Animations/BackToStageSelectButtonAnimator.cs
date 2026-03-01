using Core;
using DG.Tweening;
using R3;
using SO;
using UnityEngine;
using UnityEngine.UI;

namespace Animations 
{
    public class BackToStageSelectButtonAnimator : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;
        [SerializeField, Header("ボタンのRectTransform")]
        private RectTransform _rectTransform;
        Sequence _seq;

        private Subject<Unit> _onClickSubject = new Subject<Unit>();
        public Observable<Unit> OnClickAsObservable => _onClickSubject;

        private void Awake()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            GetComponent<Button>().OnClickAsObservable()
            .Subscribe(_ => {
                _onClickSubject?.OnNext(Unit.Default);
            }).AddTo(this);
        }

        public void Play()
        {
            _seq = DOTween.Sequence();
            _seq.Append(_canvasGroup.DOFade(1f, 0.5f).SetEase(Ease.Linear))
            .Join(_rectTransform.DOAnchorPosY(200,0.5f).From(new Vector2(0,160)))
            .AppendCallback(() =>
            {
                _canvasGroup.interactable = true;
                _canvasGroup.blocksRaycasts = true;
            });
        }
    }
}