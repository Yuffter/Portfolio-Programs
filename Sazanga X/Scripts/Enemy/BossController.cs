using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using KanKikuchi.AudioManager;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class BossController : MonoBehaviour,IEnemy
{
    Volume volume;
    ChromaticAberration chromaticAberration;
    LensDistortion lensDistortion;
    FilmGrain filmGrain;
    Image hpBar;
    RectTransform bossBar;
    [SerializeField] GameObject deadEffect;
    [SerializeField] Material bossLightMat;
    [SerializeField] GameObject bullet;
    [SerializeField] GameEvent bossDeadEvent;

    public float hp {get;set;} = 100;
    float currentTime = 0f;
    float attackInterval = 10f;

    void Awake() {
        volume = FindAnyObjectByType<Volume>();
        volume.profile.TryGet(out chromaticAberration);
        volume.profile.TryGet(out lensDistortion);
        volume.profile.TryGet(out filmGrain);
        hpBar = GameObject.Find("BossBar").transform.Find("Bar").GetComponent<Image>();
        bossBar = GameObject.Find("BossBar").GetComponent<RectTransform>();
    }
    // Start is called before the first frame update
    void Start()
    {
        bossLightMat.DOColor(Color.red * 10,"_EmissionColor",0);
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.fillAmount = hp / 100f;

        currentTime += Time.deltaTime;
        if (currentTime >= attackInterval) {
            Attack();
            currentTime = 0f;
            attackInterval = Random.Range(9,10);
        }
    }

    public async void Generate() {
        // 初期設定
        transform.position = new Vector3(0,2,30);
        transform.eulerAngles = new Vector3(90,0,0);
        transform.localScale = Vector3.zero;

        chromaticAberration.intensity.value = 1f;
        lensDistortion.intensity.value = -1f;
        SEManager.Instance.Play(SEPath.WARP,1,0,0.5f);
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMoveZ(20,0.2f).SetEase(Ease.OutCubic))
        .Join(transform.DOScale(new Vector3(0.8f,0.8f,0.8f),0.2f).SetEase(Ease.OutCubic))
        .Join(DOVirtual.Float(1,0,1,x => chromaticAberration.intensity.value = x))
        .Join(DOVirtual.Float(-1,0,1,x => lensDistortion.intensity.value = x))
        .Join(DOVirtual.Float(0,1,1,x => filmGrain.intensity.value = x));
    }

    public void Attack()
    {
        Sequence seq = DOTween.Sequence();
        SEManager.Instance.Play(SEPath.CHARGE_SHOOT);
        seq.Append(bossLightMat.DOColor(Color.red * 5000,"_EmissionColor",3))
        .AppendCallback(() => {
            SEManager.Instance.Play(SEPath.SHOOT_BOSS_BULLET);
            SEManager.Instance.Play(SEPath.KAMIKAZE);
            GameObject b = Instantiate(bullet,transform.position + new Vector3(0,-2,-1),Quaternion.identity);
            b.transform.DOMoveZ(-30,5).SetEase(Ease.Linear).OnComplete(() => Destroy(b));
            bossLightMat.DOColor(Color.red * 10,"_EmissionColor",1);
        });
    }

    public void Dead()
    {
        Instantiate(deadEffect, transform.position, Quaternion.identity);
        SEManager.Instance.Play(SEPath.EXPLOSION);
        bossBar.DOAnchorPosY(170,1);
        bossDeadEvent.Raise();
        Destroy(gameObject);
    }

    public void Damage(float damageAmount)
    {
        hp -= damageAmount;
        SEManager.Instance.Play(SEPath.DAMAGE_1);

        if (hp <= 0) Dead();
    }
}
