using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D myRigid;
    // Start is called before the first frame update
    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
    }

    public void Move(float horizontal)
    {
        if (horizontal > 0)
        {
            myRigid.AddForce(Vector2.right * (PlayerData.Instance.movingSpeed - myRigid.velocity.x) * 20);

        }
        else if (horizontal < 0)
        {
            myRigid.AddForce(Vector2.left * (PlayerData.Instance.movingSpeed + myRigid.velocity.x) * 20);
        }
        else
        {
            myRigid.velocity = new Vector2(0f, myRigid.velocity.y);
        }
    }
}
