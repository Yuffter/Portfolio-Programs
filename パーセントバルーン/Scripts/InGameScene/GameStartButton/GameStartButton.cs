using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx;
using System;
using DG.Tweening;
using KanKikuchi.AudioManager;

namespace InGameScene {
    public class GameStartButton : ButtonAnimationBase
    {
        [SerializeField, Header("ボタン")]
        private Button _button;

        public IObservable<Unit> OnClickAsObservable => _button.OnClickAsObservable();

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            OnClickAsObservable
                .First()
                .Subscribe(_ => {
                    SEManager.Instance.Play(SEPath.DECISION_BUTTON);
                    transform.parent.DOScaleX(0, 0.5f);
                }).AddTo(this); 
        }
    }
}
