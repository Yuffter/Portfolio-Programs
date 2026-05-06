using Cysharp.Threading.Tasks;
using UnityEngine;
using Project.Main.GameSystems.Presentation;

namespace Project.Main.GameSystems.Sequences
{
    public class CountDownSequence
    {
        public CountDownSequence(ICountDownPresentation countDownPresentation)
        {
            _countDownPresentation = countDownPresentation;
        }
        private readonly ICountDownPresentation _countDownPresentation;

        /// <summary>
        /// カウントダウンを開始する
        /// </summary>
        /// <param name="duration">カウント時間</param>
        /// <returns></returns>
        public async UniTask StartCountDownAsync(float duration)
        {
            await _countDownPresentation.StartCountDownAsync(duration);
        }
    }
}