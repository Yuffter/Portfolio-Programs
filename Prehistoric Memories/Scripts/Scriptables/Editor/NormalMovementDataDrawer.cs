using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using DG.Tweening;

[CustomPropertyDrawer(typeof(NormalMovementData))]
public class NormalMovementDataDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // List用に1つのプロパティであることを示すためPropertyScopeで囲む
        using (new EditorGUI.PropertyScope(position, label, property))
        {
            // 0指定だとReorderableListのドラッグと被るのでLineHeightを指定
            position.height = EditorGUIUtility.singleLineHeight;

            var movementTypeRect = new Rect(position)
            {
                y = position.y
            };
            var actionTypeProperty = property.FindPropertyRelative("movementType");
            actionTypeProperty.enumValueIndex = EditorGUI.Popup(movementTypeRect, "遷移タイプ", actionTypeProperty.enumValueIndex, Enum.GetNames(typeof(MovementType)));

            switch ((MovementType)actionTypeProperty.enumValueIndex)
            {
                //POSITIONプロパティ
                case MovementType.POSITION:
                    var positionRect = new Rect(movementTypeRect)
                    {
                        y = movementTypeRect.y + EditorGUIUtility.singleLineHeight + 2f
                    };
                    var positionProperty = property.FindPropertyRelative("position");
                    positionProperty.vector3Value = EditorGUI.Vector3Field(positionRect, "遷移後の座標", positionProperty.vector3Value);

                    var durationRect = new Rect(positionRect)
                    {
                        y = positionRect.y + EditorGUIUtility.singleLineHeight + 2f
                    };
                    var durationProperty = property.FindPropertyRelative("duration");
                    durationProperty.floatValue = EditorGUI.FloatField(durationRect, "遷移時間", durationProperty.floatValue);

                    var isRelativeRect = new Rect(durationRect)
                    {
                        y = durationRect.y +EditorGUIUtility.singleLineHeight + 2f
                    };
                    var isRelativeProperty = property.FindPropertyRelative("isRelative");
                    isRelativeProperty.boolValue = EditorGUI.Toggle(isRelativeRect, "相対的な移動か", isRelativeProperty.boolValue);

                    var easeRect = new Rect(isRelativeRect)
                    {
                        y = isRelativeRect.y + EditorGUIUtility.singleLineHeight + 2f
                    };
                    var easeProperty = property.FindPropertyRelative("ease");
                    easeProperty.enumValueIndex = EditorGUI.Popup(easeRect, "イージングタイプ", easeProperty.enumValueIndex, Enum.GetNames(typeof(Ease)));

                    var connectTypeRect = new Rect(easeRect)
                    {
                        y = easeRect.y + EditorGUIUtility.singleLineHeight + 2f
                    };
                    var connectTypeProperty = property.FindPropertyRelative("connectType");
                    connectTypeProperty.enumValueIndex = EditorGUI.Popup(connectTypeRect, "連結タイプ", connectTypeProperty.enumValueIndex, Enum.GetNames(typeof(ConnectType)));
                    break;

                //ROTATIONプロパティ
                case MovementType.ROTATION:
                    var rotationRect = new Rect(movementTypeRect)
                    {
                        y = movementTypeRect.y + EditorGUIUtility.singleLineHeight + 2f
                    };
                    var rotationProperty = property.FindPropertyRelative("rotation");
                    rotationProperty.vector3Value = EditorGUI.Vector3Field(rotationRect, "遷移後の回転", rotationProperty.vector3Value);

                    durationRect = new Rect(rotationRect)
                    {
                        y = rotationRect.y + EditorGUIUtility.singleLineHeight + 2f
                    };
                    durationProperty = property.FindPropertyRelative("duration");
                    durationProperty.floatValue = EditorGUI.FloatField(durationRect, "遷移時間", durationProperty.floatValue);

                    isRelativeRect = new Rect(durationRect)
                    {
                        y = durationRect.y + EditorGUIUtility.singleLineHeight + 2f
                    };
                    isRelativeProperty = property.FindPropertyRelative("isRelative");
                    isRelativeProperty.boolValue = EditorGUI.Toggle(isRelativeRect, "相対的な移動か", isRelativeProperty.boolValue);

                    easeRect = new Rect(isRelativeRect)
                    {
                        y = isRelativeRect.y + EditorGUIUtility.singleLineHeight + 2f
                    };
                    easeProperty = property.FindPropertyRelative("ease");
                    easeProperty.enumValueIndex = EditorGUI.Popup(easeRect, "イージングタイプ", easeProperty.enumValueIndex, Enum.GetNames(typeof(Ease)));

                    connectTypeRect = new Rect(easeRect)
                    {
                        y = easeRect.y + EditorGUIUtility.singleLineHeight + 2f
                    };
                    connectTypeProperty = property.FindPropertyRelative("connectType");
                    connectTypeProperty.enumValueIndex = EditorGUI.Popup(connectTypeRect, "連結タイプ", connectTypeProperty.enumValueIndex, Enum.GetNames(typeof(ConnectType)));
                    break;

                //SCALEプロパティ
                case MovementType.SCALE:
                    var scaleRect = new Rect(movementTypeRect)
                    {
                        y = movementTypeRect.y + EditorGUIUtility.singleLineHeight + 2f
                    };
                    var scaleProperty = property.FindPropertyRelative("scale");
                    scaleProperty.vector3Value = EditorGUI.Vector3Field(scaleRect, "遷移後のスケール", scaleProperty.vector3Value);

                    durationRect = new Rect(scaleRect)
                    {
                        y = scaleRect.y + EditorGUIUtility.singleLineHeight + 2f
                    };
                    durationProperty = property.FindPropertyRelative("duration");
                    durationProperty.floatValue = EditorGUI.FloatField(durationRect, "遷移時間", durationProperty.floatValue);

                    isRelativeRect = new Rect(durationRect)
                    {
                        y = durationRect.y + EditorGUIUtility.singleLineHeight + 2f
                    };
                    isRelativeProperty = property.FindPropertyRelative("isRelative");
                    isRelativeProperty.boolValue = EditorGUI.Toggle(isRelativeRect, "相対的な移動か", isRelativeProperty.boolValue);

                    easeRect = new Rect(isRelativeRect)
                    {
                        y = isRelativeRect.y + EditorGUIUtility.singleLineHeight + 2f
                    };
                    easeProperty = property.FindPropertyRelative("ease");
                    easeProperty.enumValueIndex = EditorGUI.Popup(easeRect, "イージングタイプ", easeProperty.enumValueIndex, Enum.GetNames(typeof(Ease)));

                    connectTypeRect = new Rect(easeRect)
                    {
                        y = easeRect.y + EditorGUIUtility.singleLineHeight + 2f
                    };
                    connectTypeProperty = property.FindPropertyRelative("connectType");
                    connectTypeProperty.enumValueIndex = EditorGUI.Popup(connectTypeRect, "連結タイプ", connectTypeProperty.enumValueIndex, Enum.GetNames(typeof(ConnectType)));
                    break;

                //COOLDOWNプロパティ
                case MovementType.COOLDOWN:
                    var cooldownDurationRect = new Rect(movementTypeRect)
                    {
                        y = movementTypeRect.y + EditorGUIUtility.singleLineHeight + 2f
                    };
                    var cooldownDurationProperty = property.FindPropertyRelative("cooldownDuration");
                    cooldownDurationProperty.floatValue = EditorGUI.FloatField(cooldownDurationRect, "待機時間", cooldownDurationProperty.floatValue);
                    break;
            }
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var height = EditorGUIUtility.singleLineHeight;

        var actionTypeProperty = property.FindPropertyRelative("movementType");
        switch ((MovementType)actionTypeProperty.enumValueIndex)
        {
            case MovementType.POSITION:
                height = 130f;
                break;
            case MovementType.ROTATION:
                height = 130f;
                break;
            case MovementType.SCALE:
                height = 130f;
                break;
            case MovementType.COOLDOWN:
                height = 50f;
                break;
        }

        return height;
    }
}
