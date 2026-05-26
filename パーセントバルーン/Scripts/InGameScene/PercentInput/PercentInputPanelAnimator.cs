using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using InGameScene.Interface;
using TMPro;

namespace InGameScene {
    public class PercentInputPanelAnimator : MonoBehaviour, IInitializer
    {
        [SerializeField, Header("パーセント入力パネルのCanvasGroup")]
        private CanvasGroup _percentInputPanelCanvasGroup;

        /// <summary>
        /// パーセント入力画面を表示する
        /// </summary>
        public async UniTask ShowPercentInputPanel() {
            /* 前の入力をリセットする */
            Transform percentInputs = _percentInputPanelCanvasGroup.transform.Find("PercentInputs");
            foreach (Transform child in percentInputs) {
                child.GetComponentInChildren<TMP_InputField>().text = "";
            }

            await _percentInputPanelCanvasGroup.DOFade(1, 0.3f).SetLink(_percentInputPanelCanvasGroup.gameObject).AsyncWaitForCompletion();

            /* 入力を有効にする */
            _percentInputPanelCanvasGroup.blocksRaycasts = true;
            _percentInputPanelCanvasGroup.interactable = true;
        }

        /// <summary>
        /// パーセント入力画面を非表示にする
        /// </summary>
        public async UniTask HidePercentInputPanel() {
            /* 入力を無効にする */
            _percentInputPanelCanvasGroup.blocksRaycasts = false;
            _percentInputPanelCanvasGroup.interactable = false;


            await _percentInputPanelCanvasGroup.DOFade(0, 0.3f).SetLink(_percentInputPanelCanvasGroup.gameObject).AsyncWaitForCompletion();
        }

        public void Initialize()
        {
            _percentInputPanelCanvasGroup.alpha = 0;
            _percentInputPanelCanvasGroup.blocksRaycasts = false;
            _percentInputPanelCanvasGroup.interactable = false;
        }
    }
}