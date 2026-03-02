using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInput : MonoBehaviour
{
    public List<KeyCode> keys = new List<KeyCode>();
    public bool isKey, isKeyDown, isKeyUp;

    private void Update()
    {
        if (keys.Count == 0) return;

        bool check = false;
        for (int i = 0; i < keys.Count; i++)
        {
            if (Input.GetKey(keys[i])) check = true;
        }

        if (check)
        {
            if (!isKey) isKeyDown = true;
            isKey = true;
        }
        else
        {
            if (isKey) isKeyUp = true;
            isKey = false;
        }

        StartCoroutine(CountKey());
    }

    private IEnumerator CountKey()
    {
        yield return new WaitForFixedUpdate();
        isKeyUp = false;
        isKeyDown = false;
    }
}
