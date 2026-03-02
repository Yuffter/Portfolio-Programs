using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallInitializer : MonoBehaviour
{
    int openCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0;i < StagesData.Instance.stagesData.Count;i++)
        {
            if (StagesData.Instance.stagesData[i].isOpened) openCount++;
        }

        transform.position = new Vector3(-10 + (openCount-1) * 7, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
