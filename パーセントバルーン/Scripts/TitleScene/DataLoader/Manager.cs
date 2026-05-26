using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace DataLoader
{
    public class Manager : MonoBehaviour
    {
        [SerializeField]
        private string _sheetURL;
        private List<Data> _dataList = new List<Data>();
        public List<Data> DataList => _dataList;

        private void Start()
        {
            _ = GetCSV(); // Fire and forget
        }
        private async UniTask GetCSV()
        {
            using (UnityWebRequest www = UnityWebRequest.Get(_sheetURL))
            {
                await www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Logger.LoggerManager.LogError("エラー: " + www.error);
                }
                else
                {
                    // 取得したCSVテキスト
                    string csvText = www.downloadHandler.text;
                    Logger.LoggerManager.Log("CSV取得成功:\n" + csvText);

                    // ここでCSVをパース（分割）して使う
                    string[] lines = csvText.Split(new[] { "\n", "\r", "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);
                    for (int i = 1; i < lines.Length; i++) // 1行目はヘッダーなのでスキップ
                    {
                        string[] fields = lines[i].Split(',');
                        if (fields.Length >= 3)
                        {
                            Data data = new Data
                            {
                                Question = fields[0].Trim(),
                                AnswerPercent = int.Parse(fields[1].Trim()),
                                SpriteURL = fields[2].Trim()
                            };
                            _dataList.Add(data);
                        }
                    }

                    Logger.LoggerManager.Log("スプライトのロード開始...");
                    // 全てのダウンロードタスクを作成
                    var tasks = _dataList.Select(async (data, index) =>
                    {
                        Sprite sprite = await LoadSpriteFromURL(data.SpriteURL);

                        // 構造体なので、直接リストの中身を更新
                        var d = _dataList[index];
                        d.Sprite = sprite;
                        _dataList[index] = d;

                        Logger.LoggerManager.Log($"ロード完了: {d.Question}");
                    }).ToArray();

                    // 全ての完了を待つ
                    await UniTask.WhenAll(tasks);

                    Logger.LoggerManager.Log("全てのデータ準備が完了しました！");

                    // 問題データベースを更新
                    GameData.Instance.QuestionsData = new List<QuestionData>();
                    for (int i = 0; i < _dataList.Count; i++)
                    {
                        GameData.Instance.QuestionsData.Add(new QuestionData
                        {
                            Question = _dataList[i].Question,
                            Percentage = _dataList[i].AnswerPercent,
                            Sprite = _dataList[i].Sprite
                        });
                    }
                }
            }
        }

        private async UniTask<Sprite> LoadSpriteFromURL(string url)
        {
            if (string.IsNullOrEmpty(url)) return null;
            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
            {
                await request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Logger.LoggerManager.LogError("スプライトロードエラー: " + request.error);
                    return null;
                }

                Texture2D texture = DownloadHandlerTexture.GetContent(request);
                return Sprite.Create(
                    texture, // テクスチャ全体を使用
                    new Rect(0, 0, texture.width, texture.height), // スプライトの切り出し範囲
                    new Vector2(0.5f, 0.5f)
                ); // ピボットを中央に設定
            }
        }
    }

    public struct Data
    {
        public string Question;
        public int AnswerPercent;
        public string SpriteURL;
        public Sprite Sprite;
    }
}
