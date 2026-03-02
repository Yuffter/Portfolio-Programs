using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerLadder : MonoBehaviour
{
    public IReadOnlyReactiveProperty<bool> IsHitLadder => isHitLadder;
    ReactiveProperty<bool> isHitLadder = new ReactiveProperty<bool>(false);

    float originalGravityScale;
    Rigidbody2D myRigid;
    // Start is called before the first frame update
    void Start()
    {
        originalGravityScale = GetComponent<Rigidbody2D>().gravityScale;
        myRigid = GetComponent<Rigidbody2D>();

        //梯子に当たったとき
        this.OnTriggerEnter2DAsObservable()
            .Where(collision => collision.CompareTag("Ladder"))
            .Subscribe(_ =>
            {
                isHitLadder.Value = true;
                myRigid.gravityScale = 0f;
                myRigid.velocity = Vector3.zero;
            });

        this.OnTriggerStay2DAsObservable()
            .Where(collision => collision.CompareTag("Ladder"))
            .Subscribe(_ =>
            {
                isHitLadder.Value = true;
                myRigid.gravityScale = 0f;
                myRigid.velocity = Vector3.zero;
            });

        //梯子から脱出したとき
        this.OnTriggerExit2DAsObservable()
            .Where(collision => collision.CompareTag("Ladder"))
            .Subscribe(_ =>
            {
                isHitLadder.Value = false;
                GetComponent<Rigidbody2D>().gravityScale = originalGravityScale;
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Climb(float vertical)
    {
        if (isHitLadder.Value == false) return;

        if (vertical > 0)
        {
            transform.Translate(Vector3.up * Time.deltaTime * PlayerData.Instance.ladderSpeed);
        }
        else if (vertical < 0)
        {
            transform.Translate(Vector3.down * Time.deltaTime * PlayerData.Instance.ladderSpeed);
        }
    }
}
