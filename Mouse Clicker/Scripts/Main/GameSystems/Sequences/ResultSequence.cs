using Cysharp.Threading.Tasks;
using Project.Main.GameSystems.Presentation;
using UnityEngine;

namespace Project.Main.GameSystems.Sequences
{
    public class ResultSequence
    {
        private readonly IResultPresentation _resultPresentation;

        public ResultSequence(IResultPresentation resultPresentation)
        {
            _resultPresentation = resultPresentation;
        }

        /// <summary>
        /// 結果を表示する
        /// </summary>
        /// <returns></returns>
        public async UniTask StartSequenceAsync()
        {
            await _resultPresentation.ShowResultAsync();
        }
    }
}