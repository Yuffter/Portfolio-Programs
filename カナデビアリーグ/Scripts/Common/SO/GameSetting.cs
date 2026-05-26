using Alchemy.Inspector;
using UnityEngine;

namespace MinutesGame.Common.SO
{
    [CreateAssetMenu(fileName = "GameSetting", menuName = "MinutesGame/SO/GameSetting")]
    public class GameSetting : ScriptableObject
    {
        [Title("タイマー関連")]
        [SerializeField, Header("タイマーの最大時間(s)"), Range(0f,60f)]
        private float _maxTimerDuration;
        /// <summary>
        /// タイマーの最大時間(s)
        /// </summary>
        public float MaxTimerDuration => _maxTimerDuration;

        [SerializeField, Header("1回あたりのスコア量(Normal Ver)")]
        private int _scorePerHitNormal;
        /// <summary>
        /// 1回あたりのスコア量(Normal)
        /// </summary>
        public int ScorePerHitNormal => _scorePerHitNormal;

        [SerializeField, Header("1回あたりのスコア量(DX Ver)")]
        private int _scorePerHitDX;
        /// <summary>
        /// 1回あたりのスコア量(DX)
        /// </summary>
        public int ScorePerHitDX => _scorePerHitDX;

        [SerializeField, Header("DXモード時の矢印の削除間隔(s)"), Range(0f,2f)]
        private float _dxArrowDeletionInterval;
        /// <summary>
        /// DXモード時の矢印の削除間隔(s)
        /// </summary>
        public float DxArrowDeletionInterval => _dxArrowDeletionInterval;
    }
}