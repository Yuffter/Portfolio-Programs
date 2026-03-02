using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float horizontal { get; private set; }
    public float vertical { get; private set; }

    public KeyInput jumpKey { get; private set; }

    public Vector2 lastMove { get; private set; }

    public Vector2 movement { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        jumpKey = new GameObject("JumpKey").AddComponent<KeyInput>();
        jumpKey.transform.parent = transform;
        jumpKey.keys = new List<KeyCode>() { KeyCode.W, KeyCode.UpArrow, KeyCode.Space };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        movement = new Vector2(horizontal, vertical);

        if (Mathf.Abs(movement.x) > 0.5f)
        {
            lastMove = new Vector2(movement.x, 0f);
        }
    }
}
