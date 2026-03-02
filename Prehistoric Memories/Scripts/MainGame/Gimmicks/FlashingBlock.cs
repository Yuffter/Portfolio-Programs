using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingBlock : MonoBehaviour
{
    public float lightUpDuration, lightOutDuration;
    public bool isStartWithLightOn;
    // Start is called before the first frame update
    void Start()
    {
        var seq = DOTween.Sequence();

        if (isStartWithLightOn)
        {
            seq.AppendInterval(lightUpDuration);
            seq.AppendCallback(() => {
                GetComponent<Collider2D>().enabled = false;
                GetComponent<SpriteRenderer>().DOFade(0, 0.1f);
            });
            seq.AppendInterval(lightOutDuration);
            seq.AppendCallback(() => {
                GetComponent<Collider2D>().enabled = true;
                GetComponent<SpriteRenderer>().DOFade(1, 0.1f);
            });
        }
        else
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().DOFade(0, 0.1f);
            seq.AppendInterval(lightOutDuration);
            seq.AppendCallback(() => {
                GetComponent<Collider2D>().enabled = true;
                GetComponent<SpriteRenderer>().DOFade(1, 0.1f);
            });
            seq.AppendInterval(lightUpDuration);
            seq.AppendCallback(() => {
                GetComponent<Collider2D>().enabled = false;
                GetComponent<SpriteRenderer>().DOFade(0, 0.1f);
            });
        }

        seq.SetEase(Ease.Linear).SetLoops(-1);
        seq.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
