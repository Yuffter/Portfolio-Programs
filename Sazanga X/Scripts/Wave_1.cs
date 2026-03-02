using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using KanKikuchi.AudioManager;
using UniRx.Triggers;
using UniRx;

public class Wave_1 : MonoBehaviour
{
    [SerializeField] WeekestEnemyGenerator weekestEnemyGenerator;
    [SerializeField] KamikazeGenerator kamikazeGenerator;
    [SerializeField] CirclingEnemyGenerator circlingEnemyGenerator;
    [SerializeField] GameObject player;
    [SerializeField] RectTransform mission;
    [SerializeField] WaveChangeAnimation waveChangeAnimation;
    [SerializeField] GameObject wave_2_obj;
    bool isCleared = false;
    GameObject[] enemies = new GameObject[100];
    Vector3[] patterns = new Vector3[] {
        new Vector3(-5,5,20),
        new Vector3(0,5,20),
        new Vector3(5,5,20),
        new Vector3(-5,0,20),
        new Vector3(0,0,20),
        new Vector3(5,0,20),
        new Vector3(-5,-5,20),
        new Vector3(0,-5,20),
        new Vector3(5,-5,20)
    };
    float tmpCount = 0f;
    const int OWN_TIME = 50;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameTimeManager.CurrentTime == 1 * OWN_TIME)
        {
            enemies[0] = weekestEnemyGenerator.Generate(new Vector3(5,0),11);
        }

        if (GameTimeManager.CurrentTime == 2 * OWN_TIME)
        {
            enemies[1] = weekestEnemyGenerator.Generate(new Vector3(-5,0),10);
        }

        if (GameTimeManager.CurrentTime == 3 * OWN_TIME)
        {
            enemies[2] = weekestEnemyGenerator.Generate(new Vector3(0,0),3,0.2f);
        }

        // if (GameTimeManager.CurrentTime == 4 * OWN_TIME)
        // {
        //     if (enemies[0] != null) enemies[0].transform.DORotate(new Vector3(0, 360, 0), 1, RotateMode.FastBeyond360).SetLoops(2).SetLink(enemies[0]);
        //     if (enemies[1] != null) enemies[1].transform.DORotate(new Vector3(0, 360, 0), 1, RotateMode.FastBeyond360).SetLoops(2).SetLink(enemies[1]);
        // }

        if (GameTimeManager.CurrentTime == 6 * OWN_TIME) {
            enemies[3] = weekestEnemyGenerator.Generate(new Vector3(-5,5),6);
        }

        if (GameTimeManager.CurrentTime == 6.2f * OWN_TIME) {
            enemies[4] = weekestEnemyGenerator.Generate(new Vector3(0,5),5.8f);
        }

        if (GameTimeManager.CurrentTime == 6.4f * OWN_TIME) {
            enemies[5] = weekestEnemyGenerator.Generate(new Vector3(5,5),5.6f);
        }

        if (GameTimeManager.CurrentTime == 7.4f * OWN_TIME) {
            enemies[6] = weekestEnemyGenerator.Generate(new Vector3(-5,-5),4.6f);
        }

        if (GameTimeManager.CurrentTime == 7.6f * OWN_TIME) {
            enemies[7] = weekestEnemyGenerator.Generate(new Vector3(0,-5),4.4f);
        }

        if (GameTimeManager.CurrentTime == 7.8f * OWN_TIME) {
            enemies[8] = weekestEnemyGenerator.Generate(new Vector3(5,-5),4.2f);
        }

        if (GameTimeManager.CurrentTime == 9f * OWN_TIME) {
            kamikazeGenerator.Generate(new Vector3(0,0,20),1,2);
        }

        if (GameTimeManager.CurrentTime == 13f * OWN_TIME) {
            enemies[9] = weekestEnemyGenerator.Generate(new Vector3(-10,5),6,0.05f);
            enemies[10] = weekestEnemyGenerator.Generate(new Vector3(5,10),6,0.05f);
            enemies[11] = weekestEnemyGenerator.Generate(new Vector3(10,-5),6,0.05f);
            enemies[12] = weekestEnemyGenerator.Generate(new Vector3(-5,-10),6,0.05f);
            kamikazeGenerator.Generate(new Vector3(0,0,20),1,2);
        }

        if (GameTimeManager.CurrentTime == 14.5f * OWN_TIME) {
            enemies[9].transform.DOMoveX(10,4).SetEase(Ease.Linear).SetLink(enemies[9]);
            enemies[10].transform.DOMoveY(-10,4).SetEase(Ease.Linear).SetLink(enemies[10]);
            enemies[11].transform.DOMoveX(-10,4).SetEase(Ease.Linear).SetLink(enemies[11]);
            enemies[12].transform.DOMoveY(10,4).SetEase(Ease.Linear).SetLink(enemies[12]);
        }

        if (GameTimeManager.CurrentTime == 18 * OWN_TIME) {
            kamikazeGenerator.Generate(new Vector3(-5,0,20),2,2);
            kamikazeGenerator.Generate(new Vector3(0,0,20),2,2);
            kamikazeGenerator.Generate(new Vector3(5,0,20),2,2);
            kamikazeGenerator.Generate(new Vector3(0,5,20),2,2);
            kamikazeGenerator.Generate(new Vector3(0,-5,20),2,2);
        }

        if (GameTimeManager.CurrentTime == 19 * OWN_TIME) {
            kamikazeGenerator.Generate(new Vector3(-5,5,20),2,2);
            kamikazeGenerator.Generate(new Vector3(5,5,20),2,2);
            kamikazeGenerator.Generate(new Vector3(-5,-5,20),2,2);
            kamikazeGenerator.Generate(new Vector3(5,-5,20),2,2);
        }

        if (GameTimeManager.CurrentTime == 22 * OWN_TIME) {
            enemies[13] = weekestEnemyGenerator.Generate(new Vector3(-5,-5),100,0.3f);
            enemies[14] = weekestEnemyGenerator.Generate(new Vector3(5,-5),100,0.3f);
            enemies[15] = weekestEnemyGenerator.Generate(new Vector3(-5,5),100,0.3f);
            enemies[16] = weekestEnemyGenerator.Generate(new Vector3(5,5),100,0.3f);
            mission.DOAnchorPosY(-55,1);
            SEManager.Instance.Play(SEPath.OPEN_MISSION);
        }
        if (enemies[13] != null) {
            Vector3 angle = (player.transform.position - enemies[13].transform.position).normalized;
            angle.z = 0f;
            enemies[13].transform.transform.position += angle * 5 * Time.deltaTime;
        }
        if (enemies[14] != null) {
            Vector3 angle = (player.transform.position - enemies[14].transform.position).normalized;
            angle.z = 0f;
            enemies[14].transform.transform.position += angle * 5 * Time.deltaTime;
        }
        if (enemies[15] != null) {
            Vector3 angle = (player.transform.position - enemies[15].transform.position).normalized;
            angle.z = 0f;
            enemies[15].transform.transform.position += angle * 5 * Time.deltaTime;
        }
        if (enemies[16] != null) {
            Vector3 angle = (player.transform.position - enemies[16].transform.position).normalized;
            angle.z = 0f;
            enemies[16].transform.transform.position += angle * 5 * Time.deltaTime;
        }

        if (22.5f * OWN_TIME <= GameTimeManager.CurrentTime && enemies[13] == null && enemies[14] == null && enemies[15] == null && enemies[16] == null && !isCleared) {
            isCleared = true;
            mission.DOAnchorPosY(100,1);
            ChangeNextWave();
        }

        if (22 * OWN_TIME <= GameTimeManager.CurrentTime && !isCleared) {
            tmpCount += Time.deltaTime;
            if (tmpCount >= 1) {
                tmpCount = 0f;
                for (int i = 0;i < 2;i++) kamikazeGenerator.Generate(patterns[Random.Range(0,9)],1,2);
            }
        }
    }

    async void ChangeNextWave() {
        GameTimeManager.ResetTime();
        await waveChangeAnimation.Wave_2();
        GameTimeManager.IsStarting = true;
        Instantiate(wave_2_obj);
        Destroy(gameObject);
    }
}