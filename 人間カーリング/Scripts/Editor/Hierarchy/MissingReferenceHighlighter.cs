using UnityEditor;
using UnityEngine;

namespace Hierarchy 
{
    [InitializeOnLoad]
    public static class MissingReferenceHighlighter
    {
        static MissingReferenceHighlighter()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
        }

        private static void OnHierarchyGUI(int instanceID, Rect selectionRect)
        {
            GameObject obj = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            if (obj == null) return;

            var monoBehaviours = obj.GetComponents<MonoBehaviour>();
            foreach (var mono in monoBehaviours)
            {
                if (mono == null) continue;
                
                //ユーザ作成のスクリプト以外は除外
                if (mono.GetType().Assembly.GetName().Name != "Assembly-CSharp")
                {
                    continue;
                }

                var fields = mono.GetType().GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance |
                                                    System.Reflection.BindingFlags.Public);
                foreach (var field in fields)
                {
                    if (field.GetCustomAttributes(typeof(SerializeField), true).Length > 0 && (field.GetValue(mono) == null || field.GetValue(mono).Equals(null)))
                    {
                        if (field.GetCustomAttributes(typeof(IsNullableAttribute), true).Length > 0)
                        {
                            continue;
                        }
                        EditorGUI.DrawRect(selectionRect, new Color(1f, 0.3f, 0.3f, 0.3f));
                        return;
                    }
                }
            }
        }
    }
}