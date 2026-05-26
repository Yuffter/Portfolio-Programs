using System.Collections.Generic;
using UnityEngine;

namespace MinutesGame.Common.SE
{
    /// <summary>
    /// SEデータベースクラス
    /// </summary>
    [CreateAssetMenu(fileName = "SEDatabase", menuName = "MinutesGame/SO/SE/SEDatabase")]
    public class SEDatabase : ScriptableObject
    {
        [SerializeField] private SEDatum[] _seData;
        /// <summary>
        /// SEデータ配列取得
        /// </summary>
        public SEDatum[] SEData => _seData;

        private Dictionary<SEName, AudioClip> _seDictionary;

        /// <summary>
        /// 自動生成されたEnumとAudioClipの対応付け辞書を初期化する
        /// </summary>
        public void Initialize()
        {
            _seDictionary = new Dictionary<SEName, AudioClip>();
            foreach (var datum in _seData)
            {
                if (System.Enum.TryParse<SEName>(datum.SEName, out var seName))
                {
                    _seDictionary[seName] = datum.AudioClip;
                }
                else
                {
                    Debug.LogWarning($"SEDatabase: Invalid SEName '{datum.SEName}' in SEDatum.");
                }
            }
        }

        /// <summary>
        /// SENameからAudioClipを取得する
        /// </summary>
        /// <param name="seName">取得したいSEの名前</param>
        /// <returns>対応するAudioClip、存在しない場合はnull</returns>
        public AudioClip GetAudioClip(SEName seName)
        {
            if (_seDictionary != null && _seDictionary.TryGetValue(seName, out var clip))
            {
                return clip;
            }
            Debug.LogWarning($"SEDatabase: SEName '{seName}' not found in dictionary.");
            return null;
        }
    }

    [System.Serializable]
    public class SEDatum
    {
        [SerializeField] private string _seName;
        [SerializeField] private AudioClip _audioClip;
        /// <summary>
        /// SE名
        /// </summary>
        public string SEName => _seName;
        /// <summary>
        /// オーディオクリップ
        /// </summary>
        public AudioClip AudioClip => _audioClip;
    }
}
