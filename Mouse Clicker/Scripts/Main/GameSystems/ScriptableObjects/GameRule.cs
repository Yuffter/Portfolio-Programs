using UnityEngine;

namespace Project.Main.GameSystems.ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameRule", menuName = "ScriptableObjects/GameRule")]
    public class GameRule : ScriptableObject
    {
        [SerializeField, Header("ゲームの時間制限")] private int _timeLimit;
        [SerializeField, Header("マウスの生成間隔の上昇率")] private AnimationCurve _mouseSpawnRateCurve;
        [SerializeField, Header("マウスの生成間隔の初期値")] private float _initialMouseSpawnRate;
        [SerializeField, Header("マウスのデスポーン時間")] private float _mouseDespawnTime;

        /// <summary>
        /// ゲームの制限時間
        /// </summary>
        public int TimeLimit => _timeLimit;

        /// <summary>
        /// マウスの生成間隔の上昇率
        /// </summary>
        public AnimationCurve MouseSpawnRateCurve => _mouseSpawnRateCurve;

        /// <summary>
        /// マウスの生成間隔の初期値
        /// </summary>
        public float InitialMouseSpawnRate => _initialMouseSpawnRate;

        /// <summary>
        /// マウスのデスポーン時間
        /// </summary>
        public float MouseDespawnTime => _mouseDespawnTime;
    }
}