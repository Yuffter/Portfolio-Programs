using UnityEngine;
using DG.Tweening;

namespace InGameScene {
    public class AirshipFloatingAnimator : MonoBehaviour
    {
        private GameObject _insideAirship;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _insideAirship = transform.Find("InsideAirship").gameObject;
        }

        /// <summary>
        /// 浮遊アニメーションを再生する
        /// </summary>
        public void PlayFloatingAnimation() {
            Sequence seq = DOTween.Sequence();
            seq.Append(_insideAirship.transform.DOLocalMoveY(0.1f, 1).SetEase(Ease.InOutSine).SetRelative());
            seq.Append(_insideAirship.transform.DOLocalMoveY(-0.1f, 1).SetEase(Ease.InOutSine).SetRelative());
            seq.SetLoops(-1);
            seq.SetLink(gameObject);
        }
    }
}