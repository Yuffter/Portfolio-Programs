using MinutesGame.Common.SO;
using MinutesGame.Common.Arrow;
using UnityEngine;
using VContainer;

namespace MinutesGame.Common.Factory
{
    public class ArrowFactory : Common.Interfaces.IFactory<Sprite>
    {
        [Inject]
        private ArrowSprites _arrowSprites;

        /// <summary>
        /// 矢印スプライトをランダムに生成します
        /// </summary>
        /// <returns></returns>
        public Sprite Create()
        {
            int rand = Random.Range(0, 4);
            switch (rand)
            {
                case 0:
                    return _arrowSprites.UpArrow;
                case 1:
                    return _arrowSprites.DownArrow;
                case 2:
                    return _arrowSprites.LeftArrow;
                case 3:
                    return _arrowSprites.RightArrow;
                default:
                    return null;
            }
        }
    }
}