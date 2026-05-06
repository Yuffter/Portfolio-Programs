using UnityEngine;
using Project.Main.GameSystems.Presentation;
using Cysharp.Threading.Tasks;

namespace Project.Main.GameSystems.Sequences
{
    public class ExplanationSequence
    {
        private readonly ITransitionPresentation _transitionPresentation;
        private readonly IExplanationPresentation _explanationPresentation;

        public ExplanationSequence(ITransitionPresentation transitionPresentation, IExplanationPresentation explanationPresentation)
        {
            _explanationPresentation = explanationPresentation;
            _transitionPresentation = transitionPresentation;
        }

        /// <summary>
        /// 説明シーケンスを開始する
        /// </summary>
        /// <param name="fadeDuration">フェード時間</param>
        /// <returns></returns>
        public async UniTask StartSequenceAsync(float fadeDuration)
        {
            // フェードインを実行
            await _transitionPresentation.FadeOutAsync(fadeDuration);

            // 説明を表示
            _explanationPresentation.ShowExplanation();

            await UniTask.WaitUntil(() => Input.GetKeyDown(KeyCode.Z));

            _explanationPresentation.HideExplanation();
        }
    }
}