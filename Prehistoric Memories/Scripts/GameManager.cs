using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.Events;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI quitText;
    [SerializeField] float quitKeyPressDuration;
    float currentQuitKeyPressTime = 0f;
    static GameManager _gameManager;
    public static GameManager Instance
    {
        get
        {
            return _gameManager;
        }
    }

    private void Awake()
    {
        if (_gameManager == null)
        {
            _gameManager = this;
            SceneManager.LoadSceneAsync("Title", LoadSceneMode.Additive);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ゲーム終了処理
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitText.DOKill();
            quitText.DOFade(1, 2).SetEase(Ease.Linear);
            quitText.text = "終了中";
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            if (currentQuitKeyPressTime > quitKeyPressDuration / 4 * 3)
            {
                quitText.text = "終了中...";
            }
            else if (currentQuitKeyPressTime > quitKeyPressDuration / 2)
            {
                quitText.text = "終了中..";
            }
            else if (currentQuitKeyPressTime > quitKeyPressDuration / 4)
            {
                quitText.text = "終了中.";
            }
            currentQuitKeyPressTime += Time.deltaTime;

            if (currentQuitKeyPressTime > quitKeyPressDuration)
            {
                //セーブ処理
                System.Diagnostics.Process.Start(Application.dataPath.Replace("_Data", ".exe"));
                Application.Quit();
            }
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            currentQuitKeyPressTime = 0f;
            quitText.DOKill();
            quitText.DOFade(0, 2).SetEase(Ease.Linear);
        }
    }
}
