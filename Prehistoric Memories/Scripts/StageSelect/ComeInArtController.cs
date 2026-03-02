using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComeInArtController : MonoBehaviour
{
    ComeInArtAnimation comeInArtAnimation;
    ArtController artController;
    // Start is called before the first frame update
    void Start()
    {
        comeInArtAnimation = GetComponent<ComeInArtAnimation>();
        artController = GetComponent<ArtController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !StagesData.Instance.stagesData[artController.artNumber].isCleared && StagesData.Instance.stagesData[artController.artNumber].isOpened)
        {
            comeInArtAnimation.Play();
        }
    }
}
