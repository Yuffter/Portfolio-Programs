using UnityEditor;
using UnityEngine;

namespace Hierarchy 
{
    [InitializeOnLoad]
    public class HoverHighlighter
    {
        private static GameObject _highlightedObject;

        static HoverHighlighter()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
            SceneView.duringSceneGui += OnSceneGUI;
        }

        // ヒエラルキーウィンドウのGUIイベント処理
        private static void OnHierarchyGUI(int instanceID, Rect selectionRect)
        {
            Event e = Event.current;

            if (selectionRect.Contains(e.mousePosition))
            {
                _highlightedObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            }
        }

        // シーンビューのGUIイベント処理
        private static void OnSceneGUI(SceneView sceneView)
        {
            if (_highlightedObject != null)
            {
                // オブジェクトにRendererがあればそのBoundsを取得（3Dオブジェクト用）
                Renderer renderer = _highlightedObject.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Handles.color = Color.yellow;
                    Handles.DrawWireCube(renderer.bounds.center, renderer.bounds.size);
                }
                else
                {
                    // RectTransformがある場合はその矩形を描画（2Dオブジェクト用）
                    RectTransform rectTransform = _highlightedObject.GetComponent<RectTransform>();
                    if (rectTransform != null)
                    {
                        DrawRectTransformHighlight(rectTransform);
                    }
                    else
                    {
                        // 上記のコンポーネントがない場合はTransformの位置に小さなマーカーを描画
                        Vector3 position = _highlightedObject.transform.position;
                        Handles.SphereHandleCap(0, position, Quaternion.identity, 0.5f, EventType.Repaint);
                    }
                }
            }

            SceneView.RepaintAll();
        }

        // RectTransformの矩形を描画
        private static void DrawRectTransformHighlight(RectTransform rectTransform)
        {
            Vector3[] worldCorners = new Vector3[4];
            rectTransform.GetWorldCorners(worldCorners);

            Handles.DrawLine(worldCorners[0], worldCorners[1]);
            Handles.DrawLine(worldCorners[1], worldCorners[2]);
            Handles.DrawLine(worldCorners[2], worldCorners[3]);
            Handles.DrawLine(worldCorners[3], worldCorners[0]);
        }
    }
}