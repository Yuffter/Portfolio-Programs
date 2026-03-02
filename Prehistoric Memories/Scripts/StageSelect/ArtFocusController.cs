using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtFocusController : MonoBehaviour
{
    GameObject artFocus;
    GameObject player;
    ArtController artController;
    [SerializeField] float focusRadius = 3f;
    // Start is called before the first frame update
    void Start()
    {
        artFocus = transform.Find("ArtFocus").gameObject;
        player = GameObject.Find("Player");
        artController = GetComponent<ArtController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        //クリア直後のものの場合処理を行わない
        if (StagesData.Instance.stagesData[artController.artNumber].isChanged)
        {
            return;
        }

        //クリア済みかつクリア直後でない場合
        if (StagesData.Instance.stagesData[artController.artNumber].isCleared && !StagesData.Instance.stagesData[artController.artNumber].isChanged)
        {
            artFocus.Show();
            return;
        }
        //まだ解放されていない場合
        if (!StagesData.Instance.stagesData[artController.artNumber].isOpened)
        {
            artFocus.Hide();
            return;
        }

        float distance = Mathf.Sqrt(Mathf.Pow(player.transform.position.x - artFocus.transform.position.x, 2) + Mathf.Pow(player.transform.position.y - artFocus.transform.position.y, 2));

        if (distance >= focusRadius)
        {
            artFocus.Hide();
        }
        else
        {
            artFocus.Show();
        }
    }
}
