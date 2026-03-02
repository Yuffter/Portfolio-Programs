using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cinemachine;
using UnityEngine.Rendering.Universal;

public class BlocksBreaker : MonoBehaviour
{
    [SerializeField] List<GameObject> blocks = new List<GameObject>();
    [SerializeField] CinemachineVirtualCamera camera;
    [SerializeField] CinemachineVirtualCamera playerCamera;
    [SerializeField] ElectricityModel model;
    [SerializeField] Light2D light;
    // Start is called before the first frame update
    void Start()
    {
        this.OnTriggerEnter2DAsObservable()
            .Where(collision => collision.CompareTag("Player"))
            .First()
            .Subscribe(_ =>
            {
                StartCoroutine(Break());
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Break()
    {
        model.isAction = true;
        light.intensity = 0.3f;
        FindAnyObjectByType<MainGameManager>().DisablePlayerMoving();
        camera.Priority = 11;
        yield return new WaitForSeconds(2);
        foreach (var block in blocks)
        {
            Destroy(block);
        }
        yield return new WaitForSeconds(1f);
        camera.Priority = 9;
        model.isAction = false;
        FindAnyObjectByType<MainGameManager>().EnablePlayerMoving();
    }
}
