using UnityEngine;
using InGameScene.Interface;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using KanKikuchi.AudioManager;
using System;
using TMPro;
using UniRx;

namespace InGameScene {
    public class PercentGaugeAnimator : MonoBehaviour, IInitializer
    {
        [SerializeField, Header("ゲージのCanvasGroup")]
        private CanvasGroup _gaugeCanvasGroup;

        [SerializeField, Header("ゲージのImage")]
        private Image _gaugeImage;

        [SerializeField, Header("パーセントを表示するバルーン")]
        private GameObject _percentBalloon;

        [SerializeField, Header("パーセントを表示するテキスト")]
        private TMP_Text _percentText;

        private const float PERCENT_BALLOON_POSITION = 145;
        private const float PERCENT_BALLOON_INITIAL_POSITION = -200;
        public void Initialize()
        {
            /* ゲージを非表示にする */
            _gaugeCanvasGroup.alpha = 0;
            _gaugeCanvasGroup.blocksRaycasts = false;
            _gaugeCanvasGroup.interactable = false;
            _gaugeImage.fillAmount = 0;

            /* パーセントを表示するバルーンを画面外に配置する */
            _percentBalloon.GetComponent<RectTransform>().anchoredPosition = new Vector2(-130, PERCENT_BALLOON_INITIAL_POSITION);
        }

        /// <summary>
        /// パーセントバーをアニメーションさせる
        /// </summary>
        /// <param name="percent"></param>
        /// <returns></returns>
        public async UniTask Animate(float percent) {
            _gaugeCanvasGroup.alpha = 1;
            _gaugeCanvasGroup.blocksRaycasts = true;
            _gaugeCanvasGroup.interactable = true;
            _percentBalloon.GetComponent<RectTransform>().DOAnchorPosY(PERCENT_BALLOON_POSITION, 0.5f).SetEase(Ease.OutBack);

            await UniTask.Delay(TimeSpan.FromSeconds(1));

            /* 一度パーセントを100に持っていく */
            Tween gaugeTween = _gaugeImage.DOFillAmount(1, 5);
            await gaugeTween.OnUpdate(() => {
                SEManager.Instance.Play(SEPath.GAUGE_WAVE, 1, 0, 0.5f + 1.5f * gaugeTween.ElapsedPercentage());

                /* パーセントをテキスト表示する */
                _percentText.text = $"{(int)(_gaugeImage.fillAmount * 100)}";
            }).AsyncWaitForCompletion();

            /* パーセントをランダムな位置にもってくる */
            gaugeTween = _gaugeImage.DOFillAmount(UnityEngine.Random.Range(0f, 1f), 6).SetEase(Ease.OutQuint);
            gaugeTween.OnUpdate(() => {
                if (gaugeTween.ElapsedPercentage() < 0.7f)
                SEManager.Instance.Play(SEPath.GAUGE_WAVE, 1, 0, 0.5f + 1.5f * (1-gaugeTween.ElapsedPercentage()));

                /* パーセントをテキスト表示する */
                _percentText.text = $"{(int)(_gaugeImage.fillAmount * 100)}";
            });
            await UniTask.Delay(TimeSpan.FromSeconds(5.5f));
            gaugeTween.Kill();
            SEManager.Instance.Play(SEPath.STOP_GAUGE);
            BGMManager.Instance.Stop();

            gaugeTween = _gaugeImage.DOFillAmount(percent / 100f, 0.05f).SetEase(Ease.Linear);
            await gaugeTween.OnUpdate(() => {
                /* パーセントをテキスト表示する */
                _percentText.text = $"{(int)Mathf.Ceil(_gaugeImage.fillAmount * 100)}";
            }).AsyncWaitForCompletion();
        }

        /// <summary>
        /// ゲージを非表示にする
        /// </summary>
        /// <returns></returns>
        public async UniTask HideGauge() {
            Sequence seq = DOTween.Sequence();
            seq.Append(_gaugeCanvasGroup.DOFade(0,0.5f).SetLink(_gaugeCanvasGroup.gameObject));
            seq.Join(_percentBalloon.GetComponent<RectTransform>().DOAnchorPosY(PERCENT_BALLOON_INITIAL_POSITION, 0.5f).SetLink(_percentBalloon.gameObject));
            seq.AppendCallback(() => {
                _gaugeCanvasGroup.blocksRaycasts = false;
                _gaugeCanvasGroup.interactable = false;
                _gaugeImage.fillAmount = 0f;
                _percentText.text = "0";
            });
            seq.SetLink(gameObject);
            await seq.AsyncWaitForCompletion();
        }
    }
}