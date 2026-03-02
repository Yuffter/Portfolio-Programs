using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.Mathematics;
using UnityEngine;

public class WaterModel : MonoBehaviour
{
    public IReadOnlyReactiveProperty<float> RemainingWater => remainingWater;
    ReactiveProperty<float> remainingWater = new ReactiveProperty<float>(100);

    [SerializeField] float duration = 200;
    // Start is called before the first frame update
    void Start()
    {
        DOVirtual.Float(100, 0, duration, x => remainingWater.Value = x).SetEase(Ease.Linear);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
