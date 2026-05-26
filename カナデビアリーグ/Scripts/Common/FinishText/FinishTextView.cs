using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace MinutesGame.Common.FinishText
{
    public class FinishTextView : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _finishTextRectTransform;

        private void Awake()
        {
            _finishTextRectTransform.gameObject.SetActive(false);
        }

        public async UniTask ShowFinishTextAsync()
        {
            _finishTextRectTransform.gameObject.SetActive(true);
            await _finishTextRectTransform.DOShakeAnchorPos(1f, 30, 10).AsyncWaitForCompletion();
            await UniTask.Delay(TimeSpan.FromSeconds(1));
        }
    }
}