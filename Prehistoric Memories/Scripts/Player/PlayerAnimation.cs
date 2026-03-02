using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerAnimation : MonoBehaviour
{
    Animator animator;
    PlayerLadder playerLadder;

    const string WALK = "IsWalk";

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerLadder = GetComponent<PlayerLadder>();
    }
    // Start is called before the first frame update
    void Start()
    {
        playerLadder.IsHitLadder
            .Subscribe(value =>
            {
                animator.SetBool("IsClimbing", value);
            });


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAnimation(Vector2 movement,Vector2 lastMove)
    {
        animator.SetFloat("Dir_X", movement.x);
        animator.SetFloat("Dir_Y", movement.y);
        animator.SetFloat("LastMove_X", lastMove.x);
        animator.SetFloat("LastMove_Y", lastMove.y);
        animator.SetFloat("Input", movement.magnitude);
    }
}
