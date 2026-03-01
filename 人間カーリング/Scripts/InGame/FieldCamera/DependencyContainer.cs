using SO;
using UnityEngine;

namespace FieldCamera
{
    /// <summary>
    /// 依存関係を管理するコンテナクラス
    /// </summary>
    public class DependencyContainer : MonoBehaviour
    {
        private InputProvider _inputProvider;
        /// <summary>
        /// ユーザーの入力を提供するクラス
        /// </summary>
        public InputProvider InputProvider => _inputProvider;

        private ClickObjectProvider _clickObjectProvider;
        /// <summary>
        /// クリックされたオブジェクト(Player限定)を提供するクラス
        /// </summary>
        public ClickObjectProvider ClickObjectProvider => _clickObjectProvider;

        private OverviewCameraController _overviewCameraController;
        public OverviewCameraController OverviewCameraController => _overviewCameraController;

        [SerializeField, Header("カメラ本体")]
        private Camera _mainCamera;
        public Camera MainCamera => _mainCamera;

        [SerializeField, Header("Overviewカメラのパラメータ")]
        private OverviewCameraParameters _overviewCameraParameters;
        public OverviewCameraParameters OverviewCameraParameters => _overviewCameraParameters;

        [SerializeField, Header("ガイドイベントハブ")]
        private SO.EventHub.GuideEventHub _guideEventHub;
        public SO.EventHub.GuideEventHub GuideEventHub => _guideEventHub;

        [SerializeField, Header("詳細ビューイベントハブ")]
        private SO.EventHub.DescriptionViewerEventHub _descriptionViewerEventHub;
        public SO.EventHub.DescriptionViewerEventHub DescriptionViewerEventHub => _descriptionViewerEventHub;

        public Vector3 BeforeFocusPosition { get; set; } = Vector3.zero;

        public void Initialize()
        {
            _inputProvider = new InputProvider();
            _clickObjectProvider = new ClickObjectProvider();
            _overviewCameraController = new OverviewCameraController(this);
            BeforeFocusPosition = Camera.main.transform.position;
        }
    }
}
