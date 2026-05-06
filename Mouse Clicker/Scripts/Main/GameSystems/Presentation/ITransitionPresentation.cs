using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Main.GameSystems.Presentation
{
    public interface ITransitionPresentation
    {
        /// <summary>
        /// フェードインを非同期実行
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        UniTask FadeInAsync(float duration);

        /// <summary>
        /// フェードアウトを非同期実行
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        UniTask FadeOutAsync(float duration);
    }
}