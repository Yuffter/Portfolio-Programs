using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Consumables.ItemSlot.QuantityText
{
    /// <summary>
    /// 消耗品の所持数表示View
    /// </summary>
    public class QuantityTextView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _quantityText;
        [SerializeField] private Image _notAvailableImage;
        [SerializeField] private RectTransform _notAvailableRectTransform;

        private void Awake()
        {
            _notAvailableImage.color = new Color(1, 1, 1, 0);
        }

        /// <summary>
        /// 所持数テキストを設定する
        /// </summary>
        /// <param name="quantity">新しい所持数</param>
        public void SetQuantityText(int quantity)
        {
            string text = $"x{quantity}";
            _quantityText.SetText(text);

            if (quantity == 0)
            {
                ShowNotAvailable();
            }
        }

        /// <summary>
        /// 所持数が0のときの表示を行う
        /// </summary>
        private void ShowNotAvailable()
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(_notAvailableImage.DOFade(1, 0.2f));
            seq.Append(_notAvailableRectTransform.DOScale(1f, 0.2f).From(2f).SetEase(Ease.OutElastic));
        }
    }
}
