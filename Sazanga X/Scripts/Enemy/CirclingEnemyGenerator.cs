using System.Collections;
using System.Collections.Generic;
using KanKikuchi.AudioManager;
using UnityEngine;

public class CirclingEnemyGenerator : MonoBehaviour
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

    public GameObject Generate(Vector3 initialPosition,float rotateSpeed,float aliveDuration,float spawnInterval) {
        GameObject g = Instantiate(enemy);
        SEManager.Instance.Play(SEPath.WARP);
        g.GetComponent<CirclingEnemyController>().Init(initialPosition,rotateSpeed,aliveDuration,spawnInterval);
        return g;
    }
}
