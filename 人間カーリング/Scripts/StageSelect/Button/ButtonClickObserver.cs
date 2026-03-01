using AudioManager.SE;
using DG.Tweening;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;

namespace StageSelect.Button
{
    /// <summary>
    /// ボタンのクリックを発火するクラス
    /// </summary>
    public class ButtonClickObserver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        Sequence _seq;
        private UnityEngine.UI.Button _button;
        private Subject<Unit> _onClickSubject = new Subject<Unit>();

        private float _focusedScale = 1.2f;
        private float _unfocusedScale = 1.0f;

        private void Awake()
        {
            // フォーカス状態のスケールを計算
            _unfocusedScale = transform.localScale.x;
            _focusedScale = _unfocusedScale * 1.2f;

            _button = GetComponent<UnityEngine.UI.Button>();
        }
        /// <summary>
        /// クリックされたときに発火する
        /// </summary>
        public Observable<Unit> OnClick => _onClickSubject;
        public void OnPointerEnter(PointerEventData eventData)
        {
            _seq?.Kill();
            _seq = DOTween.Sequence();
            _seq.Append(transform.DOScale(_focusedScale, 0.1f));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _seq?.Kill();
            _seq = DOTween.Sequence();
            _seq.Append(transform.DOScale(_unfocusedScale, 0.1f));
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_button.interactable == false)
            {
                _seq?.Kill();
                _seq = DOTween.Sequence();
                _seq.Append(GetComponent<RectTransform>().DOShakePosition(0.2f, 10f, 20));
                SEManager.Instance.Play(SEName.CannotClick);
            }
            else
            {
                _seq?.Kill();
                _seq = DOTween.Sequence();
                _seq.Append(transform.DOPunchScale(Vector3.one * 0.3f, 0.2f, 10, 1));
                _onClickSubject.OnNext(Unit.Default);
            }
        }
    }
}
