using UnityEngine;

namespace DescriptionViewer
{
    public class DescriptionStringGenerator
    {
        private ItemDataBase _itemDataBase;

        public DescriptionStringGenerator(ItemDataBase itemDataBase)
        {
            _itemDataBase = itemDataBase;
        }

        public string GenerateItemDescription(ItemType itemType)
        {
            ItemData itemData = _itemDataBase.GetItemData(itemType);
            if (itemData == null)
            {
                return "No description available.";
            }

            return FormatItem(itemData);
        }

        public string GenerateCharacterDescription(PlayerBase playerBase)
        {
            return FormatCharacter(playerBase);
        }

        private string FormatItem(ItemData rawData)
        {
            return $"アイテム名: {rawData.getItemName()}\n説明: {rawData.getDescription()}";
        }

        private string FormatCharacter(PlayerBase playerBase)
        {
            return $"速度: {playerBase._speed}\nスコア倍率: {playerBase._scoreRate}\n攻撃力: {playerBase._Attack}";
        }
    }
}
