using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletController : MonoBehaviour,IBullet
{
    public float damageAmount {get;set;} = 40;
    [SerializeField] GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            FindAnyObjectByType<PlayerHPModel>().Damage(damageAmount);
            Instantiate(explosion,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
