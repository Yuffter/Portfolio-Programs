using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

namespace InGameScene {
    public class DecisionPercentButton : ButtonAnimationBase
    {
        [SerializeField, Header("ボタン")]
        private Button _button;

        /// <summary>
        /// ボタンがクリックされたときに通知されるObservable
        /// </summary>
        /// <returns></returns>
        public IObservable<Unit> OnClickAsObservable => _button.OnClickAsObservable();
    }
}