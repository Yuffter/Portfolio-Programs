using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerSceneLoader : MonoBehaviour
{
    private static Queue <string> _scenesToLoad = new Queue<string>();
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize()
    {
        _scenesToLoad.Clear();
        _scenesToLoad.Enqueue(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("ManagerScene");
        SceneManager.LoadScene(_scenesToLoad.Dequeue(), LoadSceneMode.Additive);
    }
}
