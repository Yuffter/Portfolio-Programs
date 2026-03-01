using UnityEditor;
using UnityEngine;

/// <summary>
/// Hierarchyウィンドウで特定のコンポーネントを持つGameObjectを色分けして表示するエディタ拡張
/// </summary>
[InitializeOnLoad]
public static class HierarchyColorChanger
{
    /// <summary>
    /// ハイライト表示する対象のコンポーネントクラス名
    /// </summary>
    private const string TargetClassName = "ServiceRegisterer";

    /// <summary>
    /// 静的コンストラクタ。エディタ起動時に自動実行される
    /// </summary>
    static HierarchyColorChanger()
    {
        // Hierarchyウィンドウの描画イベントにコールバックを登録
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
    }

    /// <summary>
    /// Hierarchyウィンドウの各アイテムが描画される際に呼ばれるコールバック
    /// </summary>
    /// <param name="instanceID">GameObjectのインスタンスID</param>
    /// <param name="selectionRect">描画される矩形領域</param>
    private static void OnHierarchyGUI(int instanceID, Rect selectionRect)
    {
        // インスタンスIDからGameObjectを取得
        var obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
        if (obj == null) return;

        // 指定したクラス名のコンポーネントを持っているかチェック
        if (!HasComponentWithClassName(obj, TargetClassName)) return;

        // 紫系の半透明色で背景を描画
        var color = new Color(0.5f, 0f, 1f, 0.15f);
        EditorGUI.DrawRect(selectionRect, color);
    }

    /// <summary>
    /// 指定したGameObjectが特定のクラス名のコンポーネントを持っているか判定
    /// </summary>
    /// <param name="obj">チェック対象のGameObject</param>
    /// <param name="className">検索するコンポーネントのクラス名</param>
    /// <returns>指定したクラス名のコンポーネントを持っている場合true</returns>
    private static bool HasComponentWithClassName(GameObject obj, string className)
    {
        // GameObjectにアタッチされている全コンポーネントを取得
        var components = obj.GetComponents<Component>();
        foreach (var comp in components)
        {
            if (comp == null) continue; // Missing Script 対策

            // コンポーネントのクラス名が一致するかチェック
            if (comp.GetType().Name == className)
                return true;
        }
        return false;
    }
}
