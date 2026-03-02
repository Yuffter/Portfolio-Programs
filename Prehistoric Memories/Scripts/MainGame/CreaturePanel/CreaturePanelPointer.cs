using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Playables;

public class CreaturePanelPointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerMoveHandler
{
    Image border;
    GameObject requiredColorPanel;
    TextMeshProUGUI explanationLabel;
    [HideInInspector]
    public int id;

    CreatureData ownCreatureData = null;
    GameObject creatureObject = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        //生成可能の場合
        if (JudgeIfInksAreEnough(ownCreatureData.requiredColorRed, ownCreatureData.requiredColorGreen, ownCreatureData.requiredColorBlue))
        {
            CreateNewCreature();
            Debug.Log("生成出来ます");
            if (FindAnyObjectByType<TutorialManager>() != null)
            {
                FindAnyObjectByType<TutorialManager>().SelectCreature();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        border.color = Color.yellow;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        border.color = Color.white;
        requiredColorPanel.transform.localPosition = new Vector3(10000, 0, 0);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        explanationLabel.text =
            $"必要な色\n" +
            $"<color=red>赤 {CreaturesData.Instance.dCreaturesData[id].requiredColorRed}\n" +
            $"<color=green>緑 {CreaturesData.Instance.dCreaturesData[id].requiredColorGreen}\n" +
            $"<color=blue>青 {CreaturesData.Instance.dCreaturesData[id].requiredColorBlue}";

        RectTransform canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
        var mousePos = Input.mousePosition;
        var magnification = canvasRect.sizeDelta.x / Screen.width;
        mousePos.x = mousePos.x * magnification - canvasRect.sizeDelta.x / 2;
        mousePos.y = mousePos.y * magnification - canvasRect.sizeDelta.y / 2;
        mousePos.z = transform.localPosition.z;

        mousePos.x -= 200;

        requiredColorPanel.transform.localPosition = mousePos;
    }

    private void Awake()
    {
        border = transform.Find("Border").GetComponent<Image>();
        requiredColorPanel = GameObject.Find("RequireColor");
        explanationLabel = requiredColorPanel.transform.Find("Explanation").GetComponent<TextMeshProUGUI>();

        
    }

    public void UpdateContent()
    {
        //自分自身の生成物データを取得する
        ownCreatureData = CreaturesData.Instance.dCreaturesData[id];
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    bool JudgeIfInksAreEnough(float red, float green, float blue)
    {
        var currentColors = PlayerData.Instance.inkRemainingAmount;
        return (currentColors[0].remainingAmount >= red && currentColors[1].remainingAmount >= green && currentColors[2].remainingAmount >= blue);
    }

    void CreateNewCreature()
    {
        GameObject.Find("Trash_1").GetComponent<RectTransform>().anchoredPosition =
            new Vector2(GameObject.Find("Trash_1").GetComponent<RectTransform>().anchoredPosition.x, 100);
        GameObject.Find("Trash_2").GetComponent<RectTransform>().anchoredPosition =
            new Vector2(GameObject.Find("Trash_2").GetComponent<RectTransform>().anchoredPosition.x, 100);

        GameObject.Find("CreatureListClose").GetComponent<PlayableDirector>().Play();
        creatureObject = Instantiate(ownCreatureData.creaturePrefab);
        creatureObject.GetComponent<SpriteRenderer>().DOFade(0.5f, 0);
        creatureObject.GetComponent<NewCreatureController>().id = id;
    }
}
