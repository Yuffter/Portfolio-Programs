using System;
using TMPro;
using UnityEngine;

namespace Consumables.ItemDescriptionPanel
{
    /// <summary>
    /// アイテム説明パネルの窓口クラス
    /// </summary>
    public class Provider : MonoBehaviour
    {
        [SerializeField, Header("アイテム説明パネルのCanvasGroup")] private CanvasGroup _canvasGroup;
        [SerializeField, Header("説明テキスト")] private TMP_Text _descriptionText;
        /// <summary>
        /// 説明パネルを表示するイベント
        /// </summary>
        public event Action<ItemDescriptionEvent> OnShowEvent;

        /// <summary>
        /// 説明パネルを非表示にするイベント
        /// </summary>
        public event Action OnHideEvent;

        private void Awake()
        {
            SetEvents();
        }

        private void SetEvents()
        {
            OnShowEvent += itemDescriptionEvent =>
            {
                Show(itemDescriptionEvent.ItemName, itemDescriptionEvent.ItemDescription, itemDescriptionEvent.Position);
            };

            OnHideEvent += Hide;
        }

        /// <summary>
        /// 説明パネルを表示するよう通知する
        /// </summary>
        /// <param name="itemDescriptionEvent"></param>
        public void NotifyShow(ItemDescriptionEvent itemDescriptionEvent)
        {
            OnShowEvent?.Invoke(itemDescriptionEvent);
        }

        /// <summary>
        /// 説明パネルを非表示にするよう通知する
        /// </summary>
        public void NotifyHide()
        {
            OnHideEvent?.Invoke();
        }

        /// <summary>
        /// 説明パネルを表示する
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="itemDescription"></param>
        private void Show(string itemName, string itemDescription, Vector2 position)
        {
            _canvasGroup.alpha = 1f;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;

            GetComponent<RectTransform>().position = new Vector3(
                Mathf.Clamp(position.x, 250f, Screen.width - 200f),
                Mathf.Clamp(position.y + 200, 100f, Screen.height - 100f),
                0f
            );
            _descriptionText.SetText($"<u>{itemName}</u>\n\n{itemDescription}");
        }

        /// <summary>
        /// 説明パネルを非表示にする
        /// </summary>
        private void Hide()
        {
            _canvasGroup.alpha = 0f;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        private void OnDestroy()
        {
            OnShowEvent = null;
            OnHideEvent = null;
        }
    }

    public struct ItemDescriptionEvent
    {
        public string ItemName;
        public string ItemDescription;
        public Vector2 Position;

        public ItemDescriptionEvent(string itemName, string itemDescription, Vector2 position)
        {
            ItemName = itemName;
            ItemDescription = itemDescription;
            Position = position;
        }
    }
}
