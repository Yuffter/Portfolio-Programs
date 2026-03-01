using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "OverviewCameraParameters", menuName = "ScriptableObjects/OverviewCameraParameters")]
    public class OverviewCameraParameters : ScriptableObject
    {
        [SerializeField, Header("カメラの移動速度")]
        private float _moveSpeed = 5f;

        /// <summary>
        /// カメラの移動速度
        /// </summary>
        public float moveSpeed => _moveSpeed;

        [SerializeField, Header("ズーム速度")]
        private float _zoomSpeed = 10f;
        /// <summary>
        /// ズーム速度
        /// </summary>
        public float ZoomSpeed => _zoomSpeed;

        [SerializeField, Header("ズームの最小値")]
        private float _minZoom = 20f;
        /// <summary>
        /// ズームの最小値
        /// </summary>
        public float MinZoom => _minZoom;

        [SerializeField, Header("ズームの最大値")]
        private float _maxZoom = 60f;
        /// <summary>
        /// ズームの最大値
        /// </summary>
        public float MaxZoom => _maxZoom;
    }
}