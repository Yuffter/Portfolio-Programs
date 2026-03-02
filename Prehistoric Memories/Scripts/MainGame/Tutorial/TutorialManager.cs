using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using DG.Tweening;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] List<GameObject> tutorialTexts = new List<GameObject>();

    [SerializeField] GameObject standingPositionSquare;
    [SerializeField] GameObject player;
    [SerializeField] GameObject supportTree;
    [Header("条件分岐関連")]
    [SerializeField] ObjectOutline objectOutline;
    [SerializeField] MainGameManager mainGameManager;
    [SerializeField] RemainingPieceModel pieceModel;

    public bool canOpenCreatureList = false;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1;i < tutorialTexts.Count;i++)
        {
            tutorialTexts[i].SetActive(false);
        }
        standingPositionSquare.SetActive(false);
        supportTree.Hide();

        //生成物が生成されたとき
        objectOutline.IsMaterialized
            .Where(value => value)
            .Subscribe(_ =>
            {
                tutorialTexts[0].Hide();
                tutorialTexts[1].Show();
                tutorialTexts[2].Show();
                standingPositionSquare.SetActive(true);
            }).AddTo(this);

        mainGameManager.IsCreatureListOpen
            .Where(value => value)
            .First()
            .Subscribe(_ =>
            {
                tutorialTexts[1].Hide();
                tutorialTexts[2].Hide();
                tutorialTexts[3].Show();
                standingPositionSquare.SetActive(false);
            })
            .AddTo(this);

        pieceModel.PieceCount
            .Where(value => value == 8)
            .Subscribe(_ =>
            {
                tutorialTexts[5].Hide();
                tutorialTexts[6].Show();
            });
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position,standingPositionSquare.transform.position);
        if (distance <= 0.4f && tutorialTexts[1].activeSelf) canOpenCreatureList = true;
        else canOpenCreatureList = false;
    }

    public void SelectCreature()
    {
        tutorialTexts[3].Hide();
        tutorialTexts[4].Show();
        supportTree.Show();
    }

    public void CreateNewCreature()
    {
        supportTree.Hide();
        tutorialTexts[4].Hide();

        tutorialTexts[5].Show();
    }

    public void DestroyCreature()
    {
        tutorialTexts[6].Hide();
        tutorialTexts[7].Show();

        Destroy(this);
    }
}
