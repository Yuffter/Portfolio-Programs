using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class TitleContentView : MonoBehaviour
{
    [SerializeField] GameObject rightPointer, leftPointer;
    [SerializeField] List<Vector2> rightPointerPositions = new List<Vector2>();
    [SerializeField] List<Vector2> leftPointerPositions = new List<Vector2>();
    [SerializeField] List<TMP_Text> contents = new List<TMP_Text>();

    [SerializeField] Material dissolveTransitionMat;

    [Header("設定関連")]
    [SerializeField] GameEvent unableToControllEvent;
    [SerializeField] CanvasGroup settingGroup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnChangeContent(int number)
    {
        rightPointer.GetComponent<RectTransform>().DOAnchorPos(rightPointerPositions[number],0.3f);
        leftPointer.GetComponent<RectTransform>().DOAnchorPos(leftPointerPositions[number], 0.3f);

        if (number == 0)
        {
            DOVirtual.Float(contents[0].fontSize, 80f, 0.3f, x => contents[0].fontSize = x);
            DOVirtual.Float(contents[1].fontSize, 65f, 0.3f, x => contents[1].fontSize = x);
        }
        else
        {
            DOVirtual.Float(contents[0].fontSize, 65f, 0.3f, x => contents[0].fontSize = x);
            DOVirtual.Float(contents[1].fontSize, 80f, 0.3f, x => contents[1].fontSize = x);
        }
    }

    public async void OnDecision(TitleContentModel.ContentType contentType)
    {
        switch (contentType)
        {
            case TitleContentModel.ContentType.START:
                await UniTask.Delay(TimeSpan.FromSeconds(1f));
                await dissolveTransitionMat.DOFloat(1f, "_Progress", 2f).SetEase(Ease.Linear).AsyncWaitForCompletion();
                SceneManager.LoadScene("Game");
                break;
            case TitleContentModel.ContentType.CONTINUE:
                unableToControllEvent.Raise();
                await UniTask.Delay(TimeSpan.FromSeconds(1f));
                settingGroup.DOFade(1,1).OnComplete(() => settingGroup.interactable = true);
                break;
        }
    }
}
