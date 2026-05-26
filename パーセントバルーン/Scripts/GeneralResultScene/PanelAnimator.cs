using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using KanKikuchi.AudioManager;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace GeneralResultScene
{
    /// <summary>
    /// パネルアニメータークラス
    /// </summary>
    public class PanelAnimator
    {
        private readonly PanelAnimatorConfig _panelAnimatorConfig;
        private readonly ChromaticAberration _chromaticAberration;

        public PanelAnimator(PanelAnimatorConfig panelAnimatorConfig)
        {
            _panelAnimatorConfig = panelAnimatorConfig;
            _panelAnimatorConfig.Volume.profile.TryGet(out _chromaticAberration);
        }

        public async UniTask Animate()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1));

            /* 1位基準で中心に並べるために差分を計算する */
            float diff = _panelAnimatorConfig.PanelList[0].GetComponent<TMP_Text>().preferredWidth / 2f;
            for (int i = _panelAnimatorConfig.PanelList.Count-1;i >= 0;i--)
            {
                _panelAnimatorConfig.PanelList[i].transform.DOLocalMoveX(- diff, _panelAnimatorConfig.PanelThrowDuration).SetEase(Ease.OutBounce).SetLink(_panelAnimatorConfig.PanelList[i]);
                await UniTask.Delay(TimeSpan.FromSeconds(_panelAnimatorConfig.PanelThrowInterval));
            }

            SEManager.Instance.Play(SEPath.CLAP);
            for (int i = 0;i < 3;i++)
                GameObject.Instantiate(_panelAnimatorConfig.fireworkEffect, new Vector3(0,-8,5f), Quaternion.identity);
        }
    }

    [System.Serializable]
    public class PanelAnimatorConfig
    {
        [HideInInspector]
        public List<GameObject> PanelList;
        public float PanelThrowInterval;
        public float PanelThrowDuration;
        public Volume Volume;
        public float CameraFocusOnScale;
        public float CameraFocusOnDuration;
        public float CameraFocusOffDuration;
        public float TopTextFontSize;
        public float TopTextThrowDuration;
        public GameObject ExplosionEffect;
        public GameObject fireworkEffect;
        public float SmallTextFontSize;
        public float SmallTextDistance;
    }
}
