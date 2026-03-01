using UnityEngine;

namespace Common
{
    [DisallowMultipleComponent]
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                // 一度もアクセスされていなかった場合はシーン内からインスタンスを探す
                if (_instance == null)
                {
                    _instance = FindAnyObjectByType<T>();

                    // それでも見つからなかった場合はエラーログを出す
                    if (_instance == null)
                    {
                        Debug.LogError(typeof(T) + "のインスタンスが存在しません。");
                    }
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            // すでにインスタンスが存在している場合は重複を破棄する
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            // インスタンスを設定する
            _instance = this as T;
        }
    }
}