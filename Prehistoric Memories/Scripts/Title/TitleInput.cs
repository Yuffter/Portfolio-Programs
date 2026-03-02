using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleInput : MonoBehaviour
{
    public bool isUpArrowPressed {  get; private set; }
    public bool isDownArrowPressed { get; private set; }

    public bool isDecisionPressed { get;private set; }

    public bool canSelect { get; set; } = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!canSelect)
        {
            isDecisionPressed = false;
            return;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            isUpArrowPressed = true;
        }
        else
        {
            isUpArrowPressed = false;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            isDownArrowPressed = true;
        }
        else
        {
            isDownArrowPressed = false;
        }

        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return))
        {
            isDecisionPressed = true;
            canSelect = false;
        }
        else
        {
            isDecisionPressed = false;
        }
    }
}
