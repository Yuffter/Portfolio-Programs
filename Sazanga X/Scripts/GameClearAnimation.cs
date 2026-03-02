using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using System;
using KanKikuchi.AudioManager;
using UnityEngine.UI;
using TMPro;
using UniRx.Triggers;
using UniRx;
using UnityEngine.SceneManagement;

public class GameClearAnimation : MonoBehaviour
{
    [SerializeField] GameObject grid;
    [SerializeField] List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();
    [SerializeField] GameObject gameClearTitle;
    [SerializeField] TMP_Text backToTitleLabel;
    [SerializeField] GameObject player;
    [SerializeField] GameEvent gameClearEvent;
    bool canBack = false;
    // Start is called before the first frame update
    void Start()
    {
        this.UpdateAsObservable()
            .Where(_ => canBack)
            .Where(_ => Input.GetKeyDown(KeyCode.Return))
            .First()
            .Subscribe(_ =>
            {
                SceneManager.LoadScene("Title");
            });
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z)) Play();
    }

    public async void Play() {
        await UniTask.Delay(TimeSpan.FromSeconds(3f));
        player.transform.DOMove(new Vector3(0, 0, -2.19f), 1);
        await UniTask.Delay(TimeSpan.FromSeconds(3.5f));
        grid.GetComponent<SpriteRenderer>().DOFade(0, 1);
        BGMManager.Instance.Play(BGMPath.GAME_COMPLETE,1,0,1,false);
        cameras[0].Priority = 11;
        await UniTask.Delay(TimeSpan.FromSeconds(1));
        cameras[1].Priority = 12;
        await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
        cameras[2].Priority = 13;
        await player.transform.DOMoveZ(15, 2).AsyncWaitForCompletion();
        await gameClearTitle.GetComponent<Image>().DOFillAmount(1, 1).AsyncWaitForCompletion();
        await backToTitleLabel.DOFade(1, 1).AsyncWaitForCompletion();
        canBack = true;
    }
}
