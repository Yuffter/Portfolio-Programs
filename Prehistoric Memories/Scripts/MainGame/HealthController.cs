using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] GameObject heart;
    [SerializeField] Material fillSingleColorMat;
    List<GameObject> hearts = new List<GameObject>();

    readonly Vector2 START_POSITION = new Vector2(60f, -60f);
    const float SPACING = 80f;
    int currentHealth = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateHealthBar()
    {
        StartCoroutine(_GenerateHealthBar());
    }

    IEnumerator _GenerateHealthBar()
    {
        for (int i = 0;i < StagesData.Instance.stagesData[StagesData.Instance.currentStageIndex].PlayerHp;i++)
        {
            IncreaseHealth();

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void IncreaseHealth()
    {
        currentHealth++;
        GameObject g = Instantiate(heart, transform);
        g.transform.localScale = Vector3.zero;
        g.GetComponent<RectTransform>().anchoredPosition = START_POSITION + new Vector2(SPACING * hearts.Count, 0);

        g.GetComponent<Image>().material = new Material(fillSingleColorMat);
        hearts.Add(g);

        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() => g.GetComponent<Image>().material.SetFloat("_Threshold", 1f))
            .Append(g.transform.DOScale(new Vector3(1.2f, 1.2f, 1), 0.2f))
            .Append(DOVirtual.Float(1f, 0f, 1f, x => g.GetComponent<Image>().material.SetFloat("_Threshold", x)));
    }

    public void DecreaseHealth()
    {
        currentHealth--;
        Animator animator = hearts[0].GetComponent<Animator>();
        animator.SetBool("IsBreak", true);

        Sequence seq = DOTween.Sequence();
        seq.AppendInterval(2f)
            .AppendCallback(() =>
            {
                Destroy(hearts[0]);
                hearts.RemoveAt(0);
            })
            .AppendInterval(0.1f)
            .AppendCallback(() =>
            {
                for (int i = 0; i < hearts.Count; i++)
                {
                    hearts[i].GetComponent<RectTransform>().DOAnchorPos(START_POSITION + new Vector2(SPACING * i, 0), 0.5f);
                }
            });
        
    }

    public bool CheckIsDeath()
    {
        print(currentHealth);
        return currentHealth == 0;
    }
}
