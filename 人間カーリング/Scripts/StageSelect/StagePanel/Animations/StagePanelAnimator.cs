using DG.Tweening;
using UnityEngine;

namespace StageSelect.StagePanel.Animations 
{
    public class StagePanelAnimator : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _panelTransform;
        [SerializeField]
        private RectTransform _buttonTransform;
        Sequence _focusSeq;
        Sequence _moveSeq;
        private float _originalButtonWidth;
        private float _originalButtonHeight;

        private void Awake()
        {
            _originalButtonWidth = _buttonTransform.sizeDelta.x;
            _originalButtonHeight = _buttonTransform.sizeDelta.y;
            _buttonTransform.sizeDelta = new Vector2(0, _originalButtonHeight);
        }

        /// <summary>
        /// このステージパネルにフォーカスを当てる
        /// </summary>
        public void Focus()
        {
            _focusSeq?.Kill();
            _focusSeq = DOTween.Sequence();
            _focusSeq.Append(_panelTransform.DOScale(1.0f, 0.3f).SetEase(Ease.OutBack))
                .AppendInterval(0.5f)
                .Append(_buttonTransform.DOSizeDelta(new Vector2(_originalButtonWidth, _originalButtonHeight), 0.5f)).SetEase(Ease.OutCirc);
        }

        /// <summary>
        /// このステージパネルのフォーカスを外す
        /// </summary>
        public void Unfocus()
        {
            _focusSeq?.Kill();
            _focusSeq = DOTween.Sequence();
            _focusSeq.Append(_panelTransform.DOScale(0.4f, 0.3f).SetEase(Ease.OutBack))
                .Append(_buttonTransform.DOSizeDelta(new Vector2(0, _originalButtonHeight), 1));
        }

        public void MoveLeft() 
        {
            _moveSeq?.Kill(true);
            _moveSeq = DOTween.Sequence();
            _moveSeq.Append(_panelTransform.DOLocalMoveX(-2000f, 0.3f).SetEase(Ease.OutQuart).SetRelative());
        }

        public void MoveRight() 
        {
            _moveSeq?.Kill(true);
            _moveSeq = DOTween.Sequence();
            _moveSeq.Append(_panelTransform.DOLocalMoveX(2000f, 0.3f).SetEase(Ease.OutQuart).SetRelative());
        }
    }
}