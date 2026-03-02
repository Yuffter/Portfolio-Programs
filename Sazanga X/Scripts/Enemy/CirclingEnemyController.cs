using System.Collections;
using System.Collections.Generic;
using KanKikuchi.AudioManager;
using UnityEngine;
using DG.Tweening;

public class CirclingEnemyController : MonoBehaviour,IEnemy
{
    Vector3 initialPosition;
    float rotateSpeed;
    float aliveDuration;

    public float hp {get;set;} = 5;
    [SerializeField] float spawnInterval = 1f;
    [SerializeField] float radius = 2f;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject deadEffect;
    float currentTime = 0f;
    float currentAliveTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,rotateSpeed * Time.deltaTime));

        currentTime += Time.deltaTime;
        currentAliveTime += Time.deltaTime;
        if (currentTime > spawnInterval) {
            currentTime = 0f;
            Attack();
        }

        if (currentAliveTime >= aliveDuration) {
            transform.DOMoveZ(30,0.1f).SetLink(gameObject).OnComplete(() => Destroy(gameObject));
            SEManager.Instance.Play(SEPath.WARP);
        }
    }

    public void Init(Vector3 initialPosition,float rotateSpeed,float aliveDuration,float spawnInterval) {
        this.initialPosition = initialPosition;
        this.rotateSpeed = rotateSpeed;
        this.aliveDuration = aliveDuration;
        this.spawnInterval = spawnInterval;

        transform.position = new Vector3(this.initialPosition.x,this.initialPosition.y,30);
        transform.DOScale(Vector3.zero, 0);
        transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutCubic);
        transform.DOMoveZ(17, 0.2f).SetEase(Ease.OutCubic);
    }

    public void Attack()
    {
        SEManager.Instance.Play(SEPath.PLAYER_BULLET_1,0.3f);
        int bulletCount = 5;
        float angle = 360 / bulletCount;
        for (int i = 0;i < bulletCount;i++) {
            float currentAngle = angle * i;
            Vector3 pos = new Vector3(
                transform.position.x + radius * Mathf.Cos((transform.eulerAngles.y + currentAngle) * Mathf.Deg2Rad),
                transform.position.y,
                transform.position.z + radius * Mathf.Sin((transform.eulerAngles.y + currentAngle) * Mathf.Deg2Rad));

            GameObject b = Instantiate(bullet,pos,Quaternion.identity);
            b.transform.LookAt(transform);
            b.transform.Rotate(new Vector3(0,180,0));
            b.GetComponent<CirclingEnemyBulletController>().Angle = currentAngle;
            b.GetComponent<CirclingEnemyBulletController>().Radius = radius;
            b.GetComponent<CirclingEnemyBulletController>().CenterPosition = transform.position;
            b.GetComponent<CirclingEnemyBulletController>().EulerAngleY = transform.eulerAngles.y;
        }
    }

    public void Dead()
    {
        Instantiate(deadEffect, transform.position, Quaternion.identity);
        SEManager.Instance.Play(SEPath.EXPLOSION);
        Destroy(gameObject);
    }

    public void Damage(float damageAmount)
    {
        hp -= damageAmount;
        SEManager.Instance.Play(SEPath.DAMAGE_1);

        if (hp <= 0) Dead();
    }
}
