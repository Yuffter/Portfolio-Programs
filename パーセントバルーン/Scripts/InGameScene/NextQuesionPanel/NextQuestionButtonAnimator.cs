using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using KanKikuchi.AudioManager;

namespace InGameScene {
    public class NextQuestionButtonAnimator : MonoBehaviour
    {
        [SerializeField, Header("パネルのCanvasGroup")]
        private CanvasGroup _panelCanvasGroup;

        [SerializeField, Header("パネルのImage")]
        private Image _panelImage;

        [SerializeField, Header("ボタン")]
        private Button _button;

        [SerializeField, Header("問題アニメーター")]
        private QuestionAnimator _questionAnimator;
        
        /// <summary>
        /// 初期化する
        /// </summary>
        public void Initialize() {
            _panelCanvasGroup.blocksRaycasts = false;
            _panelCanvasGroup.interactable = false;
            _panelImage.fillAmount = 0f;

            var buttonAnimator = _button.gameObject.AddComponent<ButtonAnimationBase>();
            buttonAnimator.SetSizeDelta(0.1f);
        }

        /// <summary>
        /// 次の問題に進むボタンを表示する
        /// </summary>
        /// <returns></returns>
        public async UniTask ShowNextButton() {
            BGMManager.Instance.Play(BGMPath.IN_GAME_BACKGROUND, 0.6f);
            await _questionAnimator.HideQuestionPanel();
            await _panelImage.DOFillAmount(1, 1).SetLink(_panelImage.gameObject).AsyncWaitForCompletion();
            _panelCanvasGroup.blocksRaycasts = true;
            _panelCanvasGroup.interactable = true;
        }

        /// <summary>
        /// 次の問題に進むボタンを非表示にする
        /// </summary>
        /// <returns></returns>
        public async UniTask HideNextButton() {
            _panelCanvasGroup.blocksRaycasts = false;
            _panelCanvasGroup.interactable = false;
            _button.transform.DOKill(true);
            await _button.transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.2f, 10, 1).SetEase(Ease.InOutSine).SetLink(_button.gameObject).AsyncWaitForCompletion();

            await _panelImage.DOFillAmount(0, 1).SetLink(_panelImage.gameObject).AsyncWaitForCompletion();
        }
    }
}