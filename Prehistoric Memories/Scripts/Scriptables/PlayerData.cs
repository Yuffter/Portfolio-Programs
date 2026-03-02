using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject
{
    public float hp;
    [Header("それぞれのインク残量")]
    public List<Ink> inkRemainingAmount = new List<Ink>();

    [Header("左右移動")]
    public float movingSpeed = 6.0f;

    [Header("ジャンプ")]
    public LayerMask whatIsGround;
    public float groundCheckDistance = 0.5f;
    public float jumpPower = 180f;
    public float maxJumpDuration = 0.3f;
    public AnimationCurve jumpSpeedCurve;

    [Header("二段ジャンプ")]
    public float doubleJumpPower = 300f;

    [Header("スーパーダッシュ")]
    public float superDashPower = 300f;

    [Header("梯子")]
    public float ladderSpeed = 2f;

    static PlayerData _playerData;
    const string FILE_PATH = "Scriptables/PlayerData";
    public static PlayerData Instance
    {
        get
        {
            if (_playerData == null)
            {
                _playerData = Resources.Load(FILE_PATH) as PlayerData;
            }

            return _playerData;
        }
    }
}

[Serializable]
public class Ink
{
    [Header("色の名前(Red,Green,Blue)")]
    public string colorName;

    [Header("残量(max100%)")]
    public float remainingAmount;
}
