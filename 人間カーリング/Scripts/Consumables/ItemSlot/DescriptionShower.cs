using System;
using AudioManager.SE;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Consumables.ItemSlot
{
    /// <summary>
    /// アイテムスロットにマウスオーバーしたときに説明パネルを表示する
    /// </summary>
    public class DescriptionShower : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        private ItemDescriptionPanel.Provider _descriptionProvider;
        [SerializeField] private ItemComponentsProvider _itemComponentsProvider;
        private DependencyContainer _dependencyContainer;
        private ExplanationVideo.Controller _explanationVideoController;
        private Sequence _sequence;
        void Awake()
        {
            _descriptionProvider = FindAnyObjectByType<ItemDescriptionPanel.Provider>();
            _explanationVideoController = FindAnyObjectByType<ExplanationVideo.Controller>();
            _dependencyContainer = GetComponent<DependencyContainer>();
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            _sequence?.Kill(true);
            if (_dependencyContainer.QuantityModel.Quantity.CurrentValue == 0) return;
            if (!_dependencyContainer.MainController.CanUseConsumable) return;

            SEManager.Instance.Play(_itemComponentsProvider.ConsumableData.HoverSE);
            _sequence = DOTween.Sequence();
            _sequence.Append(GetComponent<RectTransform>().DOScale(1.1f, 0.2f));
            _descriptionProvider.NotifyShow(
                new ItemDescriptionPanel.ItemDescriptionEvent(
                    _itemComponentsProvider.ConsumableData.ItemName,
                    _itemComponentsProvider.ConsumableData.ItemDescription,
                    GetComponent<RectTransform>().position
                )
            );

            _explanationVideoController.NotifyShow();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _sequence?.Kill(true);
            if (_dependencyContainer.QuantityModel.Quantity.CurrentValue == 0) return;
            if (!_dependencyContainer.MainController.CanUseConsumable) return;

            _sequence = DOTween.Sequence();
            _sequence.Append(GetComponent<RectTransform>().DOScale(1f, 0.2f));
            _descriptionProvider.NotifyHide();
            _explanationVideoController.NotifyHide();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _descriptionProvider.NotifyHide();
        }
    }
}
