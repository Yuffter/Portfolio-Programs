using UnityEngine;

namespace Consumables.Items
{
    /// <summary>
    /// 消耗品基底クラス
    /// </summary>
    public abstract class ItemBase : MonoBehaviour
    {
        /// <summary>
        /// 消耗品を使用する
        /// </summary>
        /// <param name="usePosition">使用位置</param>
        public abstract bool Use(Vector2 usePosition);

        /// <summary>
        /// 消耗品がドラッグ中に呼ばれる
        /// </summary>
        public abstract void OnDrag();
    }
}
