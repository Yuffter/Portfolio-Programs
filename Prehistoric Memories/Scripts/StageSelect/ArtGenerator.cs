using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtGenerator : MonoBehaviour
{
    [SerializeField] GameObject art;
    [SerializeField] Sprite newFrame;

    AllArts allArts;
    // Start is called before the first frame update
    void Start()
    {
        allArts = FindFirstObjectByType<AllArts>();
        Generate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Generate()
    {
        for (int i = 0;i < StagesData.Instance.stagesData.Count;i++)
        {
            StageData thisStage = StagesData.Instance.stagesData[i];
            GameObject g = Instantiate(art, thisStage.position, Quaternion.identity);
            g.transform.parent = allArts.gameObject.transform;
            g.GetComponent<ArtController>().SetArtNumber(i);
            allArts.arts.Add(g);

            //クリア済みかどうか
            if (thisStage.isCleared && !thisStage.isChanged)
            {
                g.transform.Find("MainPicture").gameObject.SetSprite(thisStage.openedArt);
                g.transform.Find("Frame").gameObject.SetSprite(newFrame);
                Destroy(g.transform.Find("ArtFocus").gameObject.GetComponent<SpriteRenderer>());
            }
            else
            {
                g.transform.Find("MainPicture").gameObject.SetSprite(thisStage.closedArt);
            }
        }
    }
}
