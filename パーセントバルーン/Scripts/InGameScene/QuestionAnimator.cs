using UnityEngine;
using TMPro;
using UniRx;
using DG.Tweening;
using KanKikuchi.AudioManager;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.UI;
using InGameScene.Interface;

namespace InGameScene {
    public class QuestionAnimator : MonoBehaviour, IInitializer
    {
        [SerializeField, Header("問題番号を表示するテキスト")]
        private TextMeshProUGUI _questionNumberingLabel;
        [SerializeField, Header("問題パネル")]
        private RectTransform _questionPanel;
        [SerializeField, Header("問題文を表示するテキスト")]
        private TMP_Text _questionLabel;
        private float _questionPanelDefaultScaleX;

        [SerializeField, Header("問題の画像を表示するImage")]
        private Image _questionImage;

        [SerializeField, Header("考える時間を表すプログレスサークル")]
        private Image _thinkingTimeCircle;

        [SerializeField, Header("タイマーテキスト")]
        private TextMeshProUGUI _timerText;

        private const float THINKING_TIME_CIRCLE_INITIAL_POS_Y = 120f;
        private const float THINKING_TIME_CIRCLE_DEFAULT_POS_Y = -120f;

        /// <summary>
        /// 指定された問題を出題する
        /// </summary>
        /// <returns></returns>
        public async UniTask AnimateNextQuestion() {
            await ShowQuestionNumbering();
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            await HideQuestionNumbering();
            await UniTask.Delay(TimeSpan.FromSeconds(1));

            await ShowQuestionPanel();
        }

        /// <summary>
        /// 問題番号を表示する
        /// </summary>
        /// <returns></returns>
        private async UniTask ShowQuestionNumbering()
        {
            SetQuestionComponent();
            _questionNumberingLabel.DOFade(0f,0f).SetLink(_questionNumberingLabel.gameObject);
            _questionNumberingLabel.transform.DOScaleX(0f,0f).SetLink(_questionNumberingLabel.gameObject);
            _questionNumberingLabel.DOFade(1f,0.3f).SetLink(_questionNumberingLabel.gameObject);
            _questionNumberingLabel.transform.DOScaleX(1f,0.3f).SetLink(_questionNumberingLabel.gameObject);
            SEManager.Instance.Play(SEPath.QUESTION_NUMBERING_APPEARANCE);
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f));
        }

        /// <summary>
        /// 問題番号を非表示にする
        /// </summary>
        /// <returns></returns>
        private async UniTask HideQuestionNumbering() {
            _questionNumberingLabel.DOFade(0f,0.3f).SetEase(Ease.Linear).SetLink(_questionNumberingLabel.gameObject);
            _questionNumberingLabel.transform.DOScaleX(0f,0.5f).SetEase(Ease.Linear).SetLink(_questionNumberingLabel.gameObject);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
        }

        /// <summary>
        /// 問題パネルとボタンを表示する
        /// </summary>
        /// <returns></returns>
        private async UniTask ShowQuestionPanel() {
            SEManager.Instance.Play(SEPath.QUESTION_APPEARANCE);
            await _questionPanel.DOScaleX(_questionPanelDefaultScaleX,0.5f).SetEase(Ease.OutBounce).AsyncWaitForCompletion();

            _thinkingTimeCircle.fillAmount = 1f;
            _timerText.text = ((int)GameData.Instance.MaxThinkingTime).ToString();
            await _thinkingTimeCircle.GetComponent<RectTransform>().DOAnchorPosY(THINKING_TIME_CIRCLE_DEFAULT_POS_Y,0.5f).SetEase(Ease.OutBack).SetLink(_thinkingTimeCircle.gameObject).AsyncWaitForCompletion();

            /* タイマーをアニメーションさせる */
            _thinkingTimeCircle.DOFillAmount(0f,GameData.Instance.MaxThinkingTime).SetEase(Ease.Linear).OnUpdate(() => {
                _timerText.text = $"{(int)Mathf.Ceil(_thinkingTimeCircle.fillAmount * GameData.Instance.MaxThinkingTime)}";
            }).SetLink(_thinkingTimeCircle.gameObject);
        }

        /// <summary>
        /// 問題パネルを非表示にする
        /// </summary>
        /// <returns></returns>
        public async UniTask HideQuestionPanel() {
            await _questionPanel.DOScaleX(0f,0.5f).SetEase(Ease.InBack).SetLink(_questionPanel.gameObject).AsyncWaitForCompletion();
        }

        /// <summary>
        /// タイマーを非表示にする
        /// </summary>
        /// <returns></returns>
        public async UniTask HideTimeCircle() {
            Sequence seq = DOTween.Sequence();
            seq.Append(_thinkingTimeCircle.GetComponent<RectTransform>().DOAnchorPosY(THINKING_TIME_CIRCLE_INITIAL_POS_Y, 0.5f).SetLink(_thinkingTimeCircle.gameObject));
            seq.AppendCallback(() => _thinkingTimeCircle.fillAmount = 1f);
            seq.SetLink(gameObject);
            await seq.AsyncWaitForCompletion();
        }

        public void Initialize()
        {
            _questionPanelDefaultScaleX = _questionPanel.localScale.x;
            _questionPanel.localScale = new Vector3(0f,_questionPanel.localScale.y,_questionPanel.localScale.z);

            _thinkingTimeCircle.GetComponent<RectTransform>().anchoredPosition = new Vector2(_thinkingTimeCircle.GetComponent<RectTransform>().anchoredPosition.x, THINKING_TIME_CIRCLE_INITIAL_POS_Y);
            _thinkingTimeCircle.fillAmount = 1f;
        }

        /// <summary>
        /// 問題番号と問題文をセットする
        /// </summary>
        private void SetQuestionComponent() {
            /* 問題番号を設定 */
            _questionNumberingLabel.text = $"第{GameData.Instance.CurrentQuestionNumber}問";

            /* 問題文を設定 */
            _questionLabel.text = GameData.Instance.CurrentQuestion.Question;

            /* 問題画像を設定 */
            _questionImage.sprite = GameData.Instance.CurrentQuestion.Sprite;
        }
    }
}