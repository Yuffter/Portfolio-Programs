using Unity.Cinemachine;
using UnityEngine;
using SO;
using DG.Tweening;

namespace FieldCamera
{
    /// <summary>
    /// 俯瞰視点カメラクラス
    /// </summary>
    public class OverviewCameraController
    {
        // カメラアニメーション定数
        private const float TRANSITION_DURATION = 1f;
        private const float RESET_DURATION = 0.5f;
        private const float FOCUS_HEIGHT_OFFSET = 20f;
        private const float FOCUS_ANGLE_X = 90f;
        private const float RESET_ANGLE_X = 60f;

        private Sequence _transitionSequence;
        private DependencyContainer _dependencyContainer;
        private bool _canMove = false;

        public OverviewCameraController(DependencyContainer dependencyContainer)
        {
            _dependencyContainer = dependencyContainer ?? throw new System.ArgumentNullException(nameof(dependencyContainer));
        }

        /// <summary>
        /// カメラ操作を有効にする
        /// </summary>
        public void EnableInput()
        {
            _canMove = true;
        }

        /// <summary>
        /// カメラ操作を無効にする
        /// </summary>
        public void DisableInput()
        {
            _canMove = false;
        }

        /// <summary>
        /// カメラを移動する
        /// </summary>
        /// <param name="input">WASDの入力値</param>
        public void Move(Vector2 input)
        {
            if (!_canMove || _dependencyContainer?.MainCamera == null) return;
            // カメラの右方向のベクトルを取得
            Vector3 right = _dependencyContainer.MainCamera.transform.right;
            // ワールド座標軸のy軸とカメラの右向きベクトルの外積を取ることでカメラの前方向ベクトルを取得
            Vector3 forward = Vector3.Cross(Vector3.up, right);
            // 入力に基づいて移動ベクトルを計算
            Vector3 movement = right * input.x + forward * input.y * -1;
            // カメラの位置を更新
            _dependencyContainer.MainCamera.transform.position += movement * _dependencyContainer.OverviewCameraParameters.moveSpeed * Time.deltaTime;
        }


        /// <summary>
        /// カメラをズームする
        /// </summary>
        /// <param name="delta">マウスホイールの入力値</param>
        public void Zoom(float delta)
        {
            if (!_canMove || _dependencyContainer?.MainCamera == null) return;

            _dependencyContainer.MainCamera.fieldOfView = Mathf.Clamp(
                _dependencyContainer.MainCamera.fieldOfView - delta * _dependencyContainer.OverviewCameraParameters.ZoomSpeed * Time.deltaTime,
                _dependencyContainer.OverviewCameraParameters.MinZoom,
                _dependencyContainer.OverviewCameraParameters.MaxZoom
            );
        }

        public void FocusPosition(Vector3 position)
        {
            if (_dependencyContainer?.MainCamera == null) return;

            _transitionSequence?.Kill();
            _transitionSequence = DOTween.Sequence();
            _transitionSequence.Append(
                _dependencyContainer.MainCamera.transform.DOMove(position + new Vector3(0, FOCUS_HEIGHT_OFFSET, 0), TRANSITION_DURATION).SetEase(Ease.OutBack)
            );
            _transitionSequence.Join(
                DOTween.To(() => _dependencyContainer.MainCamera.fieldOfView, x => _dependencyContainer.MainCamera.fieldOfView = x, _dependencyContainer.OverviewCameraParameters.MaxZoom, TRANSITION_DURATION).SetEase(Ease.InOutSine)
            );
            _transitionSequence.Join(
                DOTween.To(() => _dependencyContainer.MainCamera.transform.eulerAngles, x => _dependencyContainer.MainCamera.transform.eulerAngles = x, new Vector3(FOCUS_ANGLE_X, 0, 0), TRANSITION_DURATION).SetEase(Ease.InOutSine)
            );
        }

        public void ResetFocus(Vector3 beforePosition)
        {
            if (_dependencyContainer?.MainCamera == null) return;

            _transitionSequence?.Kill();
            _transitionSequence = DOTween.Sequence();
            _transitionSequence.Append(
                _dependencyContainer.MainCamera.transform.DOMove(beforePosition, RESET_DURATION).SetEase(Ease.OutBack)
            );
            _transitionSequence.Join(
                DOTween.To(() => _dependencyContainer.MainCamera.fieldOfView, x => _dependencyContainer.MainCamera.fieldOfView = x, _dependencyContainer.OverviewCameraParameters.MaxZoom, RESET_DURATION).SetEase(Ease.InOutSine)
            );
            _transitionSequence.Join(
                DOTween.To(() => _dependencyContainer.MainCamera.transform.eulerAngles, x => _dependencyContainer.MainCamera.transform.eulerAngles = x, new Vector3(RESET_ANGLE_X, 0, 0), RESET_DURATION).SetEase(Ease.InOutSine)
            );
        }

        /// <summary>
        /// カメラの位置を取得する
        /// </summary>
        /// <returns></returns>
        public Vector3 GetPosition()
        {
            if (_dependencyContainer?.MainCamera == null)
            {
                Debug.LogWarning("Main camera is not found in DependencyContainer.");
                return Vector3.zero;
            }
            return _dependencyContainer.MainCamera.transform.position;
        }
    }
}
