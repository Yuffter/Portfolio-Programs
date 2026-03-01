using AudioManager.SE;
using UnityEngine;

namespace Consumables.ItemSlot
{
    /// <summary>
    /// 消耗品データ
    /// </summary>
    public class ConsumableData
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public Sprite ItemIcon { get; set; }
        public int ItemQuantity { get; set; }
        public GameObject Prefab { get; set; }
        public SEName HoverSE { get; set; } // アイテムにカーソルを合わせたときのSE

        public void Initialize(int itemId, string itemName, string itemDescription, Sprite itemIcon, int itemQuantity, GameObject prefab, SEName hoverSE)
        {
            ItemID = itemId;
            ItemName = itemName;
            ItemDescription = itemDescription;
            ItemIcon = itemIcon;
            ItemQuantity = itemQuantity;
            Prefab = prefab;
            HoverSE = hoverSE;
        }
    }
}
