using DG.Tweening;
using KanKikuchi.AudioManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewCreatureCreateController : MonoBehaviour
{
    public int id = -1;
    ObjectOutline objectOutline;
    GameObject requireColorPanel;
    GameObject circleRing;
    InkBottleController inkBottleController;
    // Start is called before the first frame update
    void Start()
    {
        objectOutline = GetComponent<ObjectOutline>();
        requireColorPanel = GameObject.Find("RequireColor");
        circleRing = Resources.Load("Prefabs/Effects/CircleRing") as GameObject;
        inkBottleController = FindAnyObjectByType<InkBottleController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        //実体化されていたら処理を行わない
        if (objectOutline.IsMaterialized.Value) return;
        if (!JudgeIfObjectCanBeMaterialized())
        {
            transform.DOShakePosition(0.1f, 0.5f, 90, 90);
            SEManager.Instance.Play(SEPath.FAILURE);
            return;
        }

        objectOutline.Materialize();

        requireColorPanel.transform.localPosition = new Vector3(10000, 0, 0);

        StartCoroutine(StartClickAnimation());

        if (GetComponent<BoxCollider2D>())
        {
            if (!objectOutline.maintainingTrigger) GetComponent<BoxCollider2D>().isTrigger = false;
        }
        else
        {
            if (!objectOutline.maintainingTrigger) GetComponent<PolygonCollider2D>().isTrigger = false;
        }
    }

    /// <summary>
    /// インクが足りるかどうか
    /// </summary>
    /// <returns></returns>
    bool JudgeIfObjectCanBeMaterialized()
    {
        for (int i = 0; i < objectOutline.requireInk.Count; i++)
        {
            if (objectOutline.requireInk[i] > PlayerData.Instance.inkRemainingAmount[i].remainingAmount)
            {
                return false;
            }
        }

        return true;
    }

    IEnumerator StartClickAnimation()
    {
        GameObject creaturePanel = Instantiate(Resources.Load("Prefabs/CreaturePanel") as GameObject, 
            GameObject.Find("CreatureList").GetComponentInChildren<ContentSizeFitter>().transform);
        creaturePanel.GetComponent<CreaturePanelController>().id = id;
        creaturePanel.GetComponent<CreaturePanelController>().UpdateContent();

        GameObject g = Instantiate(circleRing);
        g.transform.position = transform.position;
        Destroy(g, 3f);
        SEManager.Instance.Play(SEPath.WATER_DROPLETS);

        yield return new WaitForSeconds(1);
        Destroy(GetComponent<LineRenderer>());

        DOVirtual.Float(0, 1, 2, x => GetComponent<SpriteRenderer>().material.SetFloat("_Threshold", x));

        for (int i = 0;i < 3;i++)
        {
            PlayerData.Instance.inkRemainingAmount[i].remainingAmount -= objectOutline.requireInk[i];
        }
        inkBottleController.ChangeInkRemaining();

        transform.tag = "Touchable";
        if (objectOutline.tag != "") gameObject.tag = objectOutline.tag;

        //例外処理
        if (GetComponent<AreaEffector2D>() != null)
        {
            GetComponent<AreaEffector2D>().forceMagnitude = 70;
        }
        yield return null;
    }
}
