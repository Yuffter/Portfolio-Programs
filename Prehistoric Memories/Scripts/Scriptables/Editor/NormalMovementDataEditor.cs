using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(ObjectNormalMovementData))]
public class NormalMovementDataEditor : Editor
{
    private ReorderableList reorderableList;
    private SerializedProperty movementDataList;

    private void OnEnable()
    {
        movementDataList = serializedObject.FindProperty("movementData");

        reorderableList = new ReorderableList(serializedObject, movementDataList);
        reorderableList.drawElementCallback = (rect, index, active, focused) => {
            var actionData = movementDataList.GetArrayElementAtIndex(index);
            EditorGUI.PropertyField(rect, actionData);
        };
        reorderableList.drawHeaderCallback = (rect) => EditorGUI.LabelField(rect, "移動リスト");
        reorderableList.elementHeightCallback = index => EditorGUI.GetPropertyHeight(movementDataList.GetArrayElementAtIndex(index));
    }

    public override void OnInspectorGUI()
    {
        /*var eventId = serializedObject.FindProperty("eventId");
        EditorGUILayout.PropertyField(eventId);
        */
        serializedObject.Update();
        reorderableList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }
}
