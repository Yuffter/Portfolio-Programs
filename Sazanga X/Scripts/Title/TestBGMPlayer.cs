using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using KanKikuchi.AudioManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestBGMPlayer : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    [SerializeField] Vector3 originalScale,focusScale;
    [SerializeField] Sprite playSprite,pauseSprite;
    public void OnPointerClick(PointerEventData eventData)
    {
        ClickPlayer();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<RectTransform>().DOScale(focusScale,0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<RectTransform>().DOScale(originalScale,0.1f);
    }

    public void ClickPlayer() {
        GetComponent<RectTransform>().DOPunchScale(Vector3.one * 0.1f,0.1f,5);
        //BGMが再生されている場合
        if (BGMManager.Instance.IsPlaying()) {
            BGMManager.Instance.FadeOut(0.5f);
            GetComponent<Image>().sprite = playSprite;
        }
        else {
            BGMManager.Instance.Play(BGMPath.GAME);
            GetComponent<Image>().sprite = pauseSprite;
        }
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
