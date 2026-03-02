using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class WaterPresenter : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] WaterModel waterModel;
    // Start is called before the first frame update
    void Start()
    {
        waterModel.RemainingWater
            .Subscribe(_ =>
            {
                slider.value = waterModel.RemainingWater.Value / 100f;
            });

        waterModel.RemainingWater
            .Where(value => value == 0f)
            .First()
            .Subscribe(_ =>
            {
                FindAnyObjectByType<PlayerRespawn>().Death(true);
                print("test");
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
