using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Main.GameSystems.Presentation
{
    public interface ICountDownPresentation
    {
        /// <summary>
        /// カウントダウンを開始する
        /// </summary>
        /// <param name="duration">カウント時間</param>
        /// <returns></returns>
        UniTask StartCountDownAsync(float duration);
    }
}