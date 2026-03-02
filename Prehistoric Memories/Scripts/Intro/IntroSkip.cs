using KanKikuchi.AudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSkip : MonoBehaviour
{
    bool canSkip = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!canSkip) return;
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(Skip());
        }
    }

    IEnumerator Skip()
    {
        canSkip = false;
        BGMManager.Instance.FadeOut(3f);
        yield return FadeManager.Instance.FadeIn(3f);
        SceneManagerEx.Instance.LoadAndUnloadScene("Intro", "StageSelect", () => FadeManager.Instance.FadeOut(1f));
    }
}
