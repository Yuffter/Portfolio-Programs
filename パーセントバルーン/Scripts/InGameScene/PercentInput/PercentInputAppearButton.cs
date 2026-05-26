using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using KanKikuchi.AudioManager;

namespace InGameScene {
    public class PercentInputAppearButton : ButtonAnimationBase, IPointerClickHandler
    {
        private PercentInputPanelAnimator _percentInputPanelAnimator;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _percentInputPanelAnimator = FindObjectOfType<PercentInputPanelAnimator>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            PlayClickAnimation().Forget();
        }

        /// <summary>
        /// クリック時のアニメーションを再生する
        /// </summary>
        /// <returns></returns>
        private async UniTask PlayClickAnimation() {
            transform.DOKill(true);
            SEManager.Instance.Play(SEPath.DECISION_BUTTON);
            await transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0.1f), 0.2f, 10, 1).SetEase(Ease.InOutSine).SetLink(gameObject).AsyncWaitForCompletion();
            await _percentInputPanelAnimator.ShowPercentInputPanel();
        }
    }
}