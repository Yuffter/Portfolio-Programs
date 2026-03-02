using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameInitialization : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] InkBottleController inkBottleController;
    [SerializeField] ArtPieceGenerator artPieceGenerator;
    [SerializeField] RemainingPieceModel remainingPieceModel;
    [SerializeField] HealthController healthController;
    [SerializeField] MainGameManager mainGameManager;

    private void Awake()
    {
        //絵画ピースを作成および設置
        artPieceGenerator.Generate();

        //残りピース数を表示させる
        remainingPieceModel.SetRemainingPieceCount();
    }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(Initialize());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Initialize()
    {
        DOTween.PauseAll();
        //プレイヤーの初期位置設定
        player.GetComponent<Player>().playerRespawn.Respawn(StagesData.Instance.stagesData[StagesData.Instance.currentStageIndex].playerSpawnPosition);
        player.GetComponent<Player>().DisableMoving();

        

        

        //インクボトルを画面内に遷移させる
        inkBottleController.PlayBottlesAppearanceAnimation();

        //体力バーを生成
        healthController.GenerateHealthBar();

        yield return new WaitForSeconds(1f);
        
        mainGameManager.StartControllingIntro();
    }
}
