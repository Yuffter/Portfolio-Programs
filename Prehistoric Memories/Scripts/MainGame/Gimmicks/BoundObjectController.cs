using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using KanKikuchi.AudioManager;

public class BoundObjectController : MonoBehaviour
{
    [SerializeField] Vector3 boundPower = new Vector3(0, 100, 0);
    // Start is called before the first frame update
    void Start()
    {
        this.OnCollisionEnter2DAsObservable()
            .Where(collision => collision.collider.CompareTag("Player"))
            .ThrottleFirst(TimeSpan.FromSeconds(0.5f))        //連続で当たって予期せぬ大ジャンプが起こらないようにする
            .Subscribe(collision =>
            {
                collision.collider.GetComponent<Rigidbody2D>().AddForce(boundPower);
                SEManager.Instance.Play(SEPath.BOUND);
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
