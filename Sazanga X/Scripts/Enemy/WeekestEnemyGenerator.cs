using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class WeekestEnemyGenerator : MonoBehaviour
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

    public GameObject Generate(Vector3 initialPosition,float aliveDuration,float spawnInterval = 1f) {
        GameObject g = Instantiate(enemy);
        SEManager.Instance.Play(SEPath.WARP);
        g.GetComponent<WeekestEnemyController>().Init(initialPosition,aliveDuration,spawnInterval);
        return g;
    }
}
