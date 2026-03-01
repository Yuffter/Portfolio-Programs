using UnityEngine;
using UnityEngine.InputSystem;

namespace Consumables
{
    public class MainController : MonoBehaviour
    {
        [SerializeField] private Consumables.ItemSlot.ItemSlotGenerator _itemSlotGenerator;
        [SerializeField] private SO.EventHub.ConsumablesEventHub _consumablesEventHub;
        [SerializeField] private ItemDescriptionPanel.Provider _descriptionProvider;
        [SerializeField] private ExplanationVideo.Controller _explanationVideoController;
        private bool _canUseConsumable = false;
        public bool CanUseConsumable => _canUseConsumable;
        private SO.Consumables.HeldConsumables _heldConsumables;

        void Awake()
        {
            SetEvents();
        }

        private void SetEvents()
        {
            _consumablesEventHub.EnableConsumablesEvent.Subscribe(EnableConsumables);
            _consumablesEventHub.DisableConsumablesEvent.Subscribe(DisableConsumables);
            _consumablesEventHub.InitializeConsumablesEvent.Subscribe(InitializeConsumables);
        }

        private void EnableConsumables()
        {
            _canUseConsumable = true;
        }

        private void DisableConsumables()
        {
            _canUseConsumable = false;
            _descriptionProvider.NotifyHide();
            _explanationVideoController.NotifyHide();
        }

        private void InitializeConsumables(SO.Consumables.HeldConsumables heldConsumables)
        {
            _heldConsumables = heldConsumables;

            // 所持しているアイテムをUIに反映
            for (int i = 0; i < _heldConsumables.HeldConsumablesList.Count; i++)
            {
                var itemSlot = _itemSlotGenerator.GenerateItemSlot();
                itemSlot.SetItem(_heldConsumables.HeldConsumablesList[i].ItemID, _heldConsumables.HeldConsumablesList[i].Quantity);
            }
        }

        private void OnDestroy()
        {
            _consumablesEventHub.EnableConsumablesEvent.Unsubscribe(EnableConsumables);
            _consumablesEventHub.DisableConsumablesEvent.Unsubscribe(DisableConsumables);
            _consumablesEventHub.InitializeConsumablesEvent.Unsubscribe(InitializeConsumables);
        }
    }
}
