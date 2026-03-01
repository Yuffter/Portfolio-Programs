using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

#if UNITY_EDITOR
public class GameEventVisualizer : EditorWindow
{
    // ==================================================================================
    // データ構造
    // ==================================================================================

    [Serializable]
    private class EventNode
    {
        public string PropertyName;     // Hub内でのプロパティ名
        public ScriptableObject Asset;  // 実際のGameEventアセット
        public List<UsageEntry> Raisers = new List<UsageEntry>();
        public List<UsageEntry> Subscribers = new List<UsageEntry>();
        public bool IsFoldedOut = true;
    }

    [Serializable]
    private class UsageEntry
    {
        public string SourceType;       // "SceneObject", "Prefab", "ScriptFile"
        public string Name;             // オブジェクト名 または ファイル名
        public UnityEngine.Object ReferenceObject; // 選択時にPingする対象
        public string MethodName;       // メソッド名
        public int LineNumber;          // 行番号
        public string CodeSnippet;      // コード
    }

    // ==================================================================================
    // 変数
    // ==================================================================================

    private ScriptableObject _targetHubOrEvent;
    private List<EventNode> _analysisResults = new List<EventNode>();
    private Vector2 _scrollPos;
    
    private GUIStyle _headerStyle;
    private GUIStyle _snippetStyle;
    private GUIStyle _tagStyle;

    [MenuItem("Tools/GameEvent Visualizer")]
    public static void ShowWindow()
    {
        GetWindow<GameEventVisualizer>("Event Visualizer");
    }

    // ==================================================================================
    // GUI描画
    // ==================================================================================

    private void OnGUI()
    {
        InitializeStyles();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("GameEvent Analysis Tool", EditorStyles.boldLabel);
        
        using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
        {
            var newTarget = (ScriptableObject)EditorGUILayout.ObjectField("Target (Hub or Event)", _targetHubOrEvent, typeof(ScriptableObject), false);
            if (newTarget != _targetHubOrEvent)
            {
                _targetHubOrEvent = newTarget;
                _analysisResults.Clear();
            }

            EditorGUILayout.Space(5);
            EditorGUILayout.LabelField("解析オプション", EditorStyles.miniBoldLabel);
            
            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("現在のシーンを解析", GUILayout.Height(30)))
                {
                    AnalyzeCurrentScene();
                }
                if (GUILayout.Button("全プレファブを解析", GUILayout.Height(30)))
                {
                    AnalyzeAllPrefabs();
                }
            }
            if (GUILayout.Button("全スクリプトをテキスト検索 (Pureクラス用)", GUILayout.Height(25)))
            {
                AnalyzeAllScriptsText();
            }
        }

        if (_analysisResults.Count == 0 && _targetHubOrEvent != null)
        {
            EditorGUILayout.HelpBox("解析ボタンを押してください。", MessageType.Info);
        }

        EditorGUILayout.Space();
        DrawResults();
    }

    private void InitializeStyles()
    {
        if (_headerStyle == null) _headerStyle = new GUIStyle(EditorStyles.boldLabel);
        if (_snippetStyle == null) _snippetStyle = new GUIStyle(EditorStyles.label) { fontSize = 10, normal = { textColor = Color.gray } };
        if (_tagStyle == null) _tagStyle = new GUIStyle(EditorStyles.miniLabel) 
        { 
            alignment = TextAnchor.MiddleRight, 
            normal = { textColor = new Color(0.2f, 0.6f, 1f) } 
        };
    }

    private void DrawResults()
    {
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

        foreach (var node in _analysisResults)
        {
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    var title = string.IsNullOrEmpty(node.PropertyName) || node.PropertyName == "Self" 
                        ? node.Asset.name 
                        : $"{node.PropertyName} ({node.Asset.name})";

                    node.IsFoldedOut = EditorGUILayout.Foldout(node.IsFoldedOut, $"{title} [R:{node.Raisers.Count} / S:{node.Subscribers.Count}]", true);
                    
                    if (GUILayout.Button("Asset", GUILayout.Width(50)))
                    {
                        EditorGUIUtility.PingObject(node.Asset);
                    }
                }

                if (node.IsFoldedOut)
                {
                    EditorGUI.indentLevel++;
                    if (node.Raisers.Count > 0)
                    {
                        EditorGUILayout.LabelField("📢 Raisers (発行)", _headerStyle);
                        foreach (var entry in node.Raisers) DrawUsageEntry(entry);
                        EditorGUILayout.Space(5);
                    }

                    if (node.Subscribers.Count > 0)
                    {
                        EditorGUILayout.LabelField("👂 Subscribers (購読)", _headerStyle);
                        foreach (var entry in node.Subscribers) DrawUsageEntry(entry);
                    }
                    EditorGUI.indentLevel--;
                }
            }
            EditorGUILayout.Space(5);
        }
        EditorGUILayout.EndScrollView();
    }

    private void DrawUsageEntry(UsageEntry entry)
    {
        using (new EditorGUILayout.HorizontalScope())
        {
            // ソース種別表示 (Scene, Prefab, Script)
            EditorGUILayout.LabelField($"[{entry.SourceType}]", GUILayout.Width(70));

            // オブジェクト/ファイルへのリンク
            if (GUILayout.Button(entry.Name, EditorStyles.linkLabel, GUILayout.Width(180)))
            {
                if (entry.ReferenceObject != null)
                {
                    Selection.activeObject = entry.ReferenceObject;
                    EditorGUIUtility.PingObject(entry.ReferenceObject);
                }
            }

            using (new EditorGUILayout.VerticalScope())
            {
                string location = string.IsNullOrEmpty(entry.MethodName) 
                    ? $"(L{entry.LineNumber})" 
                    : $"{entry.MethodName} (L{entry.LineNumber})";
                
                EditorGUILayout.LabelField(location);
                EditorGUILayout.LabelField($"   {entry.CodeSnippet.Trim()}", _snippetStyle);
            }
        }
    }

    // ==================================================================================
    // 共通: ターゲット特定処理
    // ==================================================================================

    private List<EventNode> PrepareAnalysisNodes()
    {
        _analysisResults.Clear();
        if (_targetHubOrEvent == null) return _analysisResults;

        var targetEventMap = IdentifyTargetEvents(_targetHubOrEvent);
        foreach (var kvp in targetEventMap)
        {
            _analysisResults.Add(new EventNode { PropertyName = kvp.Key, Asset = kvp.Value });
        }
        return _analysisResults;
    }

    // ==================================================================================
    // 1. シーン解析 (Scene Objects)
    // ==================================================================================

    private void AnalyzeCurrentScene()
    {
        PrepareAnalysisNodes();
        var allMonos = FindObjectsOfType<MonoBehaviour>(true);
        ScanMonoBehaviours(allMonos, "SceneObject");
    }

    // ==================================================================================
    // 2. プレファブ解析 (Project Prefabs)
    // ==================================================================================

    private void AnalyzeAllPrefabs()
    {
        PrepareAnalysisNodes();
        
        string[] guids = AssetDatabase.FindAssets("t:Prefab");
        int count = 0;

        try
        {
            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                
                // プログレスバー表示
                if (count % 10 == 0)
                {
                    if (EditorUtility.DisplayCancelableProgressBar("Scanning Prefabs", path, (float)count / guids.Length))
                        break;
                }

                GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (prefab != null)
                {
                    var components = prefab.GetComponentsInChildren<MonoBehaviour>(true);
                    // プレファブパスをソースタイプとして渡すのではなく、"Prefab"として渡す
                    // ReferenceObjectにはPrefab Assetそのものを設定
                    ScanMonoBehaviours(components, "Prefab"); 
                }
                count++;
            }
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }
    }

    // ==================================================================================
    // 共通スキャンロジック (MonoBehaviour)
    // ==================================================================================

    private void ScanMonoBehaviours(MonoBehaviour[] monos, string sourceType)
    {
        foreach (var mb in monos)
        {
            if (mb == null) continue;
            var scriptContent = GetScriptContent(mb);
            if (string.IsNullOrEmpty(scriptContent)) continue;

            var mbType = mb.GetType();
            var fields = GetInspectableFields(mbType);

            foreach (var field in fields)
            {
                var fieldValue = field.GetValue(mb) as ScriptableObject;
                if (fieldValue == null) continue;

                // Hubを持っている場合
                if (fieldValue == _targetHubOrEvent)
                {
                    foreach (var node in _analysisResults)
                    {
                        ScanTextForUsage(scriptContent, field.Name, node.PropertyName, node, mb.gameObject.name, mb.gameObject, sourceType);
                    }
                }
                // GameEventを直接持っている場合
                else if (IsGameEvent(fieldValue))
                {
                    var hitNode = _analysisResults.FirstOrDefault(x => x.Asset == fieldValue);
                    if (hitNode != null)
                    {
                        ScanTextForUsage(scriptContent, field.Name, "", hitNode, mb.gameObject.name, mb.gameObject, sourceType);
                    }
                }
            }
        }
    }

    // ==================================================================================
    // 3. 全スクリプトテキスト解析 (Pure C# Classes / Unknown References)
    // ==================================================================================

    private void AnalyzeAllScriptsText()
    {
        PrepareAnalysisNodes();

        string[] guids = AssetDatabase.FindAssets("t:MonoScript");
        int count = 0;

        try
        {
            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                MonoScript script = AssetDatabase.LoadAssetAtPath<MonoScript>(path);
                if (script == null) continue;
                
                // パッケージ内のスクリプトなどを除外したければここでフィルタ
                if (path.StartsWith("Packages/")) continue;

                if (count % 50 == 0)
                {
                    if (EditorUtility.DisplayCancelableProgressBar("Scanning Scripts", path, (float)count / guids.Length))
                        break;
                }

                string content = script.text;

                // 各イベントノードに対して、「.PropertyName.Raise(」のようなパターンがないか探す
                // Pureクラスでは変数名が不明なため、プロパティ名に依存した検索を行う
                foreach (var node in _analysisResults)
                {
                    // GameEvent単体の場合はプロパティ名がないので、この手法では変数名不明のため検索困難
                    // (ただし、汎用的な ".Raise(" だけ検索するとノイズが多すぎるため、Hub経由を主とする)
                    if (string.IsNullOrEmpty(node.PropertyName) || node.PropertyName == "Self") continue;

                    // 検索パターン: .OverviewEvent.Raise(  または .OverviewEvent.Subscribe(
                    // 変数名は何でも良いため、直前にドットがあることを条件にする
                    ScanPureTextForUsage(content, node.PropertyName, node, script.name, script, "ScriptFile");
                }
                count++;
            }
        }
        finally
        {
            EditorUtility.ClearProgressBar();
        }
    }

    private void ScanPureTextForUsage(string content, string propertyName, EventNode node, string name, UnityEngine.Object refObj, string type)
    {
        // プロパティ名の前にドット、後ろにRaise/Subscribe
        // 例: _hub.OverviewEvent.Raise(...) -> ".OverviewEvent.Raise"
        
        string escapedProp = Regex.Escape(propertyName);

        // Raise
        var raiseMatches = Regex.Matches(content, $@"\.{escapedProp}\.Raise\s*\(");
        foreach (Match match in raiseMatches)
        {
            AddUsage(node.Raisers, name, refObj, content, match.Index, $".{propertyName}.Raise", type);
        }

        // Subscribe
        var subMatches = Regex.Matches(content, $@"\.{escapedProp}\.Subscribe\s*\(");
        foreach (Match match in subMatches)
        {
            AddUsage(node.Subscribers, name, refObj, content, match.Index, $".{propertyName}.Subscribe", type);
        }
    }

    // ==================================================================================
    // 共通: テキスト検索 & 登録
    // ==================================================================================

    private void ScanTextForUsage(string content, string variableName, string propertyName, EventNode node, string objName, UnityEngine.Object refObj, string sourceType)
    {
        string accessPath = string.IsNullOrEmpty(propertyName) 
            ? variableName 
            : $"{variableName}.{propertyName}";
        string escapedPath = Regex.Escape(accessPath);

        // Raise
        var raiseMatches = Regex.Matches(content, $@"{escapedPath}\.Raise\s*\(");
        foreach (Match match in raiseMatches)
        {
            AddUsage(node.Raisers, objName, refObj, content, match.Index, accessPath + ".Raise", sourceType);
        }

        // Subscribe
        var subMatches = Regex.Matches(content, $@"{escapedPath}\.Subscribe\s*\(");
        foreach (Match match in subMatches)
        {
            AddUsage(node.Subscribers, objName, refObj, content, match.Index, accessPath + ".Subscribe", sourceType);
        }
    }

    private void AddUsage(List<UsageEntry> list, string name, UnityEngine.Object refObj, string content, int index, string accessPath, string sourceType)
    {
        int lineNumber = GetLineNumber(content, index);
        
        // 重複チェック
        if (list.Any(x => x.ReferenceObject == refObj && x.LineNumber == lineNumber)) return;

        string methodName = FindMethodName(content, index);
        string lineContent = GetLineContent(content, index);

        list.Add(new UsageEntry
        {
            SourceType = sourceType,
            Name = name,
            ReferenceObject = refObj,
            LineNumber = lineNumber,
            MethodName = methodName,
            CodeSnippet = lineContent,
        });
    }

    // ==================================================================================
    // ユーティリティ
    // ==================================================================================

    private Dictionary<string, ScriptableObject> IdentifyTargetEvents(ScriptableObject target)
    {
        var result = new Dictionary<string, ScriptableObject>();

        if (IsGameEvent(target))
        {
            result.Add("Self", target);
            return result;
        }

        var type = target.GetType();
        foreach (var prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
            if (IsGameEventType(prop.PropertyType))
            {
                var value = prop.GetValue(target) as ScriptableObject;
                if (value != null) result[prop.Name] = value;
            }
        }
        // フィールドも検索（バッキングフィールド除外）
        foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
            if (IsGameEventType(field.FieldType))
            {
                if (field.Name.Contains("k__BackingField")) continue;
                if (field.Name.StartsWith("_")) continue; 
                var value = field.GetValue(target) as ScriptableObject;
                if (value != null && !result.ContainsKey(field.Name)) result[field.Name] = value;
            }
        }
        return result;
    }

    private bool IsGameEvent(ScriptableObject obj) => obj != null && IsGameEventType(obj.GetType());

    private bool IsGameEventType(Type type)
    {
        if (type == null) return false;
        while (type != null && type != typeof(object))
        {
            if (type.Name == "GameEvent" || (type.IsGenericType && type.Name.StartsWith("GameEvent"))) return true;
            type = type.BaseType;
        }
        return false;
    }

    private FieldInfo[] GetInspectableFields(Type type)
    {
        var fields = new List<FieldInfo>();
        while (type != null && type != typeof(MonoBehaviour))
        {
            var myFields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            foreach (var f in myFields)
            {
                if (f.IsPublic || f.GetCustomAttribute<SerializeField>() != null) fields.Add(f);
            }
            type = type.BaseType;
        }
        return fields.ToArray();
    }

    private string GetScriptContent(MonoBehaviour mb)
    {
        var monoScript = MonoScript.FromMonoBehaviour(mb);
        return monoScript != null ? monoScript.text : null;
    }

    private int GetLineNumber(string text, int index) => text.Take(index).Count(c => c == '\n') + 1;

    private string GetLineContent(string text, int index)
    {
        int start = text.LastIndexOf('\n', index);
        if (start == -1) start = 0; else start++;
        int end = text.IndexOf('\n', index);
        if (end == -1) end = text.Length;
        return text.Substring(start, end - start);
    }

    private string FindMethodName(string text, int index)
    {
        var substring = text.Substring(0, index);
        var matches = Regex.Matches(substring, @"(public|private|protected|internal|void|override|IEnumerator)\s+[\w<>]+\s+(?<method>\w+)\s*\(.*?\)\s*\{");
        if (matches.Count > 0) return matches[matches.Count - 1].Groups["method"].Value;
        return "<Unknown Method>";
    }
}
#endif