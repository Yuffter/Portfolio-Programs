using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PieceCollider : MonoBehaviour
{
    private void Start()
    {
        this.OnTriggerEnter2DAsObservable()
            .Where(collision => collision.CompareTag("Player"))
            .First()
            .Subscribe(_ =>
            {
                GetComponent<PieceController>().CollideWithPlayer();
            });
    }
}
