using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerHPBarView : MonoBehaviour
{
    [SerializeField] RectTransform bar;
    [SerializeField] RectTransform playerHPPanel;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateBar(float ratio)
    {
        bar.GetComponent<Image>().DOFillAmount(ratio, 1);
        playerHPPanel.DOShakeAnchorPos(0.5f, 20, 90, 90, false, true);
    }
}
