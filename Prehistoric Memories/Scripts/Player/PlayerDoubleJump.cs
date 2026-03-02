using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;

public class PlayerDoubleJump : MonoBehaviour
{
    PlayerGroundCheck playerGroundCheck;
    PlayerJump playerJump;
    public bool hasDoneDoubleJump = false;
    // Start is called before the first frame update
    void Start()
    {
        playerGroundCheck = GetComponent<PlayerGroundCheck>();
        playerJump = GetComponent<PlayerJump>();

        //二段ジャンプの処理
        this.UpdateAsObservable()
            .Where(_ => AbilitiesData.Instance.abilities[0].isEnable)
            .Where(_ => Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
            .Where(_ => !playerGroundCheck.IsGrounded.Value)
            .Where(_ => !hasDoneDoubleJump)
            .Where(_ => !playerJump.isJumping)
            .Subscribe(_ =>
            {
                hasDoneDoubleJump = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
                GetComponent<Rigidbody2D>().AddForce(Vector2.up * PlayerData.Instance.doubleJumpPower);
            });

        //二段ジャンプ可能にする
        playerGroundCheck.IsGrounded
            .Where(value => value)
            .Subscribe(_ =>
            {
                hasDoneDoubleJump = false;
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
