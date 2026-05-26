using UnityEngine;

[DisallowMultipleComponent]
public class SingletonBase<T> : MonoBehaviour
    where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance is not null)
                return _instance;

            _instance = (T)FindObjectOfType(typeof(T));

            if (_instance is null)
            {
                Debug.LogError(typeof(T) + "is nothing");
            }

            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance is not null && _instance != this)
        {
            return;
        }

        _instance = this as T;
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }
}