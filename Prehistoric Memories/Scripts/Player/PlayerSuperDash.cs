using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerSuperDash : MonoBehaviour
{
    PlayerGroundCheck playerGroundCheck;
    PlayerDoubleJump playerDoubleJump;
    PlayerInput playerInput;
    Rigidbody2D myRigid;
    bool hasDoneSuperDash = false;
    [SerializeField] GameObject particle;
    // Start is called before the first frame update
    void Start()
    {
        playerGroundCheck = GetComponent<PlayerGroundCheck>();
        playerInput = GetComponent<PlayerInput>();
        playerDoubleJump = GetComponent<PlayerDoubleJump>();
        myRigid = GetComponent<Rigidbody2D>();

        this.UpdateAsObservable()
            .Where(_ => AbilitiesData.Instance.abilities[1].isEnable)
            //.Where(_ => !playerDoubleJump.hasDoneDoubleJump)
            .Where(_ => !hasDoneSuperDash)
            .Where(_ => !playerGroundCheck.IsGrounded.Value)
            .Where(_ => Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            .Subscribe(_ =>
            {
                hasDoneSuperDash = true;
                GameObject g = Instantiate(particle, transform.position, Quaternion.identity);
                g.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                //右向き加速の場合
                if (playerInput.lastMove.x > 0)
                {
                    myRigid.velocity = new Vector2(myRigid.velocity.x, 0f);
                    myRigid.AddForce(Vector2.right * PlayerData.Instance.superDashPower);
                }
                //左向き加速の場合
                else
                {
                    myRigid.velocity = new Vector2(myRigid.velocity.x, 0f);
                    myRigid.AddForce(Vector2.left * PlayerData.Instance.superDashPower);
                }
            });

        //二段ジャンプ可能にする
        playerGroundCheck.IsGrounded
            .Where(value => value)
            .Subscribe(_ =>
            {
                hasDoneSuperDash = false;
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
