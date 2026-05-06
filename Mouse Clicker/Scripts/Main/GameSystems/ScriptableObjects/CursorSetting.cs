using UnityEngine;

namespace Project.Main.GameSystems.ScriptableObjects
{
    /// <summary>
    /// カーソルの設定を管理するScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "CursorSetting", menuName = "ScriptableObjects/CursorSetting")]
    public class CursorSetting : ScriptableObject
    {
        [SerializeField, Header("移動可能な横幅")] private float _maxHorizontal;
        [SerializeField, Header("移動可能な縦幅")] private float _maxVertical;
        [SerializeField, Header("横方向のスペース")] private float _spaceHorizontal;
        [SerializeField, Header("縦方向のスペース")] private float _spaceVertical;

        /// <summary>
        /// 移動可能な横幅
        /// </summary>
        public float MaxHorizontal => _maxHorizontal;

        /// <summary>
        /// 移動可能な縦幅
        /// </summary>
        public float MaxVertical => _maxVertical;

        /// <summary>
        /// 横方向のスペース
        /// </summary>
        public float SpaceHorizontal => _spaceHorizontal;

        /// <summary>
        /// 縦方向のスペース
        /// </summary>
        public float SpaceVertical => _spaceVertical;
    }
}