using UnityEngine;
using System.Collections;
using Unity.Mathematics;

public class BulletController_1 : MonoBehaviour,IBullet
{
    public float damageAmount { get; set; } = 1f;
    public GameObject damageEffect;

    void Update()
    {
        transform.Translate(0, 0, 30 * Time.deltaTime);

        if (transform.position.z > 40)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IEnemy>() != null) {
            IEnemy enemy = other.gameObject.GetComponent<IEnemy>();
            enemy.Damage(damageAmount);
            Instantiate(damageEffect,transform.position,quaternion.identity);
            Destroy(gameObject);
        }
    }
}