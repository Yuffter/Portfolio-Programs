using System.Collections;
using System.Collections.Generic;
using KanKikuchi.AudioManager;
using UnityEngine;

public class KamikazeController : MonoBehaviour,IBullet
{
    GameObject smallAroundObjects,bigAroundObjects;
    [SerializeField] GameObject explosion;

    public float damageAmount {get;set;} = 20;

    // Start is called before the first frame update
    void Start()
    {
        // smallAroundObjects = transform.Find("SmallAroundObjects").gameObject;
        // bigAroundObjects = transform.Find("BigAroundObjects").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // smallAroundObjects.transform.Rotate(new Vector3(0,0,360 * Time.deltaTime));
        // bigAroundObjects.transform.Rotate(new Vector3(0,0,-360 * Time.deltaTime));
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            FindAnyObjectByType<PlayerHPModel>().Damage(damageAmount);
            Instantiate(explosion,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
