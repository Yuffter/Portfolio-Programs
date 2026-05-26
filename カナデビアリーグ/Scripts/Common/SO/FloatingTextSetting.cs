using UnityEngine;

namespace MinutesGame.Common.SO
{
    [CreateAssetMenu(fileName = "FloatingTextSetting", menuName = "MinutesGame/FloatingTextSetting")]
    public class FloatingTextSetting : ScriptableObject
    {
        [SerializeField]
        private float _floatUpDistance = 50f;
        public float FloatUpDistance => _floatUpDistance;

        [SerializeField]
        private float _floatUpDuration = 1f;
        public float FloatUpDuration => _floatUpDuration;

        [SerializeField]
        private Color _textColor = Color.white;
        public Color TextColor => _textColor;

        [SerializeField]
        private int _fontSize = 24;
        public int FontSize => _fontSize;
    }
}