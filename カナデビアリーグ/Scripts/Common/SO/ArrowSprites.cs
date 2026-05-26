using MinutesGame.Common.Arrow;
using UnityEngine;

namespace MinutesGame.Common.SO
{
    [CreateAssetMenu(fileName = "ArrowSprites", menuName = "MinutesGame/SO/ArrowSprites")]
    public class ArrowSprites : ScriptableObject
    {
        [SerializeField, Header("上矢印のスプライト")]
        private Sprite _upArrow;
        public Sprite UpArrow => _upArrow;

        [SerializeField, Header("下矢印のスプライト")]
        private Sprite _downArrow;
        public Sprite DownArrow => _downArrow;

        [SerializeField, Header("左矢印のスプライト")]
        private Sprite _leftArrow;
        public Sprite LeftArrow => _leftArrow;

        [SerializeField, Header("右矢印のスプライト")]
        private Sprite _rightArrow;
        public Sprite RightArrow => _rightArrow;
        /// <summary>
        /// スプライトから矢印のタイプを取得します
        /// </summary>
        /// <param name="sprite">スプライト</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException"></exception>

        public ArrowType GetArrowTypeFromSprite(Sprite sprite)
        {
            if (sprite == _upArrow)
            {
                return ArrowType.Up;
            }
            else if (sprite == _downArrow)
            {
                return ArrowType.Down;
            }
            else if (sprite == _leftArrow)
            {
                return ArrowType.Left;
            }
            else if (sprite == _rightArrow)
            {
                return ArrowType.Right;
            }
            else
            {
                throw new System.ArgumentException("Invalid sprite provided");
            }
        }
    }
}