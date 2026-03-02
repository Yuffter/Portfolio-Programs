using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerGroundCheck : MonoBehaviour
{
    public IReadOnlyReactiveProperty<bool> IsGrounded => isGrounded;
    ReactiveProperty<bool> isGrounded = new ReactiveProperty<bool>(false);

    public Ray2D downRay;
    public RaycastHit2D[] hit_downRay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded.Value = CheckTheGround();
    }

    bool CheckTheGround()
    {
        /*Ray2D */downRay = new Ray2D(transform.position, Vector2.down);
        /*RaycastHit2D[] */hit_downRay = Physics2D.RaycastAll(downRay.origin, downRay.direction, PlayerData.Instance.groundCheckDistance, PlayerData.Instance.whatIsGround.value);

        Debug.DrawRay(downRay.origin, downRay.direction * PlayerData.Instance.groundCheckDistance, Color.red);
        return hit_downRay.Length >= 1 && hit_downRay[0].collider.gameObject.CompareTag("Untouchable") == false;
    }
}
