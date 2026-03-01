using System;
using Coffee.UIEffects;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Animation
{
    public class TurnTextAnimator : MonoBehaviour
    {
        private TMP_Text _turnText;
        private UIEffect _uiEffect;
        private const float DURATION = 1;
        private const float END_VALUE = 0.3f;
        void Awake()
        {
            _turnText = GetComponent<TMP_Text>();
            _uiEffect = GetComponent<UIEffect>();
        }

        public async UniTask Show(int currentTurnCount = 0)
        {
            _turnText.SetText($"残り {currentTurnCount} ターン");
            DOTween.To(
                () => 1,
                x => _uiEffect.transitionRate = x,
                END_VALUE,
                DURATION
            ).SetEase(Ease.Linear).SetLink(gameObject);
        }

        public async UniTask Hide()
        {
            DOTween.To(
                () => END_VALUE,
                x => _uiEffect.transitionRate = x,
                1,
                DURATION
            ).SetEase(Ease.OutExpo).SetLink(gameObject);
            await UniTask.Delay(TimeSpan.FromSeconds(DURATION));
        }
    }
}