using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimeManager : MonoBehaviour
{
    public static int CurrentTime { get; private set; }
    public static bool IsStarting { get; set; }
    [SerializeField] TMP_Text currentTimeLabel;

    private void Awake()
    {
        CurrentTime = 0;
        IsStarting = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //デバッグ機能
        // if (Input.GetKeyDown(KeyCode.Alpha1))
        // {
        //     IsStarting = !IsStarting;
        // }


        currentTimeLabel.text = CurrentTime.ToString();
    }

    private void FixedUpdate()
    {
        if (!IsStarting) return;

        CurrentTime++;
    }

    public static void ResetTime() {
        CurrentTime = 0;
        IsStarting = false;
    }
}
