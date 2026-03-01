using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using unityroom.Api;

namespace SO
{
    [CreateAssetMenu(menuName = "ScriptableObject/Create StageDataBase")]
    public class StageDatabase : ScriptableObject
    {
        [SerializeField, Header("バージョン情報")]
        private int _version;
        /// <summary>
        /// バージョン情報
        /// </summary>
        public int Version => _version;
        [SerializeField, Header("各ステージの設定事項等")]
        private List<StageData> _stages;
        /// <summary>
        /// 各ステージの設定事項等
        /// </summary>
        public IReadOnlyList<StageData> Stages => _stages;

        /// <summary>
        /// 全ステージのハイスコアをロードする
        /// <para>もしハイスコア情報が存在しない場合は、該当ステージのハイスコアを0に初期化する</para>
        /// <para>タイトル画面にて呼び出すことを想定</para>
        /// </summary>
        public void LoadHighScores()
        {
            for (int i = 0; i < _stages.Count; i++)
            {
                string stageKey = $"Stage_{i}";
                if (PlayerPrefs.HasKey(stageKey))
                {
                    _stages[i].HighScore = PlayerPrefs.GetInt(stageKey);
                }
                else
                {
                    _stages[i].HighScore = 0;
                    PlayerPrefs.SetInt(stageKey, 0);
                }
            }
            // WebGL対応: 初期化したデータを確実に保存
            PlayerPrefs.Save();
        }

        /// <summary>
        /// 指定したステージのハイスコアを保存する
        /// </summary>
        /// <param name="stageIndex"></param>
        /// <param name="score"></param>
        public void SaveHighScore(int stageIndex, int score)
        {
            if (stageIndex < 0 || stageIndex >= _stages.Count)
            {
                Debug.LogError($"Invalid stage index: {stageIndex}");
                return;
            }

            if (score > _stages[stageIndex].HighScore)
            {
                _stages[stageIndex].HighScore = score;
                PlayerPrefs.SetInt($"Stage_{stageIndex}", score);
                PlayerPrefs.Save();
            }
        }

        /// <summary>
        /// 全てのステージのハイスコアをリセットする
        /// </summary>
        public void ResetHighScores()
        {
            for (int i = 0; i < _stages.Count; i++)
            {
                _stages[i].HighScore = 0;
                PlayerPrefs.SetInt($"Stage_{i}", 0);
            }
            PlayerPrefs.Save();
        }

        /// <summary>
        /// 全てのステージのスコアの総合点数を計算し、unityroomのランキングに送信する
        /// </summary>
        public void SubmitScoreToRanking()
        {
            int totalScore = 0;
            for (int i = 0; i < _stages.Count; i++)
            {
                totalScore += _stages[i].HighScore;
            }
            Debug.Log($"全ステージの総合点数: {totalScore}");
            UnityroomApiClient.Instance.SendScore(1, totalScore, ScoreboardWriteMode.HighScoreDesc);
        }
    }

    [Serializable]
    public class StageData
    {
        [SerializeField, Header("最大ターン数")]
        private int _maxTurnCount;
        /// <summary>
        /// 最大ターン数
        /// </summary>
        public int MaxTurnCount => _maxTurnCount;

        [SerializeField, Header("ステージの説明文")]
        private string _stageDescription;
        /// <summary>
        /// ステージの説明文
        /// </summary>
        public string StageDescription => _stageDescription;

        [SerializeField, Header("サムネイル画像")]
        private Sprite _thumbnail;
        /// <summary>
        /// サムネイル画像
        /// </summary>
        public Sprite Thumbnail => _thumbnail;
        [SerializeField, Header("ステージシーン名")]
        private string _stageSceneName;
        /// <summary>
        /// ステージシーン名
        /// </summary>
        public string StageSceneName => _stageSceneName;
        [SerializeField, Header("ステージの消耗品データベース")]
        private SO.Consumables.HeldConsumables _stageConsumables;
        /// <summary>
        /// ステージの消耗品データベース
        /// </summary>
        public SO.Consumables.HeldConsumables StageConsumables => _stageConsumables;

        [SerializeField, Header("機能群シーンデータ")]
        private List<FeatureSceneData> _featureScenes;
        /// <summary>
        /// 機能群シーンデータ
        /// </summary>
        public IReadOnlyList<FeatureSceneData> FeatureScenes => _featureScenes;

        /// <summary>
        /// 現在の残りターン数
        /// </summary>
        public int CurrentTurnLeft { get; set; }

        /// <summary>
        /// ハイスコア
        /// </summary>
        public int HighScore { get; set; } = 0;
    }

    [Serializable]
    public class FeatureSceneData
    {
        [SerializeField, Header("シーン名")] private string _sceneName;
        public string SceneName => _sceneName;

        [SerializeField, Header("マルチ読み込みするかどうか")] private bool _shouldLoadAdditively;
        public bool ShouldLoadAdditively => _shouldLoadAdditively;
    }
}
