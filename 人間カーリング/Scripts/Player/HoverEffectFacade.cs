using UnityEngine;

namespace Player
{
    /// <summary>
    /// ホバーエフェクトへの窓口クラス
    /// </summary>
    public class HoverEffectFacade
    {
        private HoverEffectCollection _hoverEffectCollection;

        public HoverEffectFacade()
        {
            _hoverEffectCollection = new HoverEffectCollection();
        }

        /// <summary>
        /// ホバーエフェクトを有効化する
        /// </summary>
        public void EnableHoverEffects()
        {
            for (int i = 0; i < _hoverEffectCollection.Length; i++)
            {
                _hoverEffectCollection.HoverEffects[i].EnableEffect();
            }
        }

        /// <summary>
        /// ホバーエフェクトを無効化する
        /// </summary>
        public void DisableHoverEffects()
        {
            for (int i = 0; i < _hoverEffectCollection.Length; i++)
            {
                _hoverEffectCollection.HoverEffects[i].DisableEffect();
            }
        }
    }
}