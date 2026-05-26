using DG.Tweening;
using TMPro;
using UnityEngine;

namespace MinutesGame.Common.Score
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _scoreText;
        private Sequence _scoreSequence;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = _scoreText.GetComponent<RectTransform>();
        }

        /// <summary>
        /// スコアのテキストを設定します
        /// </summary>
        /// <param name="score">表示するスコア</param>
        public void SetScoreText(int score)
        {
            _scoreText.text = $"Score: {score}";
            PlayAnimation();
        }

        private void PlayAnimation()
        {
            _scoreSequence?.Kill(true);
            _scoreSequence = DOTween.Sequence();
            _scoreSequence.Append(_rectTransform.DOPunchScale(new Vector3(0.2f, 0.2f, 0), 0.3f, 10, 1));
            _scoreSequence.Play();
        }
    }
}