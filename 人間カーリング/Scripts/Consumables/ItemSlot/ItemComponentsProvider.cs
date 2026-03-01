using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Consumables.ItemSlot
{
    /// <summary>
    /// アイテムスロット窓口クラス
    /// </summary>
    public class ItemComponentsProvider : MonoBehaviour
    {
        [SerializeField] private DependencyContainer _dependencyContainer;
        public Image IconImage => _dependencyContainer.IconImage;

        /// <summary>
        /// 消耗品データ
        /// </summary>
        public ConsumableData ConsumableData => _dependencyContainer.ConsumableData;
        /// <summary>
        /// ドラッグハンドラー
        /// </summary>

        public ItemDragHandler ItemDragHandler => _dependencyContainer.ItemDragHandler;

        public Consumables.ItemSlot.QuantityText.QuantityModel QuantityModel => _dependencyContainer.QuantityModel;

        /// <summary>
        /// アイコンを設定する
        /// </summary>
        /// <param name="consumableID">消耗品ID</param>
        /// <param name="quantity">所持数</param>
        public void SetItem(int consumableID, int quantity)
        {
            var database = _dependencyContainer.ConsumablesDatabase;
            var itemData = database.GetConsumableItemByID(consumableID);

            if (itemData == null)
            {
                Debug.LogError($"指定されたIDの消耗品が見つかりません。ID:{consumableID}");
                return;
            }

            Sprite iconSprite = itemData.ItemIcon;
            IconImage.sprite = iconSprite;
            _dependencyContainer.QuantityModel.SetQuantity(quantity);
            _dependencyContainer.ConsumableData.Initialize(itemData.ItemID, itemData.ItemName, itemData.ItemDescription, itemData.ItemIcon, quantity, itemData.ItemPrefab, itemData.HoverSE);
        }
    }
}
