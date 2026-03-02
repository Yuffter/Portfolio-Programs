using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using KanKikuchi.AudioManager;

public class PieceAnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DORotate(new Vector3(0, 360, 0), 8f,RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Collect()
    {
        StartCoroutine(PlayCollectAnimation());
    }

    IEnumerator PlayCollectAnimation()
    {
        SEManager.Instance.Play(SEPath.COLLECT);
        transform.DOKill();

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMoveY(-2, 2).SetRelative())
            .Append(transform.DOMoveY(15, 2).SetRelative())
            .Join(transform.DORotate(new Vector3(0, 360 * 1f, 0f), 3, RotateMode.FastBeyond360).SetEase(Ease.OutCubic))
            .OnComplete(() => Destroy(gameObject)).SetLink(gameObject);

        yield return null;
    }
}
