using AnnulusGames.TweenPlayables;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    //public bool isGrounded = false;
    public bool isJumping = false;
    Rigidbody2D myRigid;
    PlayerGroundCheck groundCheck;
    PlayerLadder playerLadder;

    float currentJumpTime = 0f;
    TextMeshProUGUI test;
    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<PlayerGroundCheck>();
        playerLadder = GetComponent<PlayerLadder>();
        /*print(GameObject.Find("RemainingPiece"));
        test = GameObject.Find("Test").GetComponent<TextMeshProUGUI>();*/
    }

    public void Jump(float vertical,bool isJumpKeyPressed,bool isGrounded)
    {
        if (playerLadder.IsHitLadder.Value) return;
        /*if (test != null)
        {
            test.text = $"isgrounded {isGrounded} \nisJumping {isJumping}\n collider count {groundCheck.hit_downRay.Length}";
            for (int i = 0;i < groundCheck.hit_downRay.Length;i++)
            {
                test.text += "\n" + groundCheck.hit_downRay[i].collider.gameObject.name;
            }
        }*/
        if ((vertical > 0f || isJumpKeyPressed) && isGrounded && !isJumping)
        {
            myRigid.velocity = new Vector2(myRigid.velocity.x, 0f);
            myRigid.AddForce(Vector2.up * PlayerData.Instance.jumpPower);

            isJumping = true;
            print("ジャンプはじめ");
        }
        else if (isJumping)
        {
            if (vertical > 0 || isJumpKeyPressed)
            {
                print("ジャンプ中");
                currentJumpTime += Time.deltaTime;
                if (currentJumpTime >= PlayerData.Instance.maxJumpDuration)
                {
                    isJumping = false;
                }
                myRigid.AddForce(Vector2.up * PlayerData.Instance.jumpPower * PlayerData.Instance.jumpSpeedCurve.Evaluate(currentJumpTime /  PlayerData.Instance.maxJumpDuration));
            }
            else
            {
                print("ジャンプ終了");
                isJumping = false;
            }
        }
        else
        {
            print($"isgrounded {isGrounded} isJumping {isJumping}");
            currentJumpTime = 0f;
        }
    }
}
