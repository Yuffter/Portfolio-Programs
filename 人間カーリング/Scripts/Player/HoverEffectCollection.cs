using System.Collections.Generic;
using System.Linq;
using Player;
using UnityEngine;

namespace Player
{
    /// <summary>
    /// ホバーエフェクトを保持しておくコレクション
    /// </summary>
    public class HoverEffectCollection
    {
        private HoverEffect[] _hoverEffects;
        /// <summary>
        /// シーンに存在しているホバーエフェクト
        /// </summary>
        public HoverEffect[] HoverEffects => _hoverEffects;

        public int Length => _hoverEffects.Length;

        public HoverEffectCollection()
        {
            _hoverEffects = GameObject.FindObjectsByType<HoverEffect>(FindObjectsSortMode.None);
        }
    }
}