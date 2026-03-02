using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Vector3? currentPlayerRespawnPosition = null;
    [SerializeField] float deadline = -5f;
    [SerializeField] GameObject deadEffect;
    HealthController healthController;
    MainGameManager mainGameManager;

    bool isCompletelyDeath = false;

    private void Awake()
    {
        healthController = FindObjectOfType<HealthController>();
        mainGameManager = FindObjectOfType<MainGameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isCompletelyDeath) return;

        if (transform.position.y < deadline)
        {
            Death();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Death();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (FindAnyObjectByType<HealthController>() != null)
            {
                SceneManagerEx.Instance.LoadAndUnloadScene($"Stage_{StagesData.Instance.currentStageIndex+1}", $"Stage_{StagesData.Instance.currentStageIndex+1}");
            }
        }
    }

    public void Respawn(Vector3? position = null)
    {
        if (currentPlayerRespawnPosition == null)
        {
            currentPlayerRespawnPosition = position;
        }

        transform.position = currentPlayerRespawnPosition.Value;
    }

    public void Death(bool isCompleteDeath = false)
    {
        if (isCompleteDeath)
        {
            isCompletelyDeath = true;
            mainGameManager.StartGameOver();
            Instantiate(deadEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

        healthController.DecreaseHealth();

        //ゲームオーバーの場合
        if (IsGameOver())
        {
            isCompletelyDeath = true;
            mainGameManager.StartGameOver();
            Instantiate(deadEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            return;
        }

        Respawn();
        FadeManager.Instance.ChangeColor(Color.black);
        FadeManager.Instance.FadeIn(0.3f, () => FadeManager.Instance.FadeOut(0.3f));
    }

    bool IsGameOver()
    {
        return healthController.CheckIsDeath();
    }
}
