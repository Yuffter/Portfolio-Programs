using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using KanKikuchi.AudioManager;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackToMenuButton : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] Vector3 orignalScale,focusScale;
    [SerializeField] CanvasGroup settingGroup;
    [SerializeField] GameEvent ableToControll;
    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<RectTransform>().DOScale(focusScale,0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<RectTransform>().DOScale(orignalScale,0.1f);
    }

    public async void OnClick() {
        //テスト用BGMが再生されたままで閉じられた場合
        if (BGMManager.Instance.IsPlaying()) {
            FindAnyObjectByType<TestBGMPlayer>().ClickPlayer();
        }
        await settingGroup.DOFade(0,1).AsyncWaitForCompletion();
        settingGroup.interactable = false;
        ableToControll.Raise();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
