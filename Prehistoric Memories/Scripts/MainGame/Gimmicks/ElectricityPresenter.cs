using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElectricityPresenter : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] ElectricityModel electricityModel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = electricityModel.Ratio;
    }
}
