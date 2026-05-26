using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public static class HierarchyColorChanger
{
    private const string INSTALLER_OBJECT_NAME = "===== Installer =====";
    private const string VIEW_OBJECT_NAME = "===== View =====";
    private const string DI_OBJECT_NAME = "===== DI =====";
    static HierarchyColorChanger()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
    }

    private static void OnHierarchyGUI(int instanceID, Rect selectionRect)
    {
        var obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (obj == null) return;

        if (obj.name == INSTALLER_OBJECT_NAME)
        {
            // 背景色を描画
            EditorGUI.DrawRect(selectionRect, new Color(0.2f, 0.6f, 1.0f, 0.3f));
        }

        if (obj.name == VIEW_OBJECT_NAME)
        {
            // 背景色を描画
            EditorGUI.DrawRect(selectionRect, new Color(1.0f, 0.6f, 0.2f, 0.3f));
        }

        if (obj.name == DI_OBJECT_NAME)
        {
            // 背景色を描画
            EditorGUI.DrawRect(selectionRect, new Color(0.6f, 0.2f, 1.0f, 0.3f));
        }
    }
}
