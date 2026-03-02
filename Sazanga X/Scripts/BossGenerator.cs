using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BossGenerator : MonoBehaviour
{
    [SerializeField] GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject Generate() {
        GameObject boss = Instantiate(enemy);
        boss.GetComponent<BossController>().Generate();
        return boss;
    }
}
