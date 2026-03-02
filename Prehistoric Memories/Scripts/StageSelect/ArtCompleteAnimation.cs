using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cinemachine;
using DG.Tweening;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using TMPro;
using KanKikuchi.AudioManager;

public class ArtCompleteAnimation : MonoBehaviour
{
    [SerializeField] ArtCheckerController artCheckerController;
    [SerializeField] AllArts allArts;

    [Header("‰‰oŠÖ˜A")]
    [SerializeField] Material changeTextureMaterial;
    [SerializeField] Texture dissolveTex;
    [SerializeField] GameObject newFramePrefab;
    [SerializeField] GameObject completeParticle;
    // Start is called before the first frame update
    void Start()
    {
        artCheckerController.IsClear
            .Where(value => value)
            .Subscribe(_ => StartCoroutine(Play()));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Play()
    {
        FindAnyObjectByType<Player>().DisableMoving();
        yield return new WaitForSeconds(1f);
        StageData currentStageData = StagesData.Instance.stagesData[StagesData.Instance.currentStageIndex];
        GameObject currentStageObj = allArts.arts[StagesData.Instance.currentStageIndex];

        var focusCamera = currentStageObj.transform.Find("FocusCamera").GetComponent<CinemachineVirtualCamera>();
        var mainPicture = currentStageObj.transform.Find("MainPicture");
        var artFrame = currentStageObj.transform.Find("Frame").gameObject;
        var artFocus = currentStageObj.transform.Find("ArtFocus").gameObject;

        focusCamera.Priority = 11;

        yield return new WaitForSeconds(2.5f);

        var mat = mainPicture.GetComponent<SpriteRenderer>().material = new Material(changeTextureMaterial);
        mat.SetTexture("_SubTex", currentStageData.openedArt.texture);
        mat.SetTexture("_DissolveTex", dissolveTex);
        mat.SetFloat("_Threshold", 1.1f);

        yield return mat.DOFloat(0f, "_Threshold", 1f).WaitForCompletion();
        //yield return new WaitForSeconds(0.5f);
        artFrame.AddComponent<Rigidbody2D>();
        artFrame.AddComponent<BoxCollider2D>();
        var artFrameExp = artFrame.AddComponent<Explodable>();
        Vector3 originalFrameScale = artFrame.transform.localScale;
        Vector3 originalFramePosition = artFrame.transform.position;
        artFrameExp.extraPoints = 100;
        artFrameExp.fragmentInEditor();
        var fragments = artFrameExp.fragments;
        artFrameExp.explode();
        foreach (var g in fragments)
        {
            g.GetComponent<PolygonCollider2D>().isTrigger = true;
            g.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1000, 1000), Random.Range(0, 1)));
        }

        yield return new WaitForSeconds(0.1f);

        GameObject newFrame = Instantiate(newFramePrefab, originalFramePosition, Quaternion.identity);
        newFrame.transform.SetParent(currentStageObj.transform);
        newFrame.transform.localScale = originalFrameScale + new Vector3(1, 1, 0);
        newFrame.transform.DOScale(originalFrameScale, 0.3f).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            Destroy(artFocus.GetComponent<SpriteRenderer>());
            Instantiate(completeParticle, originalFramePosition, Quaternion.identity);
            focusCamera.transform.DOShakePosition(0.5f, 1, 90, 90, false, true);
        });

        yield return new WaitForSeconds(1.5f);
        focusCamera.Priority = 9;
        currentStageData.isChanged = false;

        yield return new WaitForSeconds(1.5f);

        if (StagesData.Instance.currentStageIndex == 2)
        {
            yield return new WaitForSeconds(1f);
            SceneManagerEx.Instance.LoadAndUnloadScene("StageSelect", "Ending");
            yield break;
        }

        GameObject abilityDescriptionPanel = GameObject.Find("AbilityDescription");
        TextMeshProUGUI abilityDescriptionLabel = abilityDescriptionPanel.transform.Find("Label").GetComponent<TextMeshProUGUI>();

        AbilitiesData.Instance.abilities[StagesData.Instance.currentStageIndex].isEnable = true;
        abilityDescriptionLabel.text = AbilitiesData.Instance.abilities[StagesData.Instance.currentStageIndex].abilityDescription;

        yield return abilityDescriptionPanel.transform.DOScaleX(1f, 1f).SetEase(Ease.OutBounce).WaitForCompletion();

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Return));

        yield return abilityDescriptionPanel.transform.DOScaleX(0f, 1f).WaitForCompletion();

        //•Ç‚ð“®‚©‚·
        GameObject rightWall = GameObject.Find("RightWall");
        var rightWallCamera = rightWall.GetComponentInChildren<CinemachineVirtualCamera>();

        rightWallCamera.Priority = 11;
        yield return new WaitForSeconds(2f);

        SEManager.Instance.Play(SEPath.MOVE_WALL);
        rightWallCamera.transform.DOShakePosition(2, 1, 90, 90, false, true);
        yield return rightWall.transform.DOMoveX(7, 2).SetRelative().WaitForCompletion();

        SEManager.Instance.Stop(SEPath.MOVE_WALL);
        StagesData.Instance.stagesData[StagesData.Instance.currentStageIndex+1].isOpened = true;
        rightWallCamera.Priority = 9;
        FindAnyObjectByType<Player>().EnableMoving();
    }
}
