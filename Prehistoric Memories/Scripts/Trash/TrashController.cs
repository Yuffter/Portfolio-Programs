using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrashController : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    public bool isMouseOver = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public async void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
        transform.DOKill();
        transform.DORotate(new Vector3(0, 0, 0), 0.05f).SetEase(Ease.Linear);
        transform.DOScale(new Vector3(1f, 1f, 1), 0.05f).SetEase(Ease.Linear);
    }

    public async void OnPointerEnter(PointerEventData eventData)
    {
        if (GetComponent<Image>().sprite.name == "Trash") isMouseOver = true;
        else isMouseOver = false;
        transform.DOKill();
        transform.DORotate(new Vector3(0, 0, 10), 0.05f).SetEase(Ease.Linear);
        transform.DOScale(new Vector3(1.2f, 1.2f, 1), 0.05f).SetEase(Ease.Linear);
    }
}
