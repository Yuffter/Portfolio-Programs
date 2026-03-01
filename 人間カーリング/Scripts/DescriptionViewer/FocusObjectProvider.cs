using UnityEngine;
using UnityEngine.InputSystem;

namespace DescriptionViewer
{
    public class FocusObjectProvider
    {
        private Camera _mainCamera;
        public FocusObjectProvider(Camera mainCamera)
        {
            _mainCamera = mainCamera;
        }

        /// <summary>
        /// マウスカーソル下のオブジェクトを取得する
        /// </summary>
        /// <returns></returns>
        public GameObject GetFocusObject()
        {
            if (_mainCamera == null)
            {
                Debug.LogError("メインカメラが見つかりません");
                return null;
            }
            Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                return hitInfo.collider.gameObject;
            }
            return null;
        }
    }
}
