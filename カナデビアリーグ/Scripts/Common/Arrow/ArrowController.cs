using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


namespace MinutesGame.Common.Arrow
{
    public enum ArrowType
    {
        Up,
        Down,
        Left,
        Right
    }
    public class ArrowController : MonoBehaviour
    {
        private Image _arrowImage;
        /// <summary>
        /// 矢印のイメージコンポーネント
        /// </summary>
        public Image ArrowImage => _arrowImage;

        private Image _backgroundImage;
        private ArrowType _arrowType;
        public ArrowType ArrowType => _arrowType;

        private readonly Color ORIGINAL_COLOR = Color.white;
        private readonly Color FOCUS_COLOR = Color.yellow;

        private RectTransform _rectTransform;

        private Sequence _missSequence;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _arrowImage = transform.GetChild(0).GetComponent<Image>();
            _backgroundImage = GetComponent<Image>();
            _rectTransform = GetComponent<RectTransform>();
        }

        /// <summary>
        /// 矢印のスプライトを設定します
        /// </summary>
        /// <param name="sprite">矢印のスプライト</param>
        /// <param name="arrowType">矢印のタイプ</param>
        public void SetArrowSpriteAndType(Sprite sprite, ArrowType arrowType)
        {
            _arrowImage.sprite = sprite;
            _arrowType = arrowType;
        }

        /// <summary>
        /// 矢印をフォーカス状態にします
        /// </summary>
        public void Focus()
        {
            _missSequence?.Kill(true);
            _backgroundImage.color = FOCUS_COLOR;
            _rectTransform.DOScale(1.1f, 0.1f).SetEase(Ease.OutElastic);
        }

        /// <summary>
        /// 矢印のフォーカスを解除します
        /// </summary>
        public void Unfocus()
        {
            _missSequence?.Kill(true);
            _backgroundImage.color = ORIGINAL_COLOR;
            _rectTransform.DOScale(1.0f, 0.1f).SetEase(Ease.OutElastic);
        }

        public void Miss()
        {
            _missSequence?.Kill(true);
            _missSequence = DOTween.Sequence();
            _missSequence.AppendCallback(() => _backgroundImage.color = Color.red);
            _missSequence.Append(_rectTransform.DOShakePosition(0.5f, new Vector2(10f, 0), 10, 90, false, true));
            _missSequence.AppendCallback(() => _backgroundImage.color = FOCUS_COLOR);
            _missSequence.Play();
        }

        /// <summary>
        /// 矢印を画面内に投げ入れます
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public async UniTask ThrowInAsync(float from, float to)
        {
            await _rectTransform.DOAnchorPosX(to, 0.5f).From(new Vector2(from, 0)).SetEase(Ease.OutCubic);
        }
    }
}