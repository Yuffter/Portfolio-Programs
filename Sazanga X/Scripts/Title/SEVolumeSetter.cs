using System.Collections;
using System.Collections.Generic;
using KanKikuchi.AudioManager;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SEVolumeSetter : MonoBehaviour
{
    Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume() {
        SEManager.Instance.ChangeBaseVolume(slider.value);
    }
}
