using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System;
using KanKikuchi.AudioManager;
using System.Security.Cryptography;

public class PlayerMovement : MonoBehaviour
{
    public float inputInterval = 1.5f;
    public int currentRow = 2, currentColumn = 2;
    bool canMove = true;

    [SerializeField] PlayerAnimation playerAnimation;
    void Update()
    {
        if (!canMove) return;
        // 左に移動
        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentColumn != 1)
        {
            transform.DOMoveX(-GameSetting.Instance.oneBlockSize, PlayerStatus.Instance.moveDuration).SetRelative();
            WaitMoving();
            currentColumn--;
            playerAnimation.PlayMoveLeftAnimation();
        }
        // 右に移動
        if (Input.GetKeyDown(KeyCode.RightArrow) && GameSetting.Instance.movableColumn != currentColumn)
        {
            transform.DOMoveX(GameSetting.Instance.oneBlockSize, PlayerStatus.Instance.moveDuration).SetRelative();
            WaitMoving();
            currentColumn++;
            playerAnimation.PlayMoveRightAnimation();
        }
        // 上に移動
        if (Input.GetKeyDown(KeyCode.UpArrow) && currentRow != 1)
        {
            transform.DOMoveY(GameSetting.Instance.oneBlockSize, PlayerStatus.Instance.moveDuration).SetRelative();
            WaitMoving();
            currentRow--;
            //playerAnimation.PlayMoveUpAnimation();
        }
        // 下に移動
        if (Input.GetKeyDown(KeyCode.DownArrow) && GameSetting.Instance.movableRow != currentRow)
        {
            transform.DOMoveY(-GameSetting.Instance.oneBlockSize, PlayerStatus.Instance.moveDuration).SetRelative();
            WaitMoving();
            currentRow++;
            // playerAnimation.PlayMoveDownAnimation();
        }

        async void WaitMoving()
        {
            canMove = false;
            await UniTask.Delay(TimeSpan.FromSeconds(inputInterval));
            canMove = true;
        }
    }

    public void UnableToMove() {
        canMove = false;
    }
}
