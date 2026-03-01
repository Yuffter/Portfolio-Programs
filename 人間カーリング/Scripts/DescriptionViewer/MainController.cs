using UnityEngine;

namespace DescriptionViewer
{
    public class MainController : MonoBehaviour
    {
        private Camera _mainCamera;
        private FocusObjectProvider _focusObjectProvider;
        private const string CAPTURABLE_TAG = "CapturableItem";
        private const string CHARACTER_TAG = "Player";
        [SerializeField] private DescriptionPanel.Controller _descriptionPanelController;
        [SerializeField] private ItemDataBase _itemDataBase;
        [SerializeField] private SO.EventHub.DescriptionViewerEventHub _descriptionViewerEventHub;
        private DescriptionStringGenerator _descriptionStringGenerator;
        private bool _isDescriptionViewerEnabled = true;

        private void Awake()
        {
            _mainCamera = Camera.main;
            _focusObjectProvider = new FocusObjectProvider(_mainCamera);
            _descriptionStringGenerator = new DescriptionStringGenerator(_itemDataBase);
            _descriptionViewerEventHub.EnableDescriptionViewerEvent.Subscribe(EnableDescriptionViewer);
            _descriptionViewerEventHub.DisableDescriptionViewerEvent.Subscribe(DisableDescriptionViewer);
        }

        private void Update()
        {
            if (!_isDescriptionViewerEnabled)
            {
                return;
            }

            GameObject focusObject = _focusObjectProvider.GetFocusObject();
            if (focusObject == null || (!IsCapturableItem(focusObject) && !IsCharacter(focusObject)))
            {
                _descriptionPanelController.HideDescription();
                return;
            }

            // 説明パネルがすでに表示されている場合は、軽量化のために処理を行わない
            // if (_descriptionPanelController.IsActive)
            // {
            //     return;
            // }

            // アイテムの場合
            if (IsCapturableItem(focusObject))
            {
                CapturableItem capturableItem = focusObject.GetComponent<CapturableItem>();

                ItemType itemType = capturableItem.itemType;
                _descriptionPanelController.ShowDescription(
                    _descriptionStringGenerator.GenerateItemDescription(itemType),
                    focusObject.transform.position
                );
            }
            // キャラクターの場合
            else if (IsCharacter(focusObject))
            {
                PlayerBase playerBase = focusObject.GetComponent<PlayerBase>();
                _descriptionPanelController.ShowDescription(
                    _descriptionStringGenerator.GenerateCharacterDescription(playerBase),
                    focusObject.transform.position
                );
            }
            else
            {
                _descriptionPanelController.HideDescription();
            }
        }

        private bool IsCapturableItem(GameObject obj)
        {
            return obj.CompareTag(CAPTURABLE_TAG);
        }

        private bool IsCharacter(GameObject obj)
        {
            return obj.CompareTag(CHARACTER_TAG);
        }

        private void EnableDescriptionViewer()
        {
            _isDescriptionViewerEnabled = true;
        }

        private void DisableDescriptionViewer()
        {
            _isDescriptionViewerEnabled = false;
            _descriptionPanelController.HideDescription();
        }

        private void OnDestroy()
        {
            _descriptionViewerEventHub.EnableDescriptionViewerEvent.Unsubscribe(EnableDescriptionViewer);
            _descriptionViewerEventHub.DisableDescriptionViewerEvent.Unsubscribe(DisableDescriptionViewer);
        }
    }
}
