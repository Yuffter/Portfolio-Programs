using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RequireColorShower : MonoBehaviour
{
    ObjectOutline objectOutline;
    GameObject requireColorPanel;
    TextMeshProUGUI explanationLabel;
    // Start is called before the first frame update
    void Start()
    {
        objectOutline = GetComponent<ObjectOutline>();
        requireColorPanel = GameObject.Find("RequireColor");
        explanationLabel = requireColorPanel.transform.Find("Explanation").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseOver()
    {
        //実体化されていたら処理を行わない
        if (objectOutline.IsMaterialized.Value) return;

        explanationLabel.text =
            $"必要な色\n" +
            $"<color=red>赤 {objectOutline.requireInk[0]}\n" +
            $"<color=green>緑 {objectOutline.requireInk[1]}\n" +
            $"<color=blue>青 {objectOutline.requireInk[2]}";

        RectTransform canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
        var mousePos = Input.mousePosition;
        var magnification = canvasRect.sizeDelta.x / Screen.width;
        mousePos.x = mousePos.x * magnification - canvasRect.sizeDelta.x / 2;
        mousePos.y = mousePos.y * magnification - canvasRect.sizeDelta.y / 2;
        mousePos.z = transform.localPosition.z;

        mousePos.x -= 200;

        requireColorPanel.transform.localPosition = mousePos;
    }

    public void OnMouseExit()
    {
        requireColorPanel.transform.localPosition = new Vector3(10000, 0, 0);
    }
}
