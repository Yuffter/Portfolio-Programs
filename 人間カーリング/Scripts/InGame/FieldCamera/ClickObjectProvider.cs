using UnityEngine;
using UnityEngine.InputSystem;

namespace FieldCamera
{
    /// <summary>
    /// クリックされたオブジェクト(プレイヤー限定)を提供するクラス
    /// </summary>
    public class ClickObjectProvider
    {
        /// <summary>
        /// クリックされたオブジェクトを取得する
        /// </summary>
        /// <returns></returns>
        public GameObject GetClickedObject()
        {
            if (Camera.main == null)
            {
                Debug.LogWarning("Main camera is not found.");
                return null;
            }

            if (Mouse.current == null)
            {
                Debug.LogWarning("Mouse is not found.");
                return null;
            }

            // マウスのスクリーン座標を取得
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            // レイを作成してオブジェクトを検出
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if (hitInfo.collider.CompareTag("Player"))
                {
                    return hitInfo.collider.gameObject;
                }
            }
            return null;
        }
    }
}
