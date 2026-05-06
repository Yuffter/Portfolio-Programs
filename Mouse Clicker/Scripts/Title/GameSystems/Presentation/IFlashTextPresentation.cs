namespace Project.Title.GameSystems.Presentation
{
    /// <summary>
    /// テキストのフラッシュアニメーションを制御するインターフェース
    /// </summary>
    public interface IFlashTextPresentation
    {
        /// <summary>
        /// フラッシュアニメーションを開始する
        /// </summary>
        void StartAnimation();

        /// <summary>
        /// フラッシュアニメーションを停止する
        /// </summary>
        void StopAnimation();
    }
}