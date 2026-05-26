using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;

namespace InGameScene {
    public class AirshipInitialAnimator : MonoBehaviour
    {
        [SerializeField, Header("気球提供クラス")]
        private AirshipsProvider _airshipsProvider;
        [SerializeField, Header("遷移後の気球のy座標")]
        private float _airshipPosY;
        
        /// <summary>
        /// 気球をアニメーションさせる
        /// </summary>
        /// <returns></returns>
        public async UniTask Animate() {
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
            foreach (var airship in _airshipsProvider.Airships) {
                airship.transform.DOMoveY(_airshipPosY, 2f).SetEase(Ease.OutBack).OnComplete(() => {
                    airship.GetComponent<AirshipFloatingAnimator>().PlayFloatingAnimation();
                }).SetLink(airship.gameObject);
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
            }
            await UniTask.Delay(TimeSpan.FromSeconds(2f));
        }
    }
}