using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StageSelect.StagePanel
{
    /// <summary>
    /// ステージパネルに必要な依存関係をまとめるコンテナクラス
    /// </summary>
    public class DependencyContainer : MonoBehaviour
    {
        [SerializeField, Header("ステージパネルのアニメーションコンポーネント")] private Animations.StagePanelAnimator _animator;
        public Animations.StagePanelAnimator Animator => _animator;

        [SerializeField, Header("ステージのサムネイル画像表示用Imageコンポーネント")] private Image _thumbnail;
        public Image Thumbnail => _thumbnail;

        [SerializeField, Header("ハイスコアテキスト")] private TMP_Text _highScoreText;
        public TMP_Text HighScoreText => _highScoreText;
    }
}
