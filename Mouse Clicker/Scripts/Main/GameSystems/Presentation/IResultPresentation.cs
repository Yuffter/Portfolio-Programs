using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Main.GameSystems.Presentation
{
    public interface IResultPresentation
    {
        /// <summary>
        /// 結果を表示する
        /// </summary>
        /// <returns></returns>
        UniTask ShowResultAsync();
    }
}