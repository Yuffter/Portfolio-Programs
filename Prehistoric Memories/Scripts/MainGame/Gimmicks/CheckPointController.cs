using KanKikuchi.AudioManager;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    [SerializeField] Vector3 newSpawnPoint;
    [SerializeField] GameObject checkPointParticle;
    // Start is called before the first frame update
    void Start()
    {
        this.OnTriggerEnter2DAsObservable()
            .Where(collision => collision.CompareTag("Player"))
            .First()
            .Subscribe(player =>
            {
                player.GetComponent<PlayerRespawn>().currentPlayerRespawnPosition = newSpawnPoint;
                Instantiate(checkPointParticle,player.transform.position, Quaternion.identity);
                SEManager.Instance.Play(SEPath.CHECK_POINT);
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
