using System.Collections.Generic;
using UniRx;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject
{
    /// <summary>
    /// 現在のグループの数
    /// </summary>
    [Header("現在のグループの数")]
    public int CurrentGroupCount;

    /// <summary>
    /// 最大グループ数
    /// </summary>
    [Header("最大グループ数")]
    public int MaxGroupCount;

    /// <summary>
    /// 最小グループ数
    /// </summary>
    [Header("最小グループ数")]
    public int MinGroupCount;

    /// <summary>
    /// 初期のバルーン数
    /// </summary>
    [Header("初期のバルーン数")]
    public int InitialBalloonCount;

    /// <summary>
    /// グループのデータ
    /// </summary>
    [Header("グループのデータ")]
    public List<GroupData> GroupsData;

    /// <summary>
    /// 現在の問題番号
    /// </summary>
    [Header("現在の問題番号")]
    public int CurrentQuestionNumber;

    /// <summary>
    /// 最大思考時間
    /// </summary>
    [Header("最大思考時間")]
    public float MaxThinkingTime;

    /// <summary>
    /// パーセントバルーンの得点倍率
    /// </summary>
    [Header("パーセントバルーンの得点倍率")]
    public int PercentBalloonPointMultiplier;

    /// <summary>
    /// 探索ラリーの得点倍率
    /// </summary>
    [Header("探索ラリーの得点倍率")]
    public int SearchRallyPointMultiplier;

    /// <summary>
    /// 問題のデータ
    /// </summary>
    [Header("問題のデータ")]
    public List<QuestionData> QuestionsData;

    /// <summary>
    /// 現在の問題のデータ
    /// </summary>
    public QuestionData CurrentQuestion => QuestionsData[CurrentQuestionNumber-1];

    private static GameData _instance;
    public static GameData Instance {
        get {
            if (_instance == null) {
                _instance = Resources.Load<GameData>("ScriptableObjects/GameData");
            }
            return _instance;
        }
    }


    /// <summary>
    /// ゲームデータの初期化を行う
    /// <para>タイトル画面で班の数が指定された後に呼び出される</para>
    /// </summary>
    public void Initialize()
    {
        GroupsData.Clear();

        for (int i = 0; i < CurrentGroupCount; i++)
        {
            GroupsData.Add(new GroupData
            {
                Name = "チーム" + (i + 1),
                RemainingCount = InitialBalloonCount
            });
        }

        Logger.LoggerManager.Log("[GameData] ゲームデータの初期化が完了しました");
    }
}

[System.Serializable]
public class GroupData {
    /// <summary>
    /// チーム名
    /// </summary>
    [Header("チーム名")]
    public string Name;

    /// <summary>
    /// 残りバルーン数
    /// </summary>
    [Header("残りバルーン数")]
    public int RemainingCount;

    /// <summary>
    /// 探索ラリーの得点
    /// </summary>
    [Header("探索ラリーの得点")]
    public int SearchRallyPoint;

    /// <summary>
    /// 総合得点
    /// </summary>
    [Header("総合得点")]
    public int TotalPoint;
}

[System.Serializable]
public class QuestionData {
    /// <summary>
    /// 問題文
    /// </summary>
    [Header("問題文"), TextArea(3, 6)]
    public string Question;

    /// <summary>
    /// 答えのパーセンテージ
    /// </summary>
    [Header("答えのパーセンテージ"), Range(0, 100)]
    public int Percentage;

    /// <summary>
    /// 問題画像
    /// </summary>
    [Header("問題画像")]
    public Sprite Sprite;
}
