using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using KanKikuchi.AudioManager;
using UnityEngine;

public class KamikazeGenerator : MonoBehaviour
{
    [SerializeField] GameObject kamikazePrefab;
    [SerializeField] GameObject exclamationMarkObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate(Vector3 position,float waitTime,float duration) {
        StartCoroutine(GenerateCoroutine(position,waitTime,duration));
    }

    IEnumerator GenerateCoroutine(Vector3 position,float waitTime,float duration) {
        GameObject exMark = Instantiate(exclamationMarkObj,new Vector3(position.x,position.y,10),Quaternion.identity);
        SEManager.Instance.Play(SEPath.WARNING_APPEAR,0.7f);

        yield return new WaitForSeconds(waitTime);

        Destroy(exMark);
        GameObject kamikaze = Instantiate(kamikazePrefab,position,Quaternion.Euler(90,0,0));
        kamikaze.transform.localScale = Vector3.zero;
        kamikaze.transform.DOScale(Vector3.one,0.5f);
        kamikaze.transform.DOMoveZ(-8,duration).SetEase(Ease.Linear).OnComplete(() => Destroy(kamikaze));
        SEManager.Instance.Play(SEPath.KAMIKAZE);
    }
}
