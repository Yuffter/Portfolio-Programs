using UnityEngine;
using UnityEngine.Video;

namespace Common
{
    /// <summary>
    /// WebGLビルドにおける自動再生の不具合を回避するためのクラス<br/>
    /// 再生をプログラムから開始することで正常に動作するようになります
    /// </summary>
    public class VideoStarter : MonoBehaviour
    {
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            GetComponent<VideoPlayer>().Play();
        }
    }
}
