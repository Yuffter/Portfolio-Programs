using UnityEngine;
using System.Collections.Generic;

namespace SO.Consumables
{
    [CreateAssetMenu(fileName = "HeldConsumables", menuName = "ScriptableObject/Consumables/HeldConsumables")]
    public class HeldConsumables : ScriptableObject
    {
        [SerializeField, Header("所持消耗品IDリスト")] private List<HeldConsumableData> _heldConsumablesList;
        public List<HeldConsumableData> HeldConsumablesList => _heldConsumablesList;
    }

    [System.Serializable]
    public class HeldConsumableData
    {
        public int ItemID;
        public int Quantity;

        public HeldConsumableData(int itemId, int quantity)
        {
            ItemID = itemId;
            Quantity = quantity;
        }
    }
}
