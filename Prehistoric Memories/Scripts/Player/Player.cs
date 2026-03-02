using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerJump))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(PlayerGroundCheck))]
[RequireComponent(typeof(PlayerAnimation))]
[RequireComponent(typeof(PlayerRespawn))]
[RequireComponent(typeof(PlayerDoubleJump))]
[RequireComponent(typeof(PlayerSuperDash))]
[RequireComponent(typeof(PlayerLadder))]
public class Player : MonoBehaviour
{
    PlayerMovement playerMovement;
    PlayerJump playerJump;
    PlayerInput playerInput;
    PlayerGroundCheck playerGroundCheck;
    PlayerAnimation playerAnimation;
    PlayerDoubleJump playerDoubleJump;
    PlayerLadder playerLadder;
    [HideInInspector]
    public PlayerRespawn playerRespawn;

    public bool canMove { get; private set; } = true;
    Rigidbody2D myRigid;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerJump = GetComponent<PlayerJump>();
        playerInput = GetComponent<PlayerInput>();
        playerGroundCheck = GetComponent<PlayerGroundCheck>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerRespawn = GetComponent<PlayerRespawn>();
        playerDoubleJump = GetComponent<PlayerDoubleJump>();
        playerLadder = GetComponent<PlayerLadder>();

        myRigid = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!canMove) return;

        playerMovement.Move(playerInput.horizontal);
        playerJump.Jump(playerInput.vertical, playerInput.jumpKey.isKey, playerGroundCheck.IsGrounded.Value);
        playerAnimation.StartAnimation(playerInput.movement,playerInput.lastMove);
        playerLadder.Climb(playerInput.vertical);
    }

    public void EnableMoving()
    {
        canMove = true;
        myRigid.isKinematic = false;
    }

    public void DisableMoving()
    {
        canMove = false;
        playerAnimation.StartAnimation(Vector2.zero, playerInput.lastMove);
        myRigid.isKinematic = true;
        myRigid.velocity = Vector2.zero;
    }
}
