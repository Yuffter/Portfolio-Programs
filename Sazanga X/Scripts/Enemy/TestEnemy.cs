using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using KanKikuchi.AudioManager;

public class TestEnemy : MonoBehaviour, IEnemy
{
    public float hp { get; set; } = 4;
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
        SEManager.Instance.Play(SEPath.PLAYER_BULLET_1,0.3f);
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
        Instantiate(deadEffect, transform.position, quaternion.identity);
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
}
