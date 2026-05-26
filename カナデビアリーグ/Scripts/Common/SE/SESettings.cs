using UnityEngine;

namespace MinutesGame.Common.SE
{
    /// <summary>
    /// SE設定クラス
    /// </summary>
    [CreateAssetMenu(fileName = "SESettings", menuName = "MinutesGame/SO/SE/SESettings")]
    public class SESettings : ScriptableObject
    {
        [SerializeField, Header("SEマスター音量"), Range(0f, 1f)] private float _masterVolume = 1f;
        [SerializeField, Header("同時再生可能なSEの最大数"), Range(1, 100)] private int _maxSimultaneousSECount = 100;

        /// <summary>
        /// SEマスター音量
        /// </summary>
        public float MasterVolume => _masterVolume;
        /// <summary>
        /// 同時再生可能なSEの最大数
        /// </summary>
        public int MaxSimultaneousSECount => _maxSimultaneousSECount;
    }
}
