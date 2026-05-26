using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System;
using UniRx;
using UnityEngine.UI;
using KanKikuchi.AudioManager;

namespace TitleScene {
    public class DecisionButtonView : ButtonAnimationBase
    {
        [SerializeField]
        private Button _button;
        private IObservable<Unit> _onClickAsObservable => _button.OnClickAsObservable();
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _onClickAsObservable
                .First()
                .Subscribe(_ => {
                    PlayClickAnimation().Forget();
                    SEManager.Instance.Play(SEPath.DECISION_BUTTON);
                    BGMManager.Instance.FadeOut(1);
                }).AddTo(this);
        }

        private async UniTask PlayClickAnimation() {
            transform.DOKill(true);
            await transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.2f, 10, 1).SetEase(Ease.InOutSine).SetLink(gameObject).AsyncWaitForCompletion();
            await UniTask.Delay(TimeSpan.FromSeconds(1));
            SceneManager.LoadScene("InGameScene");
        }
    }
}