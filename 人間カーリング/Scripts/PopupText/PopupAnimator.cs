using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace PopupText
{
    public class PopupAnimator : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        public Action OnAnimationComplete;

        private Sequence _seq;
        void Awake()
        {
            _rectTransform.GetComponent<TMP_Text>().alpha = 0f;
        }

        void OnDisable()
        {
            // オブジェクトが非アクティブになる際にアニメーションをクリーンアップ
            _seq?.Kill();
            _seq = null;
        }

        void OnDestroy()
        {
            // オブジェクトが破棄される際にアニメーションをクリーンアップ
            _seq?.Kill();
            _seq = null;
        }

        public void AnimatePopup(SO.Event.PopupRequest request)
        {
            // 既存のアニメーションをクリーンアップ
            _seq?.Kill();

            // Debug.Log($"Popup Requested: Text={request.Text}, Position={request.Position}");
            Vector3 pos = Camera.main.WorldToScreenPoint(request.Position);
            float angle = Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;

            // if (pos.x < 100 ||)
            pos.x = Mathf.Clamp(pos.x, 100f, Screen.width - 100f);
            pos.y = Mathf.Clamp(pos.y, 100f, Screen.height - 100f);
            _rectTransform.position = pos;
            var tmpText = _rectTransform.GetComponent<TMP_Text>();
            tmpText.color = request.Color;
            _seq = DOTween.Sequence();
            _seq.AppendCallback(() =>
            {
                tmpText.text = request.Text;
                tmpText.alpha = 1f;
            })
            .Append(_rectTransform.DOMoveY(_rectTransform.position.y + 50f, 1f).SetEase(Ease.OutCubic))
            .Join(tmpText.DOFade(0f, 0.5f).SetDelay(0.5f))
            .AppendCallback(() =>
            {
                OnAnimationComplete?.Invoke();
                OnAnimationComplete = null;
            });
        }
    }
}
