using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Threading.Tasks;

public class WaveChangeAnimation : MonoBehaviour
{
    [SerializeField] RectTransform borderBottom, borderTop;
    [SerializeField] TMP_Text waveLabel;
    [SerializeField] Image background;
    [SerializeField] Material stripeWipe;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.F))
        // {
        //     DoTest();
        // }
    }

    public async Task Wave_1()
    {
        waveLabel.text = "WAVE 1";
        borderTop.GetComponent<Image>().color = Color.green;
        borderBottom.GetComponent<Image>().color = Color.green;
        await stripeWipe.DOFloat(0.7f, "_Progress", 1f).AsyncWaitForCompletion();
        //await background.DOColor(new Color(0, 0, 0, 0.9f), 0.5f).AsyncWaitForCompletion();
        borderBottom.DOLocalMoveX(0, 1).SetEase(Ease.OutCubic);
        await borderTop.DOLocalMoveX(0, 1).SetEase(Ease.OutCubic).AsyncWaitForCompletion();
        await waveLabel.transform.DOLocalMoveX(0, 1).SetEase(Ease.OutCubic).AsyncWaitForCompletion();
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        borderBottom.DOLocalMoveX(-1920, 1).SetEase(Ease.OutCubic);
        borderTop.DOLocalMoveX(1920, 1).SetEase(Ease.OutCubic);
        waveLabel.DOFade(0, 1f);
        stripeWipe.DOFloat(0, "_Progress", 1f);
        //background.DOColor(new Color(0, 0, 0, 0), 1);
        await DOVirtual.Float(0, 50, 1.2f, x =>
        {
            waveLabel.characterSpacing = x;
        }).AsyncWaitForCompletion();
        waveLabel.transform.DOLocalMoveX(1920, 0);
        waveLabel.characterSpacing = 0;
        await waveLabel.DOFade(1, 0).AsyncWaitForCompletion();
    }

    public async Task Wave_2() {
        waveLabel.text = "WAVE 2";
        borderTop.GetComponent<Image>().color = Color.yellow;
        borderBottom.GetComponent<Image>().color = Color.yellow;
        await stripeWipe.DOFloat(0.7f, "_Progress", 1f).AsyncWaitForCompletion();
        //await background.DOColor(new Color(0, 0, 0, 0.9f), 0.5f).AsyncWaitForCompletion();
        borderBottom.DOLocalMoveX(0, 1).SetEase(Ease.OutCubic);
        await borderTop.DOLocalMoveX(0, 1).SetEase(Ease.OutCubic).AsyncWaitForCompletion();
        await waveLabel.transform.DOLocalMoveX(0, 1).SetEase(Ease.OutCubic).AsyncWaitForCompletion();
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        borderBottom.DOLocalMoveX(-1920, 1).SetEase(Ease.OutCubic);
        borderTop.DOLocalMoveX(1920, 1).SetEase(Ease.OutCubic);
        waveLabel.DOFade(0, 1f);
        stripeWipe.DOFloat(0, "_Progress", 1f);
        //background.DOColor(new Color(0, 0, 0, 0), 1);
        await DOVirtual.Float(0, 50, 1.2f, x =>
        {
            waveLabel.characterSpacing = x;
        }).AsyncWaitForCompletion();
        waveLabel.transform.DOLocalMoveX(1920, 0);
        waveLabel.characterSpacing = 0;
        await waveLabel.DOFade(1, 0).AsyncWaitForCompletion();
    }

    public async Task Wave_3() {
        waveLabel.text = "WAVE 3";
        borderTop.GetComponent<Image>().color = Color.red;
        borderBottom.GetComponent<Image>().color = Color.red;
        await stripeWipe.DOFloat(0.7f, "_Progress", 1f).AsyncWaitForCompletion();
        //await background.DOColor(new Color(0, 0, 0, 0.9f), 0.5f).AsyncWaitForCompletion();
        borderBottom.DOLocalMoveX(0, 1).SetEase(Ease.OutCubic);
        await borderTop.DOLocalMoveX(0, 1).SetEase(Ease.OutCubic).AsyncWaitForCompletion();
        await waveLabel.transform.DOLocalMoveX(0, 1).SetEase(Ease.OutCubic).AsyncWaitForCompletion();
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        borderBottom.DOLocalMoveX(-1920, 1).SetEase(Ease.OutCubic);
        borderTop.DOLocalMoveX(1920, 1).SetEase(Ease.OutCubic);
        waveLabel.DOFade(0, 1f);
        stripeWipe.DOFloat(0, "_Progress", 1f);
        //background.DOColor(new Color(0, 0, 0, 0), 1);
        await DOVirtual.Float(0, 50, 1.2f, x =>
        {
            waveLabel.characterSpacing = x;
        }).AsyncWaitForCompletion();
        waveLabel.transform.DOLocalMoveX(1920, 0);
        waveLabel.characterSpacing = 0;
        await waveLabel.DOFade(1, 0).AsyncWaitForCompletion();
    }
}
