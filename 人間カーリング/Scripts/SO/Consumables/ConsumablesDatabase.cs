using System.Collections.Generic;
using AudioManager.SE;
using UnityEngine;

namespace SO.Consumables
{
    [CreateAssetMenu(fileName = "ConsumablesDatabase", menuName = "ScriptableObject/Consumables/ConsumablesDatabase")]
    public class ConsumablesDatabase : ScriptableObject
    {
        [SerializeField, Header("消耗品データベース")] private List<ConsumableItem> _consumableItems;
        public List<ConsumableItem> ConsumableItems => _consumableItems;

        public ConsumableItem GetConsumableItemByID(int itemID)
        {
            return _consumableItems.Find(item => item.ItemID == itemID);
        }
    }

    [System.Serializable]
    public class ConsumableItem
    {
        public int ItemID;
        public string ItemName;
        [Multiline(lines: 5)]
        public string ItemDescription;  // アイテムの説明文(使用前の説明)
        public string ItemUsedDescription; // アイテムの効果反映説明(使用後の説明)
        public Sprite ItemIcon;
        public GameObject ItemPrefab;
        public SEName HoverSE; // アイテムにカーソルを合わせたときのSE
    }
}
