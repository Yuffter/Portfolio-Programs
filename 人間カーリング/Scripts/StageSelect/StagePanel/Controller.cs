using UnityEngine;

namespace StageSelect.StagePanel
{
    /// <summary>
    /// ステージパネルのコントローラー
    /// </summary>
    public class Controller : MonoBehaviour
    {
        private DependencyContainer _dependencyContainer;

        void Awake()
        {
            _dependencyContainer = GetComponent<DependencyContainer>();
            if (_dependencyContainer == null)
            {
                Debug.LogError("DependencyContainerがアタッチされていません。");
            }
        }

        /// <summary>
        /// ステージのサムネイルを設定する
        /// </summary>
        /// <param name="thumbnail">サムネイル画像</param>
        public void SetThumbnail(Sprite thumbnail)
        {
            _dependencyContainer.Thumbnail.sprite = thumbnail;
        }

        /// <summary>
        /// ステージのハイスコアを設定する
        /// </summary>
        /// <param name="highScore"></param>
        public void SetHighScore(int highScore)
        {
            _dependencyContainer.HighScoreText.SetText($"ハイスコア : {highScore}");
        }

        /// <summary>
        /// ステージパネルを左に移動する
        /// </summary>
        public void MoveLeft()
        {
            _dependencyContainer.Animator.MoveLeft();
        }

        /// <summary>
        /// ステージパネルを右に移動する
        /// </summary>
        public void MoveRight()
        {
            _dependencyContainer.Animator.MoveRight();
        }

        /// <summary>
        /// ステージパネルをフォーカスする
        /// </summary>
        public void Focus()
        {
            _dependencyContainer.Animator.Focus();
        }

        /// <summary>
        /// ステージパネルのフォーカスを外す
        /// </summary>
        public void Unfocus()
        {
            _dependencyContainer.Animator.Unfocus();
        }
    }
}
