using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneManagerEx : MonoBehaviour
{
    static SceneManagerEx _sceneManagerEx;
    public static SceneManagerEx Instance
    {
        get
        {
            return _sceneManagerEx;
        }
    }

    private void Awake()
    {
        if (_sceneManagerEx == null)
        {
            _sceneManagerEx = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 文字列指定で1つのシーンのロード及びアンロードを行う
    /// </summary>
    /// <param name="unloadScene"></param>
    /// <param name="loadScene"></param>
    /// <param name="callback"></param>
    public void LoadAndUnloadScene(string unloadScene, string loadScene, Action callback = null)
    {
        StartCoroutine(_LoadAndUnloadScene(unloadScene, loadScene, callback));
    }
    IEnumerator _LoadAndUnloadScene(string unloadScene, string loadScene, Action callback)
    {
        if (unloadScene != null) yield return SceneManager.UnloadSceneAsync(unloadScene);
        if (loadScene != null) yield return SceneManager.LoadSceneAsync(loadScene, LoadSceneMode.Additive);
        callback?.Invoke();
    }

    /// <summary>
    /// ビルド番号指定で1つのシーンのロード及びアンロードを行う
    /// </summary>
    /// <param name="unloadScene"></param>
    /// <param name="loadScene"></param>
    /// <param name="callback"></param>
    public void LoadAndUnloadScene(int? unloadScene, int? loadScene, Action callback = null)
    {
        StartCoroutine(_LoadAndUnloadScene(unloadScene, loadScene, callback));
    }
    IEnumerator _LoadAndUnloadScene(int? unloadScene, int? loadScene, Action callback)
    {
        if (unloadScene != null) yield return SceneManager.UnloadSceneAsync(unloadScene.Value);
        if (loadScene != null) yield return SceneManager.LoadSceneAsync(loadScene.Value, LoadSceneMode.Additive);
        callback?.Invoke();
    }

    /// <summary>
    /// 文字列指定で複数のシーンのロード及びアンロードを行う
    /// </summary>
    /// <param name="unloadScenes"></param>
    /// <param name="loadScenes"></param>
    /// <param name="callback"></param>
    public void LoadAndUnloadScene(List<string> unloadScenes,List<string> loadScenes, Action callback = null)
    {
        StartCoroutine(_LoadAndUnloadScene(unloadScenes, loadScenes, callback));
    }

    IEnumerator _LoadAndUnloadScene(List<string> unloadScenes, List<string> loadScenes, Action callback = null)
    {
        if (unloadScenes != null)
        {
            for (int i = 0;i < unloadScenes.Count;i++)
            {
                yield return SceneManager.UnloadSceneAsync(unloadScenes[i]);
            }
        }
        if (loadScenes != null)
        {
            for (int i = 0; i < loadScenes.Count; i++)
            {
                yield return SceneManager.LoadSceneAsync(loadScenes[i], LoadSceneMode.Additive);
            }
        }

        callback?.Invoke();
    }

    /// <summary>
    /// ビルド番号指定で複数のシーンのロード及びアンロードを行う
    /// </summary>
    /// <param name="unloadScenes"></param>
    /// <param name="loadScenes"></param>
    /// <param name="callback"></param>
    public void LoadAndUnloadScene(List<int?> unloadScenes, List<int?> loadScenes, Action callback = null)
    {
        StartCoroutine(_LoadAndUnloadScene(unloadScenes, loadScenes, callback));
    }

    IEnumerator _LoadAndUnloadScene(List<int?> unloadScenes, List<int?> loadScenes, Action callback = null)
    {
        if (unloadScenes != null)
        {
            for (int i = 0; i < unloadScenes.Count; i++)
            {
                yield return SceneManager.UnloadSceneAsync(unloadScenes[i].Value);
            }
        }
        if (loadScenes != null)
        {
            for (int i = 0; i < loadScenes.Count; i++)
            {
                yield return SceneManager.LoadSceneAsync(loadScenes[i].Value, LoadSceneMode.Additive);
            }
        }

        callback?.Invoke();
    }
}
