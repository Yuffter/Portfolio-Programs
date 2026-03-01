using R3;

namespace Consumables.ItemSlot.QuantityText
{
    /// <summary>
    /// 消耗品の所持数管理Model
    /// </summary>
    public sealed class QuantityModel
    {
        private ReactiveProperty<int> _quantity = new ReactiveProperty<int>(-1);
        /// <summary>
        /// 所持数
        /// </summary>
        public ReadOnlyReactiveProperty<int> Quantity => _quantity;

        /// <summary>
        /// 所持数を設定する
        /// </summary>
        /// <param name="quantity">新しい所持数</param>
        public void SetQuantity(int quantity)
        {
            _quantity.Value = quantity;
        }
    }
}
