using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using DG.Tweening;
using KanKikuchi.AudioManager;

public class FallenBlockButton : MonoBehaviour
{
    [SerializeField] List<GameObject> fallenBlocks = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        this.OnTriggerEnter2DAsObservable()
            .Where(collision => collision.CompareTag("Player"))
            .Subscribe(_ =>
            {
                StartCoroutine(StartFalling());
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartFalling()
    {
        foreach (var block in fallenBlocks)
        {
            block.transform.SetY(-2.5f);
        }

        yield return new WaitForSeconds(1);

        for (int i = 0;i < 3;i++)
        {
            fallenBlocks[i].transform.DOMoveY(0f, 1f).SetEase(Ease.OutBounce);
        }

        yield return new WaitForSeconds(2f);

        for (int i = 0;i <  fallenBlocks.Count-3;i++)
        {
            //fallenBlocks[i].transform.DOMoveY(0, 0.1f);
            fallenBlocks[3 + i].transform.DOMoveY(0, 0.1f);

            yield return new WaitForSeconds(0.2f);

            if (i == 0)
            {
                fallenBlocks[i].transform.DOMoveY(-2.5f, 1f).SetEase(Ease.OutBounce);
            }
            DOVirtual.DelayedCall(0.4f, () => SEManager.Instance.Play(SEPath.STOMP, 0.6f));
            fallenBlocks[i + 1].transform.DOMoveY(-2.5f, 1f).SetEase(Ease.OutBounce);
            fallenBlocks[i+4].transform.DOMoveY(-2.5f, 1f).SetEase(Ease.OutBounce);

            yield return new WaitForSeconds(1f);
        }
    }
}
