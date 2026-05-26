using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using UniRx;
using Cysharp.Threading.Tasks;

namespace TitleScene {
    public class StartButtonView : ButtonAnimationBase
    {
        [SerializeField]
        private Button _button;
        [SerializeField]
        private CanvasGroup _groupSettingCanvasGroup;

        public IObservable<Unit> OnClickAsObservable => _button.OnClickAsObservable();

        /// <summary>
        /// クリックされた時のアニメーションを再生する
        /// </summary>
        public void StartClickAnimation() {
            transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.2f, 10, 1).SetEase(Ease.InOutSine).SetLink(gameObject);
        }

        /// <summary>
        /// 班数の設定画面を表示する
        /// </summary>
        public async UniTask ShowGroupSetting() {
            await _groupSettingCanvasGroup.DOFade(1, 0.5f).SetEase(Ease.InOutSine).SetLink(gameObject).AsyncWaitForCompletion();

            _groupSettingCanvasGroup.blocksRaycasts = true;
            _groupSettingCanvasGroup.interactable = true;
        }
    }
}