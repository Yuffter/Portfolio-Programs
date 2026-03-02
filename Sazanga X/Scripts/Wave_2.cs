using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Wave_2 : MonoBehaviour
{
    [SerializeField] WeekestEnemyGenerator weekestEnemyGenerator;
    [SerializeField] KamikazeGenerator kamikazeGenerator;
    [SerializeField] CirclingEnemyGenerator circlingEnemyGenerator;
    [SerializeField] GameObject player;
    [SerializeField] WaveChangeAnimation waveChangeAnimation;
    [SerializeField] GameObject wave_3_obj;

    bool isCleared = false;
    GameObject[] enemies = new GameObject[100];
    Vector3[] patterns = new Vector3[] {
        new Vector3(-5,5,17),
        new Vector3(0,5,17),
        new Vector3(5,5,17),
        new Vector3(-5,0,17),
        new Vector3(0,0,17),
        new Vector3(5,0,17),
        new Vector3(-5,-5,17),
        new Vector3(0,-5,17),
        new Vector3(5,-5,17)
    };
    const int OWN_TIME = 50;

    float tmp_count = 0f;
    // Start is called before the first frame update
    void Start()
    {
        weekestEnemyGenerator = FindAnyObjectByType<WeekestEnemyGenerator>();
        kamikazeGenerator = FindAnyObjectByType<KamikazeGenerator>();
        circlingEnemyGenerator = FindAnyObjectByType<CirclingEnemyGenerator>();
        player = GameObject.Find("Player");
        waveChangeAnimation = FindAnyObjectByType<WaveChangeAnimation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if (GameTimeManager.CurrentTime == 1 * OWN_TIME) {
            weekestEnemyGenerator.Generate(new Vector3(0,0,17),8,0.5f);
        }
        if (GameTimeManager.CurrentTime == 2 * OWN_TIME) {
            weekestEnemyGenerator.Generate(new Vector3(5,0,17),7,0.5f);
        }
        if (GameTimeManager.CurrentTime == 3 * OWN_TIME) {
            weekestEnemyGenerator.Generate(new Vector3(-5,0,17),6,0.5f);
        }
        if (GameTimeManager.CurrentTime == 4 * OWN_TIME) {
            circlingEnemyGenerator.Generate(new Vector3(0,5,17),90,5,0.05f);
        }
        if (GameTimeManager.CurrentTime == 5 * OWN_TIME) {
            circlingEnemyGenerator.Generate(new Vector3(0,-5,17),90,5,0.05f);
        }

        if (GameTimeManager.CurrentTime == 6 * OWN_TIME) {
            kamikazeGenerator.Generate(new Vector3(-5,5,17),2,2);
            kamikazeGenerator.Generate(new Vector3(5,5,17),2,2);
            kamikazeGenerator.Generate(new Vector3(-5,-5,17),2,2);
            kamikazeGenerator.Generate(new Vector3(5,-5,17),2,2);
        }

        if (GameTimeManager.CurrentTime == 9 * OWN_TIME) {
            circlingEnemyGenerator.Generate(new Vector3(-5,5,17),90,5,0.05f);
            weekestEnemyGenerator.Generate(new Vector3(0,5,17),5,1f);
            circlingEnemyGenerator.Generate(new Vector3(5,5,17),90,5,0.05f);
        }
        if (GameTimeManager.CurrentTime == 10 * OWN_TIME) {
            for (int i = -1;i < 1;i++) {
                for (int j = -1;j < 2;j++) {
                    kamikazeGenerator.Generate(new Vector3(5 * j,5 * i,17),1,2);
                }
            }
        }

        if (GameTimeManager.CurrentTime == 14 * OWN_TIME) {
            circlingEnemyGenerator.Generate(new Vector3(-5,-5,17),90,5,0.05f);
            weekestEnemyGenerator.Generate(new Vector3(0,-5,17),5,1f);
            circlingEnemyGenerator.Generate(new Vector3(5,-5,17),90,5,0.05f);
        }

        if (GameTimeManager.CurrentTime == 15 * OWN_TIME) {
            for (int i = 0;i < 2;i++) {
                for (int j = -1;j < 2;j++) {
                    kamikazeGenerator.Generate(new Vector3(5 * j,5 * i,17),1,2);
                }
            }
        }

        if (GameTimeManager.CurrentTime == 20 * OWN_TIME) {
            enemies[0] = weekestEnemyGenerator.Generate(new Vector3(-5,5,17),8,0.3f);
            enemies[1] = weekestEnemyGenerator.Generate(new Vector3(5,-5,17),8,0.3f);
            enemies[2] = weekestEnemyGenerator.Generate(new Vector3(0,0,17),8,0.3f);
        }

        if (GameTimeManager.CurrentTime == 21 * OWN_TIME) {
            enemies[3] = circlingEnemyGenerator.Generate(new Vector3(-5,10,17),90,7,0.05f);
            enemies[4] = circlingEnemyGenerator.Generate(new Vector3(5,10,17),90,7,0.05f);
        }

        if (GameTimeManager.CurrentTime == 22 * OWN_TIME) {
            enemies[3].transform.DOMoveY(-10,5f).SetEase(Ease.Linear);
            enemies[4].transform.DOMoveY(-10,5f).SetEase(Ease.Linear);
        }

        if (22 * OWN_TIME <= GameTimeManager.CurrentTime && GameTimeManager.CurrentTime <= 27 * OWN_TIME) {
            tmp_count += Time.deltaTime;
            if (tmp_count >= 0.5f) {
                tmp_count = 0f;

                if (enemies[0] != null) enemies[0].transform.DOMove(patterns[Random.Range(0,3)],0.3f).SetEase(Ease.Linear).SetLink(enemies[0]);
                if (enemies[2] != null) enemies[2].transform.DOMove(patterns[Random.Range(3,6)],0.3f).SetEase(Ease.Linear).SetLink(enemies[2]);
                if (enemies[1] != null) enemies[1].transform.DOMove(patterns[Random.Range(6,9)],0.3f).SetEase(Ease.Linear).SetLink(enemies[1]);
            }
        }

        if (GameTimeManager.CurrentTime == 28 * OWN_TIME) {
            tmp_count = 0f;
            for (int i = -1;i < 2;i++) circlingEnemyGenerator.Generate(new Vector3(5 * i,5,17),90,10,0.05f);
            for (int i = -1;i < 2;i++) circlingEnemyGenerator.Generate(new Vector3(5 * i,-5,17),90,10,0.05f);
        }

        if (29 * OWN_TIME <= GameTimeManager.CurrentTime && GameTimeManager.CurrentTime <= 36 * OWN_TIME) {
            tmp_count += Time.deltaTime;
            if (tmp_count >= 2f) {
                tmp_count = 0f;
                kamikazeGenerator.Generate(new Vector3(-5,0,17),1,2);
                kamikazeGenerator.Generate(new Vector3(5,0,17),1,2);
                DOVirtual.DelayedCall(1f,() => kamikazeGenerator.Generate(new Vector3(0,0,17),1,2));
            }
        }

        if (GameTimeManager.CurrentTime == 38 * OWN_TIME) {
            ChangeNextWave();
        }
    }

    async void ChangeNextWave() {
        GameTimeManager.ResetTime();
        await waveChangeAnimation.Wave_3();
        GameTimeManager.IsStarting = true;
        Instantiate(wave_3_obj);
        Destroy(gameObject);
    }
}
