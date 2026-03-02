using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InkBottleController : MonoBehaviour
{
    [SerializeField] List<GameObject> bottles = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeInkRemaining()
    {
        for (int i = 0;i < bottles.Count;i++)
        {
            GameObject currentBottle = bottles[i];
            GameObject ink = currentBottle.transform.Find("Mask").Find("Bar").gameObject;
            TextMeshProUGUI inkRemainingLabel = currentBottle.transform.Find("Mask").Find("Remaining").GetComponent<TextMeshProUGUI>();

            ink.transform.DOKill(true);
            ink.transform.DOLocalMoveY(-94 + (94 * (PlayerData.Instance.inkRemainingAmount[i].remainingAmount / 100f)), 1).SetEase(Ease.OutCubic);
            inkRemainingLabel.text = Mathf.Floor(PlayerData.Instance.inkRemainingAmount[i].remainingAmount).ToString();
        }
    }

    public void PlayBottlesAppearanceAnimation()
    {
        StartCoroutine(_PlayBottlesAppearanceAnimation());
    }

    IEnumerator _PlayBottlesAppearanceAnimation()
    {
        for (int i = 0; i < PlayerData.Instance.inkRemainingAmount.Count; i++)
        {
            PlayerData.Instance.inkRemainingAmount[i].remainingAmount = 0;
        }
        ChangeInkRemaining();

        for (int i = 0; i < PlayerData.Instance.inkRemainingAmount.Count; i++)
        {
            bottles[i].transform.DOLocalMoveY(-200, 1).SetRelative().SetEase(Ease.InOutBack);
            yield return new WaitForSeconds(0.1f);
        }
        
        foreach (var c in PlayerData.Instance.inkRemainingAmount)
        {
            DOVirtual.Float(0, 100, 1, x =>
            {
                c.remainingAmount = x;
                ChangeInkRemaining();
            });
            yield return new WaitForSeconds(0.5f);
        }
    }
}
