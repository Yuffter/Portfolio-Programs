using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialTextController : MonoBehaviour
{
    public Vector3 startPosition,endPosition;
    public float duration = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        var seq = DOTween.Sequence();
        seq.Append(GetComponent<RectTransform>().DOAnchorPos(endPosition, duration).SetEase(Ease.Linear));
        seq.Append(GetComponent<RectTransform>().DOAnchorPos(startPosition, duration).SetEase(Ease.Linear));
        seq.SetLoops(-1);
        seq.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
