using UnityEngine;

namespace Project.Main.GameSystems.Presentation
{
    public interface IExplanationPresentation
    {
        /// <summary>
        /// 説明を表示する
        /// </summary>
        void ShowExplanation();

        /// <summary>
        /// 説明を非表示にする
        /// </summary>
        void HideExplanation();
    }
}