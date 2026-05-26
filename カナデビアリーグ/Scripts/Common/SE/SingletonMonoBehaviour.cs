using UnityEngine;

namespace MinutesGame.Common.SE
{
    /// <summary>
    /// シングルトンMonoBehaviour基底クラス
    /// </summary>
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        /// <summary>
        /// インスタンス取得
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<T>();

                    if (_instance == null)
                    {
                        SetupInstance();
                    }
                }

                return _instance;
            }
        }


        protected virtual void Awake()
        {
            if (RemoveDuplicates()) return;
        }

        /// <summary>
        /// インスタンスのセットアップ
        /// </summary>
        private static void SetupInstance()
        {
            GameObject singletonObject = new GameObject(typeof(T).Name);
            _instance = singletonObject.AddComponent<T>();
            DontDestroyOnLoad(singletonObject);
        }

        /// <summary>
        /// 重複したインスタンスの削除
        /// </summary>
        private bool RemoveDuplicates()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
                return false;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
                return true;
            }
            return false;
        }
    }
}
