using DG.Tweening;
using KanKikuchi.AudioManager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TitleInput))]
[RequireComponent(typeof(TitleSelectedItemChanger))]
[RequireComponent(typeof(TitleStateManager))]
[RequireComponent(typeof(TitleAnimationManager))]
public class TitleManager : MonoBehaviour
{
    TitleInput titleInput;
    TitleSelectedItemChanger titleSelectedItemChanger;
    TitleStateManager stateManager;
    TitleAnimationManager titleAnimationManager;
    // Start is called before the first frame update
    void Start()
    {
        titleInput = GetComponent<TitleInput>();
        titleSelectedItemChanger = GetComponent<TitleSelectedItemChanger>();
        stateManager = GetComponent<TitleStateManager>();
        titleAnimationManager = GetComponent<TitleAnimationManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (titleInput.isDownArrowPressed)
        {
            titleSelectedItemChanger.ChangeDown();
        }
        if (titleInput.isUpArrowPressed)
        {
            titleSelectedItemChanger.ChangeUp();
        }

        //要素決定
        if (titleInput.isDecisionPressed)
        {
            titleAnimationManager.PlaySelectElementAnimation(stateManager.currentState);
        }
    }
}
