using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using KanKikuchi.AudioManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TestSEPlayer : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    [SerializeField] Vector3 originalScale,focusScale;
    [SerializeField] Sprite playSprite,pauseSprite;
    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponent<RectTransform>().DOPunchScale(Vector3.one * 0.1f,0.1f,5);
        GetComponent<Image>().sprite =pauseSprite;
        //BGMが再生されている場合
        SEManager.Instance.Play(SEPath.DAMAGE_1,1,0,1,false,() => GetComponent<Image>().sprite = playSprite);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<RectTransform>().DOScale(focusScale,0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<RectTransform>().DOScale(originalScale,0.1f);
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
