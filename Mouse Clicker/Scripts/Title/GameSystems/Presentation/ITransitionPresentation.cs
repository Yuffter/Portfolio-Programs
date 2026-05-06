using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Title.GameSystems.Presentation
{
    /// <summary>
    /// シーン遷移アニメーションのプレゼンテーションインターフェース
    /// </summary>
    public interface ITransitionPresentation
    {
        /// <summary>
        /// シーン遷移アニメーションを開始する
        /// </summary>
        UniTask StartTransitionAsync();
    }
}