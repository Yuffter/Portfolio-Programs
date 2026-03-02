using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

[CreateAssetMenu]
public class ObjectNormalMovementData : ScriptableObject
{
    public List<NormalMovementData> movementData = new List<NormalMovementData>();
}

[Serializable]
public class NormalMovementData
{
    public MovementType movementType;

    //全体としてのプロパティ
    public Ease ease;
    public ConnectType connectType;
    public bool isRelative = false;
    public float duration = 0;

    //POSITIONプロパティ
    public Vector3 position;

    //ROTATIONプロパティ
    public Vector3 rotation;

    //SCALEプロパティ
    public Vector3 scale;

    //COOLDOWNプロパティ
    public float cooldownDuration = 0;
}

public enum MovementType
{
    POSITION,
    ROTATION,
    SCALE,
    COOLDOWN
}

public enum ConnectType
{
    APPEND,
    JOIN
}