using DG.Tweening;
using KanKikuchi.AudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCreatureController : MonoBehaviour
{
    public bool isMaterialize = false;
    public int id = -1;
    public bool isTriggerKeep = false;
    public string tag = "";

    TrashController trash_1, trash_2;
    // Start is called before the first frame update
    void Start()
    {
        DOVirtual.DelayedCall(0.01f, () => FindAnyObjectByType<MainGameManager>().DisablePlayerMoving());
        FindAnyObjectByType<MainGameManager>().isCreatureListOpen.Value = false;

        trash_1 = GameObject.Find("Trash_1").GetComponent<TrashController>();
        trash_2 = GameObject.Find("Trash_2").GetComponent<TrashController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMaterialize)
        {
            Vector2 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = mousePos;
        }
    }

    private void OnMouseDown()
    {
        if (isMaterialize)
        {
            if (FindAnyObjectByType<TutorialManager>() != null)
            {
                FindAnyObjectByType<TutorialManager>().DestroyCreature();
            }
            GameObject effect = Instantiate(Resources.Load("Prefabs/Effects/CFXR3 Hit Light B (Air)") as GameObject);
            effect.transform.position = transform.position;
            Destroy(gameObject);
        }
        else
        {
            if (FindAnyObjectByType<TutorialManager>() != null)
            {
                if (trash_1.isMouseOver || trash_2.isMouseOver)
                {
                    SEManager.Instance.Play(SEPath.FAILURE);
                    return;
                }
                FindAnyObjectByType<TutorialManager>().CreateNewCreature();
            }
            else if (trash_1.isMouseOver || trash_2.isMouseOver)
            {
                FindAnyObjectByType<MainGameManager>().EnablePlayerMoving();
                trash_1.GetComponent<RectTransform>().anchoredPosition =
                    new Vector3(trash_1.GetComponent<RectTransform>().anchoredPosition.x, -100);
                trash_2.GetComponent<RectTransform>().anchoredPosition =
                    new Vector3(trash_2.GetComponent<RectTransform>().anchoredPosition.x, -100);
                SEManager.Instance.Play(SEPath.TRASH);
                Destroy(gameObject);
                return;
            }

            trash_1.GetComponent<RectTransform>().anchoredPosition =
                    new Vector3(trash_1.GetComponent<RectTransform>().anchoredPosition.x, -100);
            trash_2.GetComponent<RectTransform>().anchoredPosition =
                new Vector3(trash_2.GetComponent<RectTransform>().anchoredPosition.x, -100);

            transform.parent = GameObject.Find("StageObjects").transform;
            FindAnyObjectByType<MainGameManager>().EnablePlayerMoving();
            GameObject effect = Instantiate(Resources.Load("Prefabs/Effects/CFXR Magic Poof") as GameObject);
            effect.transform.position = transform.position;
            PlayerData.Instance.inkRemainingAmount[0].remainingAmount -= CreaturesData.Instance.dCreaturesData[id].requiredColorRed;
            PlayerData.Instance.inkRemainingAmount[1].remainingAmount -= CreaturesData.Instance.dCreaturesData[id].requiredColorGreen;
            PlayerData.Instance.inkRemainingAmount[2].remainingAmount -= CreaturesData.Instance.dCreaturesData[id].requiredColorBlue;

            FindAnyObjectByType<InkBottleController>().ChangeInkRemaining();

            if (tag != null || tag == "")
            {
                transform.tag = tag;
            }

            isMaterialize = true;
            GetComponent<SpriteRenderer>().DOFade(1, 0);
            if (GetComponent<BoxCollider2D>())
            {
                if (!isTriggerKeep) GetComponent<BoxCollider2D>().isTrigger = false;
                else GetComponent<AreaEffector2D>().forceMagnitude = 70;
            }
            else
            {
                if (!isTriggerKeep) GetComponent<PolygonCollider2D>().isTrigger = false;
                else GetComponent<AreaEffector2D>().forceMagnitude = 70;
            }
        }
    }
}
