using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using KanKikuchi.AudioManager;

public class ComeInArtAnimation : MonoBehaviour
{
    ArtController artController;
    AllArts allArts;
    [SerializeField] Material rippleScreen;
    List<GameObject> paints = new List<GameObject>();
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        artController = GetComponent<ArtController>();
        allArts = FindFirstObjectByType<AllArts>();
        player = GameObject.Find("Player");

        Transform paint = GameObject.Find("Paints").transform;
        for (int i = 0;i < paint.childCount;i++)
        {
            paints.Add(paint.GetChild(i).gameObject);
            paint.GetChild(i).gameObject.Hide();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        StartCoroutine(_Play());
    }

    IEnumerator _Play()
    {
        //カメラが寄る演出まで
        Sequence seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            BGMManager.Instance.FadeOut(2f);
            GameObject artObject = allArts.arts[artController.artNumber];
            CinemachineVirtualCamera camera = artObject.transform.Find("FocusCamera").GetComponent<CinemachineVirtualCamera>();
            camera.Priority = 11;

            player.transform.localScale = Vector3.zero;
            Destroy(player.GetComponent<Rigidbody2D>());

            SEManager.Instance.Play(SEPath.GO_INTO_ART, 0.8f, 0, 0.8f);

            rippleScreen.SetVector("_RippleCenter", artObject.transform.position);
            rippleScreen.SetFloat("_RippleStrength", 1f);
        })
        .Append(rippleScreen.DOFloat(0f, "_RippleStrength", 1f))
        .AppendInterval(2f);

        yield return seq.Play().WaitForCompletion();

        for (int i = 0;i < paints.Count;i++)
        {
            paints[i].Show();

            if (i == 0 || i == 1) yield return new WaitForSeconds(0.3f);
            else yield return new WaitForSeconds(0.05f);

            if (i == paints.Count-1)
            {
                FadeManager.Instance.ChangeColor(Color.white);
                FadeManager.Instance.FadeIn(0f);
            }
        }

        StagesData.Instance.currentStageIndex = artController.artNumber;
        SceneManagerEx.Instance.LoadAndUnloadScene("StageSelect", $"Stage_{artController.artNumber + 1}",() => {
            FadeManager.Instance.ChangeColor(Color.white);
            FadeManager.Instance.ChangeTexture(null);
            FadeManager.Instance.FadeOut(1f);
            });
    }
}
