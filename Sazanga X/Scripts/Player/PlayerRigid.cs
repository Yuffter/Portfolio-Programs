using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRigid : MonoBehaviour
{
    [SerializeField] float gravityScale = 0.2f;
    [SerializeField] float globalGravity = -9.81f;

    bool isCrash = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if (!isCrash) return;

        var rigid = GetComponent<Rigidbody>();
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rigid.AddForce(gravity,ForceMode.Acceleration);
    }

    public void Crash() {
        var rigid = gameObject.AddComponent<Rigidbody>();
        rigid.useGravity = false;
        isCrash = true;
        rigid.AddTorque(new Vector3(100,0,0));
        rigid.AddForce(new Vector3(0,0,300));
    }
}
