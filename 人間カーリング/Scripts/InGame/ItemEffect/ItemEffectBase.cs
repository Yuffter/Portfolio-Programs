using UnityEngine;

namespace InGame.ItemEffect
{
    /// <summary>
    /// アイテム効果の基底クラス
    /// </summary>
    public abstract class ItemEffectBase : MonoBehaviour
    {
        /// <summary>
        /// アイテム効果発動の処理
        /// </summary>
        /// <remarks>
        /// このメソッドには実際のアイテム効果を記述します
        /// <list type="bullet">
        /// <item>継承先クラスのOnTriggerEnterやUpdateで呼び出してください</item>
        /// <item>効果対象が自身だけでなく、他の対象まで及ぶ場合、別途ローカル変数を用意してください</item>
        /// </list>
        /// </remarks>
        public abstract void OnItemEffect();
    }
}
