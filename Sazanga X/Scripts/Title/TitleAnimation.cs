using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;
using TMPro;
using KanKikuchi.AudioManager;

public class TitleAnimation : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] RectTransform jet;
    [SerializeField] Image cross;
    [SerializeField] Image flash;
    [SerializeField] Image comet;
    [SerializeField] Material sazangaMat;
    [SerializeField] Material crossMat;
    [SerializeField] Material dissolveTransitionMat;
    [SerializeField] List<GameObject> contents = new List<GameObject>();
    [SerializeField] GameObject rightPointer, leftPointer;
    [SerializeField] GameEvent ableToControll;
    private void Awake()
    {
        Initialize();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartTitleAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    async void StartTitleAnimation()
    {
        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(0.5f)
            .Append(dissolveTransitionMat.DOFloat(0f, "_Progress", 2f).SetEase(Ease.Linear))
            .Append(jet.DOLocalMoveZ(1000, 0.5f).SetRelative().SetEase(Ease.OutCubic))
            .AppendInterval(0.3f)
            .Append(sazangaMat.DOFloat(1, "_Progress", 1))
            .AppendInterval(0.5f)
            .AppendCallback(() => SEManager.Instance.Play(SEPath.TITLE_SLASH, 1, 0.3f))
            .Append(cross.DOFillAmount(1f, 1f).SetEase(Ease.OutCubic))
            .AppendInterval(0.5f)
            .AppendCallback(() =>
            {
                flash.color = new Color(1, 1, 1, 1);
                flash.DOColor(new Color(1, 1, 1, 0f), 1);
                crossMat.SetFloat("_OverlayPower", 3.22f);
            })
            .AppendInterval(2f)
            .Append(comet.DOFillAmount(1f, 0.5f))
            .AppendCallback(() =>
            {
                for (int i = 0; i < contents.Count; i++)
                {
                    var content = contents[i];
                    if (i % 2 == 0)
                    {
                        content.GetComponent<RectTransform>().DOAnchorPosX(960f, 1).SetEase(Ease.OutQuart);
                    }
                    else
                    {
                        content.GetComponent<RectTransform>().DOAnchorPosX(-960f, 1).SetEase(Ease.OutQuart);
                    }
                }
            })
            .AppendInterval(0.2f)
            .AppendCallback(() => rightPointer.GetComponent<RectTransform>().DOAnchorPosX(-760, 1))
            .Append(leftPointer.GetComponent<RectTransform>().DOAnchorPosX(760, 1f))
            .Append(DOVirtual.Float(65, 80, 0.5f, x => contents[0].GetComponent<TMP_Text>().fontSize = x))
            .AppendCallback(() => ableToControll.Raise());

        //await background.DOColor(new Color(0.74f, 0.74f, 0.74f, 1f), 0.5f).AsyncWaitForCompletion();
    }

    private void Initialize()
    {
        dissolveTransitionMat.SetFloat("_Progress", 1f);
        //background.color = Color.black;
        sazangaMat.SetFloat("_Progress", 0f);
        crossMat.SetFloat("_OverlayPower", 0f);
        cross.fillAmount = 0f;
        comet.fillAmount = 0f;

        for (int i= 0;i < contents.Count;i++)
        {
            if (i % 2 == 0)
            {
                contents[i].GetComponent<RectTransform>().DOAnchorPosX(-200, 0);
            }
            else
            {
                contents[i].GetComponent<RectTransform>().DOAnchorPosX(200, 0);
            }
        }

        rightPointer.GetComponent<RectTransform>().DOAnchorPosX(100, 0);
        leftPointer.GetComponent<RectTransform>().DOAnchorPosX(-100, 0);
    }
}
