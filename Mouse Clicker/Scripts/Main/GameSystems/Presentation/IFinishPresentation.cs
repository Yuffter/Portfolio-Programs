using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Main.GameSystems.Presentation
{
    public interface IFinishPresentation
    {
        /// <summary>
        /// ゲーム終了テキストを表示する
        /// </summary>
        /// <returns></returns>
        UniTask ShowFinishTextAsync();
    }
}