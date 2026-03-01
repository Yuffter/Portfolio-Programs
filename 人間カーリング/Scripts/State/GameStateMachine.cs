using System;
using System.Collections.Generic;
using R3;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

namespace State
{
    /// <summary>
    /// ゲーム全体の状態を管理するステートマシン
    /// </summary>
    public sealed class GameStateMachine : SingletonMonoBehaviour<GameStateMachine>
    {
        [SerializeField, Header("初期ステート")]
        private GameState _initialGameState;

        private IState _currentStateInstance;
        public IState CurrentStateInstance
        {
            get
            {
                return _currentStateInstance;
            }
            set
            {
                _currentStateInstance = value;
            }
        }

        private ReactiveProperty<GameState> _currentGameStateProp = new ReactiveProperty<GameState>(GameState.Title);
        /// <summary>
        /// 現在のステート
        /// </summary>
        public ReadOnlyReactiveProperty<GameState> CurrentGameStateProp => _currentGameStateProp;

        private Subject<GameState> _onSceneChanged = new Subject<GameState>();
        /// <summary>
        /// シーンが変更されたときに発火するイベント
        /// </summary>
        public Observable<GameState> OnSceneChanged => _onSceneChanged;
        private Dictionary<GameState, IState> _stateInstances = new Dictionary<GameState, IState>();

        protected override void Awake()
        {
            base.Awake();
            CreateStates();
            SceneManager.sceneLoaded += (scene, mode) => {
                if (_currentStateInstance != null)
                {

                }
                _onSceneChanged.OnNext(_currentGameStateProp.Value);
            };
        }

        private void Start()
        {
            ChangeState(_initialGameState);
        }

        private void CreateStates()
        {
            _stateInstances[GameState.Title] = new TitleState();
            _stateInstances[GameState.StageSelect] = new StageSelectState();
            _stateInstances[GameState.GameStart] = new GameStartState();
            _stateInstances[GameState.TurnStart] = new TurnStartState();
            _stateInstances[GameState.Controllable] = new ControllableState();
            _stateInstances[GameState.Waiting] = new WaitingState();
            _stateInstances[GameState.GameEnd] = new GameEndState();
            _stateInstances[GameState.Result] = new ResultState();
        }

        private void Update()
        {
            _currentStateInstance?.OnUpdate();
        }

        /// <summary>
        /// ステートを変更する
        /// </summary>
        /// <param name="gameState">次のステート</param>
        /// <param name="isInitial">初期ステートかどうか</param>
        public void ChangeState(GameState gameState)
        {
            // 現在のステートのOnExitを呼び出す
            _currentStateInstance?.OnExit();
            _currentStateInstance?.Dispose();
            _currentStateInstance = null;
            _currentGameStateProp.Value = gameState;

            // 辞書から該当ステートのインスタンスを探してOnEnterを呼び出す
            if (_stateInstances.TryGetValue(gameState, out var nextStateInstance))
            {
                _currentStateInstance = nextStateInstance;
                _currentStateInstance.OnEnter();
            }
            else
            {
                Debug.LogWarning($"ステート{gameState}のインスタンスが見つかりませんでした");
                return;
            }
            Debug.Log($"ステートを{gameState}に変更しました");
        }

        /// <summary>
        /// ステートを変更する（シーンの読み込み完了を待機してからOnEnterを呼び出す）
        /// </summary>
        /// <param name="gameState"></param>
        /// <param name="sceneName"></param>
        public async void ChangeStateWaitUntilSceneLoaded(GameState gameState, string sceneName)
        {
            Debug.Log($"ステートを{gameState}に変更するため、シーン{sceneName}の読み込みを待っています");
            _currentStateInstance?.OnExit();
            _currentStateInstance?.Dispose();
            _currentStateInstance = null;
            _currentGameStateProp.Value = gameState;

            // 全ての読み込まれているシーンを取得し、指定されたシーンが読み込まれるまで待機する
            while (true)
            {
                bool isSceneLoaded = false;
                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    Scene scene = SceneManager.GetSceneAt(i);
                    if (scene.name == sceneName)
                    {
                        isSceneLoaded = true;
                        break;
                    }
                }
                if (isSceneLoaded)
                {
                    break;
                }
                await UniTask.Yield();
            }

            // 辞書から該当ステートのインスタンスを探してOnEnterを呼び出す
            if (_stateInstances.TryGetValue(gameState, out var nextStateInstance))
            {
                // TODO ServiceRegistererの実行が完了するのを待つために一旦固定の時間待機しているが、より確実な方法があればそちらに変更する
                await UniTask.Delay(TimeSpan.FromSeconds(1));
                _currentStateInstance = nextStateInstance;
                _currentStateInstance.OnEnter();
                Debug.Log($"ステートを{gameState}に変更しました");
            }
            else
            {
                Debug.LogWarning($"ステート{gameState}のインスタンスが見つかりませんでした");
                return;
            }
        }

        public async void ChangeStateAsync(GameState nextState, Func<UniTask> asyncAction)
        {
            _currentStateInstance?.OnExit();
            _currentStateInstance?.Dispose();
            _currentStateInstance = null;

            // 非同期アクションを実行 ターン終了イベントの実行など
            await asyncAction();

            _currentGameStateProp.Value = nextState;
            if (_stateInstances.TryGetValue(nextState, out var nextStateInstance))
            {
                _currentStateInstance = nextStateInstance;
                _currentStateInstance.OnEnter();
            }
            else
            {
                Debug.LogWarning($"ステート{nextState}のインスタンスが見つかりませんでした");
                return;
            }
            Debug.Log($"ステートを{nextState}に変更しました");
        }
    }
}
