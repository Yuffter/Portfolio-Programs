using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using TMPro;

namespace InGameScene {
    public class NextQuestionButtonView : MonoBehaviour
    {
        [SerializeField, Header("ボタン")]
        private Button _button;

        /// <summary>
        /// ボタンがクリックされたときに通知されるObservable
        /// </summary>
        /// <returns></returns>
        public IObservable<Unit> OnClickAsObservable => _button.OnClickAsObservable();

        /// <summary>
        /// テキストの内容を変更する
        /// </summary>
        /// <param name="text"></param>
        public void ChangeText(string text) {
            _button.GetComponentInChildren<TextMeshProUGUI>().text = text;
        }
    }
}