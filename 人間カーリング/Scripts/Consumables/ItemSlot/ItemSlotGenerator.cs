using UnityEngine;

namespace Consumables.ItemSlot
{
    /// <summary>
    /// アイテムスロット生成クラス
    /// </summary>
    public class ItemSlotGenerator : MonoBehaviour
    {
        [SerializeField] private ItemComponentsProvider _itemSlotPrefab;
        [SerializeField] private Transform _itemSlotParent;

        /// <summary>
        /// 新しいアイテムスロットを生成する
        /// </summary>
        /// <returns>生成されたアイテムスロットのコンポーネントプロバイダー</returns>
        public ItemComponentsProvider GenerateItemSlot()
        {
            return Instantiate(_itemSlotPrefab, _itemSlotParent);
        }
    }
}
