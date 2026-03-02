using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Wave_3 : MonoBehaviour
{
    [SerializeField] WeekestEnemyGenerator weekestEnemyGenerator;
    [SerializeField] KamikazeGenerator kamikazeGenerator;
    [SerializeField] CirclingEnemyGenerator circlingEnemyGenerator;
    [SerializeField] GameObject player;
    [SerializeField] BossGenerator bossGenerator;
    [SerializeField] RectTransform bossBar;

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

    float spawnIntervalOfWeekest = 5f,currentWeekestInterval = 0f;
    float spawnIntervalOfCircling = 5f,currentCirclingInterval = 0f;
    float spawnIntervalOfKamikaze = 5f,currentKamikazeInterval = 0f;
    float spawnIntervalOfGuardians = 4f,currentGueardiansInterval = 0f;
    // Start is called before the first frame update
    void Start()
    {
        weekestEnemyGenerator = FindAnyObjectByType<WeekestEnemyGenerator>();
        kamikazeGenerator = FindAnyObjectByType<KamikazeGenerator>();
        circlingEnemyGenerator = FindAnyObjectByType<CirclingEnemyGenerator>();
        bossGenerator = FindAnyObjectByType<BossGenerator>();
        bossBar = GameObject.Find("BossBar").GetComponent<RectTransform>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {
        if (GameTimeManager.CurrentTime == 1 * OWN_TIME) {
            enemies[0] = bossGenerator.Generate();
        }
        if (GameTimeManager.CurrentTime == 2 * OWN_TIME) {
            bossBar.DOAnchorPosY(0,1);
            enemies[0].transform.DOMoveX(-5,1).OnComplete(() =>
            {
                enemies[0].transform.DOMoveX(5, 5).SetEase(Ease.Linear).SetLink(enemies[0]).SetLoops(-1, LoopType.Yoyo);
            });
        }
        if (GameTimeManager.CurrentTime == 2 * OWN_TIME) {
            weekestEnemyGenerator.Generate(new Vector3(-5,0,17),10,0.5f);
            weekestEnemyGenerator.Generate(new Vector3(0,0,17),10,0.5f);
            weekestEnemyGenerator.Generate(new Vector3(5,0,17),10,0.5f);
        }

        if (GameTimeManager.CurrentTime >= 2 * OWN_TIME) {
            currentCirclingInterval += Time.deltaTime;
            currentKamikazeInterval += Time.deltaTime;
            currentWeekestInterval += Time.deltaTime;
            currentGueardiansInterval += Time.deltaTime;
        }

        if (currentCirclingInterval >= spawnIntervalOfCircling) {
            currentCirclingInterval = 0f;
            if ((int)(Random.Range(0,2)) == 0) {
                circlingEnemyGenerator.Generate(patterns[Random.Range(0,3)],90,3,0.07f);
            }
            else {
                circlingEnemyGenerator.Generate(patterns[Random.Range(6,9)],90,3,0.07f);
            }
        }

        if (currentKamikazeInterval >= spawnIntervalOfKamikaze) {
            currentKamikazeInterval = 0f;
            kamikazeGenerator.Generate(patterns[Random.Range(0,9)],1,2);
        }

        if (currentWeekestInterval >= spawnIntervalOfWeekest) {
            currentWeekestInterval = 0f;
            weekestEnemyGenerator.Generate(patterns[Random.Range(0,9)],3,0.5f);
        }

        if (currentGueardiansInterval >= spawnIntervalOfGuardians) {
            currentGueardiansInterval = 0f;
            weekestEnemyGenerator.Generate(new Vector3(-5,0,17),5,0.5f);
            weekestEnemyGenerator.Generate(new Vector3(0,0,17),5,0.5f);
            weekestEnemyGenerator.Generate(new Vector3(5,0,17),5,0.5f);
        }
    }
}
