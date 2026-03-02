using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UniRx;
using UniRx.Triggers;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using DG.Tweening;
using KanKikuchi.AudioManager;

public class PieceCollectorManager : MonoBehaviour
{
    [SerializeField] List<GameObject> arts = new List<GameObject>();
    [SerializeField] Material singleColorMaterial;

    [Header("演出について")]
    [SerializeField] float startSortingPosition = -5f;
    [SerializeField] PlayableDirector startTimeline;
    [SerializeField] Texture beginningFadeOutTexture;
    [SerializeField] Volume volume;
    [SerializeField] GameObject circleRing;
    [SerializeField] Texture mainArtAppearanceTexture;
    [SerializeField] List<ParticleSystem> papers = new List<ParticleSystem>();
    [SerializeField] Texture fadeInTexture;
    Vignette vignette;
    Bloom bloom;

    GameObject currentArt = null;
    public List<GameObject> currentArtPieces = null;
    public List<GameObject> backgroundArtPieces = null;
    GameObject backgroundArt = null;

    public bool canCombine = false;

    public IReadOnlyReactiveProperty<int> PieceCount => pieceCount;
    ReactiveProperty<int> pieceCount = new ReactiveProperty<int>();
    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGet(out vignette);
        volume.profile.TryGet(out  bloom);

        FadeManager.Instance.ChangeTexture(beginningFadeOutTexture);
        FadeManager.Instance.ChangeColor(Color.white);
        FadeManager.Instance.FadeOut(1f);
        CreatePieces();
        StartCoroutine(SpreadPieces());

        pieceCount.Value = currentArtPieces.Count;
        PieceCount
            .Where(value => value == 0)
            .Subscribe(_ =>
            {
                StartCoroutine(PlayAllFitAnimation());
            });

        pieceCount.Subscribe(_ => print(_));
    }

    void CreatePieces()
    {
        arts[StagesData.Instance.currentStageIndex].SetActive(true);
        currentArt = arts[StagesData.Instance.currentStageIndex];
        backgroundArt = Instantiate(currentArt, Vector3.zero, Quaternion.identity);

        //バラバラにするピースの設定
        currentArtPieces = currentArt.GetComponent<Explodable>().fragments;
        currentArt.GetComponent<Explodable>().explode(false);
        currentArt.Hide();

        for (int i = 0;i < currentArtPieces.Count;i++)
        {
            currentArtPieces[i].GetComponent<MeshRenderer>().sortingOrder = 2;

            currentArtPieces[i].GetComponent<MeshRenderer>().materials = new Material[] {
                currentArtPieces[i].GetComponent<MeshRenderer>().materials[0],
                new Material(singleColorMaterial)
            };

            currentArtPieces[i].GetComponent<MeshRenderer>().materials[1].SetColor("_DissolveColor", Color.white);
            currentArtPieces[i].GetComponent<MeshRenderer>().materials[1].SetFloat("_Threshold", 1f);

            currentArtPieces[i].AddComponent<CombinePieceController>().pieceIndex = i;
        }

        //もととなるピースの設定
        backgroundArtPieces = backgroundArt.GetComponent<Explodable>().fragments;
        foreach (var piece in backgroundArtPieces)
        {
            piece.GetComponent<PolygonCollider2D>().isTrigger = true;
            piece.GetComponent<Rigidbody2D>().isKinematic = true;
        }
        backgroundArt.GetComponent<Explodable>().explode();
        foreach (GameObject piece in backgroundArtPieces)
        {
            piece.GetComponent<MeshRenderer>().materials = new Material[] {
                piece.GetComponent<MeshRenderer>().materials[0],
                new Material(singleColorMaterial)
            };

            piece.GetComponent<MeshRenderer>().materials[1].SetColor("_DissolveColor",
                new Color(UnityEngine.Random.Range(0, 0.5f), UnityEngine.Random.Range(0, 0.5f), UnityEngine.Random.Range(0, 0.5f), 1));
            piece.GetComponent<MeshRenderer>().materials[1].SetFloat("_Threshold", 0f);

            piece.GetComponent<Rigidbody2D>().isKinematic = true;
            //piece.GetComponent<PolygonCollider2D>().isTrigger = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpreadPieces()
    {
        //等間隔に並べるための間隔を求める
        float distacne = Mathf.Abs(startSortingPosition * 2f) / (currentArtPieces.Count - 1);
        for (int i = 0;i <  currentArtPieces.Count;i++)
        {
            currentArtPieces[i].transform.position = new Vector3(startSortingPosition + distacne * i, 10, 0);
        }
        
        yield return new WaitForSeconds(1f);
        startTimeline.Play();

        //タイムラインが終了するまで待機
        yield return new WaitUntil(() => startTimeline.state == PlayState.Paused);
        canCombine = true;
    }

    public void FitPiece()
    {
        pieceCount.Value--;
    }

    IEnumerator PlayAllFitAnimation()
    {
        yield return new WaitForSeconds(1f);
        yield return DOVirtual.Float(
            0,
            1,
            1,
            x => vignette.intensity.value = x).WaitForCompletion();
        currentArt.GetComponent<SpriteRenderer>().material = new Material(singleColorMaterial);
        currentArt.GetComponent<SpriteRenderer>().material.SetTexture("_DissolveTex", mainArtAppearanceTexture);
        currentArt.GetComponent<SpriteRenderer>().material.SetFloat("_Threshold", 1f);
        currentArt.Show();
        currentArt.GetComponent<SpriteRenderer>().sortingOrder = 10;

        yield return new WaitForSeconds(1f);

        GameObject redCircleRing = Instantiate(circleRing, new Vector3(0, 1, 0), Quaternion.identity);
        redCircleRing.SetColor(1, 0, 0, 1);
        SEManager.Instance.Play(SEPath.WATER_DROPLETS);
        yield return new WaitForSeconds(0.5f);

        GameObject greenCircleRing = Instantiate(circleRing,new Vector3(-1,-1,0), Quaternion.identity);
        greenCircleRing.SetColor(0, 1, 0, 1);
        SEManager.Instance.Play(SEPath.WATER_DROPLETS);
        yield return new WaitForSeconds(0.5f);

        GameObject blueCircleRing = Instantiate(circleRing,new Vector3(1,-1,0),Quaternion.identity);
        blueCircleRing.SetColor(0, 0, 1, 1);
        SEManager.Instance.Play(SEPath.WATER_DROPLETS);
        yield return new WaitForSeconds(0.5f);

        Camera.main.DOOrthoSize(2.5f, 1);
        DOVirtual.Float(1f, 0.5f, 1f, x => vignette.intensity.value = x);
        yield return Camera.main.DOShakePosition(1f, 0.5f, 90, 90, true).WaitForCompletion();

        DOVirtual.Float(0, 30, 0.5f, x => bloom.intensity.value = x).OnComplete(() => DOVirtual.Float(30, 0, 0.5f, x => bloom.intensity.value = x));

        Camera.main.DOOrthoSize(5f, 1f).SetEase(Ease.OutBack);
        currentArt.GetComponent<SpriteRenderer>().material.SetColor("_DissolveColor", Color.white);
        currentArt.GetComponent<SpriteRenderer>().material.DOFloat(0f, "_Threshold", 1f);
        yield return DOVirtual.Float(0.5f, 0f, 1f, x => vignette.intensity.value = x).WaitForCompletion();

        papers.ForEach(x => x.Play());
        SEManager.Instance.Play(SEPath.PIECE_COMPLETE);

        yield return new WaitForSeconds(2f);
        
        FadeManager.Instance.ChangeTexture(fadeInTexture);
        FadeManager.Instance.ChangeColor(Color.black);
        yield return FadeManager.Instance.FadeIn(2f);

        StagesData.Instance.stagesData[StagesData.Instance.currentStageIndex].isChanged = true;
        StagesData.Instance.stagesData[StagesData.Instance.currentStageIndex].isCleared = true;
        SceneManagerEx.Instance.LoadAndUnloadScene("PieceCollector", "StageSelect",() =>
        {
            foreach (var g in backgroundArtPieces)
            {
                Destroy(g);
            }
            FadeManager.Instance.ChangeColor(Color.black);
            FadeManager.Instance.ChangeTexture(null);
            FadeManager.Instance.FadeOut(1f);
        });
    }
}
