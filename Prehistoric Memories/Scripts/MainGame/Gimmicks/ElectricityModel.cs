using DG.Tweening;
using KanKikuchi.AudioManager;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class ElectricityModel : MonoBehaviour
{
    public IReadOnlyReactiveProperty<float> ElectricEnergy => electricEnergy;
    ReactiveProperty<float> electricEnergy = new ReactiveProperty<float>(100);

    public bool isAction = false;
    public float Ratio
    {
        get
        {
            return (maxDuration - currentTime) / maxDuration;
        }
    }

    [SerializeField] float maxDuration = 40;
    [SerializeField] Light2D light;

    float currentTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAction) return;
        if ((Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.Return)) && currentTime <= maxDuration)
        {
            currentTime += Time.deltaTime;
            light.intensity = 0.3f;
        }
        else
        {
            light.intensity = 0.01f;
        }

        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return)) && currentTime <= maxDuration)
        {
            SEManager.Instance.Play(SEPath.LIGHT_UP);
        }

        if ((Input.GetKeyUp(KeyCode.Z) || Input.GetKeyUp(KeyCode.Return)) && currentTime <= maxDuration)
        {
            SEManager.Instance.Play(SEPath.LIGHT_OUT);
        }
    }
}
