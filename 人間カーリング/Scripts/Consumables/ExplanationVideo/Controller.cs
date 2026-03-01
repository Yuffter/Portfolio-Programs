using System;
using UnityEngine;

namespace Consumables.ExplanationVideo
{
    /// <summary>
    /// 説明動画の制御クラス
    /// </summary>
    public class Controller : MonoBehaviour
    {
        [SerializeField, Header("説明動画のUI")] private CanvasGroup _explanationVideoUI;
        public event Action OnShowEvent;
        public event Action OnHideEvent;

        private void Awake()
        {
            SetEvents();
        }

        private void Start()
        {
            _explanationVideoUI.alpha = 0f;
        }

        private void SetEvents()
        {
            OnShowEvent += Show;
            OnHideEvent += Hide;
        }

        private void Show()
        {
            _explanationVideoUI.alpha = 1f;
        }

        private void Hide()
        {
            _explanationVideoUI.alpha = 0f;
        }

        public void NotifyShow()
        {
            Debug.Log("説明動画を表示");
            OnShowEvent?.Invoke();
        }

        public void NotifyHide()
        {
            OnHideEvent?.Invoke();
        }

        private void OnDestroy()
        {
            OnShowEvent = null;
            OnHideEvent = null;
        }
    }
}
