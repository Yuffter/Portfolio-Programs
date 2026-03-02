using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletController_1 : MonoBehaviour,IBullet
{
    public float damageAmount {get;set;} = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            FindAnyObjectByType<PlayerHPModel>().Damage(damageAmount);
        }
    }
}
