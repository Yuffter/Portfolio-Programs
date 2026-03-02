using KanKikuchi.AudioManager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class TitleSelectedItemChanger : MonoBehaviour
{
    TitleStateManager stateManager;
    [SerializeField] List<TextMeshProUGUI> selectableElements = new List<TextMeshProUGUI>();
    [SerializeField] float elementMovementWidth = 30f;
    [SerializeField] float elementMovementDuration = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        stateManager = GetComponent<TitleStateManager>();

        selectableElements[(int)stateManager.currentState].text = "<u>" + selectableElements[(int)stateManager.currentState].text;
        selectableElements[(int)stateManager.currentState].rectTransform.DOKill(true);
        selectableElements[(int)stateManager.currentState].rectTransform.DOLocalMoveX(elementMovementWidth, 1).SetRelative();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeDown()
    {
        switch (stateManager.currentState)
        {
            case TitleStateManager.ElementState.START:
                SEManager.Instance.Play(SEPath.SELECTER_MOVE);
                selectableElements[(int)stateManager.currentState].text = selectableElements[(int)stateManager.currentState].text.Substring(3);
                selectableElements[(int)stateManager.currentState].rectTransform.DOKill(true);
                selectableElements[(int)stateManager.currentState].rectTransform.DOLocalMoveX(-elementMovementWidth, elementMovementDuration).SetRelative();

                stateManager.ChangeState(1);

                selectableElements[(int)stateManager.currentState].text = "<u>" + selectableElements[(int)stateManager.currentState].text;
                selectableElements[(int)stateManager.currentState].rectTransform.DOKill(true);
                selectableElements[(int)stateManager.currentState].rectTransform.DOLocalMoveX(elementMovementWidth, elementMovementDuration).SetRelative();
                break;
        }
    }

    public void ChangeUp()
    {
        switch (stateManager.currentState)
        {
            case TitleStateManager.ElementState.EXIT:
                SEManager.Instance.Play(SEPath.SELECTER_MOVE);
                selectableElements[(int)stateManager.currentState].text = selectableElements[(int)stateManager.currentState].text.Substring(3);
                selectableElements[(int)stateManager.currentState].rectTransform.DOKill(true);
                selectableElements[(int)stateManager.currentState].rectTransform.DOLocalMoveX(-elementMovementWidth, elementMovementDuration).SetRelative();

                stateManager.ChangeState(-1);

                selectableElements[(int)stateManager.currentState].text = "<u>" + selectableElements[(int)stateManager.currentState].text;
                selectableElements[(int)stateManager.currentState].rectTransform.DOKill(true);
                selectableElements[(int)stateManager.currentState].rectTransform.DOLocalMoveX(elementMovementWidth, elementMovementDuration).SetRelative();
                break;
        }
    }
}
