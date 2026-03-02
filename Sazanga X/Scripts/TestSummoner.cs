using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSummoner : MonoBehaviour
{
    [SerializeField] WeekestEnemyGenerator weekestEnemyGenerator;
    [SerializeField] CirclingEnemyGenerator circlingEnemyGenerator;
    [SerializeField] KamikazeGenerator kamikazeGenerator;
    [SerializeField] BossGenerator bossGenerator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            weekestEnemyGenerator.Generate(new Vector3(0,0,17),3,0.5f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            circlingEnemyGenerator.Generate(new Vector3(0,0,17),90,3,0.5f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            kamikazeGenerator.Generate(new Vector3(0,0,17),1,2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            bossGenerator.Generate();
        }
    }
}
