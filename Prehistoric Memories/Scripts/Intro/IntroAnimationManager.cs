using KanKikuchi.AudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroAnimationManager : MonoBehaviour
{
    [SerializeField] IntroTextTypewriter typewriter;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartTypewrite());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartTypewrite()
    {
        yield return FadeManager.Instance.FadeOut(1f);
        BGMManager.Instance.Play(BGMPath.INTRO);
        yield return StartCoroutine(typewriter.StartTypewrite());
        BGMManager.Instance.FadeOut(1f);
        yield return FadeManager.Instance.FadeIn(1f);
        SceneManagerEx.Instance.LoadAndUnloadScene("Intro", "StageSelect", () => FadeManager.Instance.FadeOut(1f));
    }
}
