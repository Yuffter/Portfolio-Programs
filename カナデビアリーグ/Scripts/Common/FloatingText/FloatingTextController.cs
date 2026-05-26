using DG.Tweening;
using TMPro;
using UnityEngine;

namespace MinutesGame.Common.FloatingText
{
    public class FloatingTextController : MonoBehaviour
    {
        private SO.FloatingTextSetting _setting;
        private Sequence _floatingTextSequence;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void Initialize(SO.FloatingTextSetting setting)
        {
            _setting = setting;
        }

        /// <summary>
        /// 指定した位置にフローティングテキストを表示します
        /// </summary>
        /// <param name="message"></param>
        /// <param name="position">UI座標系での位置</param>
        public void ShowFloatingText(string message, Vector3 position)
        {
            _floatingTextSequence?.Kill(true);
            var textComponent = GetComponent<TMP_Text>();
            textComponent.text = message;
            textComponent.color = _setting.TextColor;
            textComponent.fontSize = _setting.FontSize;
            _rectTransform.anchoredPosition = position;
            textComponent.alpha = 0;

            _floatingTextSequence = DOTween.Sequence();
            _floatingTextSequence.Append(_rectTransform.DOAnchorPosY(position.y + _setting.FloatUpDistance, _setting.FloatUpDuration))
            .Join(textComponent.DOFade(1, _setting.FloatUpDuration))
            .Append(textComponent.DOFade(0, 0.2f))
            .OnComplete(() => Destroy(gameObject));
        }
    }
}