using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class DeathBlock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.OnCollisionEnter2DAsObservable()
            .Where(collision => collision.collider.CompareTag("Player"))
            .Subscribe(collision =>
            {
                collision.collider.GetComponent<PlayerRespawn>().Death();
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
