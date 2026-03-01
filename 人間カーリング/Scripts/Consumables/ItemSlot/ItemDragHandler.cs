using System;
using AudioManager.SE;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Consumables.ItemSlot
{
    /// <summary>
    /// スロットのドラッグハンドラー
    /// </summary>
    [RequireComponent(typeof(ItemComponentsProvider))]
    public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Follower.Controller _followerController;
        private ItemComponentsProvider _itemComponentsProvider;
        private DependencyContainer _dependencyContainer;
        [SerializeField, Header("消耗品使用時のメッセージ発火用SO")] private ItemExplainEventSO _itemExplainEventSO;

        private void Awake()
        {
            _followerController = FindAnyObjectByType<Follower.Controller>();
            _dependencyContainer = GetComponent<DependencyContainer>();
            _itemComponentsProvider = GetComponent<ItemComponentsProvider>();
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_itemComponentsProvider.QuantityModel.Quantity.CurrentValue == 0)
            {
                SEManager.Instance.Play(SEName.CannotUseConsumable);
                return;
            }
            if (!_dependencyContainer.MainController.CanUseConsumable)
            {
                SEManager.Instance.Play(SEName.CannotUseConsumable);
                return;
            }

            _followerController.NotifySet(_itemComponentsProvider.ConsumableData.Prefab);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _followerController.NotifyOnDrag();
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_itemComponentsProvider.QuantityModel.Quantity.CurrentValue == 0) return;
            if (!_dependencyContainer.MainController.CanUseConsumable) return;

            bool canUse = _followerController.NotifyUse(eventData.position);
            // アイテムが使用出来た場合は所持数を減らす
            if (canUse)
            {
                _itemComponentsProvider.QuantityModel.SetQuantity(_itemComponentsProvider.QuantityModel.Quantity.CurrentValue - 1);
                _itemExplainEventSO.RaiseConsumableExplainRequest(_itemComponentsProvider.ConsumableData.ItemID);
                SEManager.Instance.Play(SEName.UseConsumable);
            }
        }
    }
}
