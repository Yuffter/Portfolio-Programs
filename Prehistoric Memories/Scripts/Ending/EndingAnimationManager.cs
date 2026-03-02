using DG.Tweening;
using KanKikuchi.AudioManager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndingAnimationManager : MonoBehaviour
{
    [SerializeField] IntroTextTypewriter typewriter;
    [SerializeField] Image background;
    [SerializeField] TextMeshProUGUI theEndText;
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
        yield return background.DOFade(1f, 1f).WaitForCompletion();
        yield return theEndText.DOFade(1f, 1f).WaitForCompletion();
        yield return new WaitForSeconds(2f);

        System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".exe"));
        Application.Quit();
    }
}
