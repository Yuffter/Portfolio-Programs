using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;
using DG.Tweening;

public class WeekestEnemyController : MonoBehaviour,IEnemy
{
    public float hp {get;set;} = 5f;
    public GameObject tama1;
    public GameObject deadEffect;
    float currentTime = 0f;
    public float spawnInterval = 0.3f;
    public Vector3 distanceFromCenter;

    public void Attack()
    {
        currentTime = 0f;

        float arriveTime = 1f;
        GameObject bullet = Instantiate(tama1, transform.position + distanceFromCenter, Quaternion.identity);
        SEManager.Instance.Play(SEPath.PLAYER_BULLET_1,0.1f);
        bullet.transform.DOMoveZ(-20, arriveTime).SetRelative().OnComplete(() =>
        {
            Destroy(bullet);
        });
    }

    public void Damage(float damageAmount)
    {
        hp -= damageAmount;
        SEManager.Instance.Play(SEPath.DAMAGE_1);

        if (hp <= 0) Dead();
    }

    public void Dead()
    {
        Instantiate(deadEffect, transform.position, Quaternion.identity);
        SEManager.Instance.Play(SEPath.EXPLOSION);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > spawnInterval)
        {
            Attack();
        }
    }

    public void Init(Vector3 initialPosition,float aliveDuration,float spawnInterval = 1f) {
        this.spawnInterval = spawnInterval;
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() => {
            transform.position = new Vector3(initialPosition.x,initialPosition.y,30);
            transform.localScale = Vector3.zero;
        })
        .Append(transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutCubic))
        .Join(transform.DOMoveZ(17, 0.2f).SetEase(Ease.OutCubic))
        .AppendInterval(aliveDuration)
        .AppendCallback(() => SEManager.Instance.Play(SEPath.WARP))
        .Append(transform.DOMoveZ(30,0.1f))
        .OnComplete(() => Destroy(gameObject)).SetLink(gameObject);
    }
}
