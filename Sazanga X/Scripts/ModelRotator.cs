using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ModelRotator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //transform.DORotate(new Vector3(-90, 360, -90), 5, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 5 * Time.deltaTime));
    }
}
