using UnityEngine;

namespace MinutesGame.Common.SE
{
    /// <summary>
    /// SE管理クラス
    /// </summary>
    public class SEManager : SingletonMonoBehaviour<SEManager>
    {
        [SerializeField] private SESettings _settings;
        [SerializeField] private SEDatabase _database;
        private AudioPlayer[] _audioPlayers;
        protected override void Awake()
        {
            base.Awake();
            //初期化処理
            Initialize();
        }

        private void Initialize()
        {
            //AudioPlayerの配列を作成
            _audioPlayers = new AudioPlayer[_settings.MaxSimultaneousSECount];
            for (int i = 0; i < _audioPlayers.Length; i++)
            {
                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                AudioPlayer audioPlayer = new AudioPlayer(audioSource);
                _audioPlayers[i] = audioPlayer;
            }

            // 辞書の初期化
            _database.Initialize();
        }

         /// <summary>
         /// SEを再生する
         /// </summary>
         /// <param name="seName">再生するSEの名前</param>
         /// <param name="volume">音量</param>
         /// <param name="pitch">ピッチ</param>
         /// <param name="loop">ループ再生するかどうか</param>
        public void Play(SEName seName, float volume = 1f, float pitch = 1f, bool loop = false)
        {
            // 利用可能なAudioPlayerを探す
            AudioPlayer? availablePlayer = null;
            foreach (var player in _audioPlayers)
            {
                if (!player.IsPlaying())
                {
                    availablePlayer = player;
                    break;
                }
            }

            if (availablePlayer.HasValue)
            {
                AudioClip clip = _database.GetAudioClip(seName);
                float adjustedVolume = volume * _settings.MasterVolume;
                availablePlayer.Value.Play(clip, adjustedVolume, pitch, loop);
            }
            else
            {
                Debug.LogWarning("利用可能なAudioPlayerがありません。");
            }
        }

        /// <summary>
        /// SEを停止する
        /// </summary>
        /// <param name="seName">停止するSEの名前</param>
        public void Stop(SEName seName)
        {
            AudioClip clip = _database.GetAudioClip(seName);
            foreach (var player in _audioPlayers)
            {
                if (player.IsPlaying() && player.AudioSource.clip == clip)
                {
                    player.Stop();
                }
            }
        }

        /// <summary>
        /// 全てのSEを停止する
        /// </summary>
        public void StopAll()
        {
            foreach (var player in _audioPlayers)
            {
                if (player.IsPlaying())
                {
                    player.Stop();
                }
            }
        }

        /// <summary>
        /// SEを一時停止する
        /// </summary>
        /// <param name="seName">一時停止するSEの名前</param>
        public void Pause(SEName seName)
        {
            AudioClip clip = _database.GetAudioClip(seName);
            foreach (var player in _audioPlayers)
            {
                if (player.IsPlaying() && player.AudioSource.clip == clip)
                {
                    player.Pause();
                }
            }
        }

        /// <summary>
        /// 全てのSEを一時停止する
        /// </summary>
        public void PauseAll()
        {
            foreach (var player in _audioPlayers)
            {
                if (player.IsPlaying())
                {
                    player.Pause();
                }
            }
        }

        /// <summary>
        /// SEの再生を再開する
        /// </summary>
        /// <param name="seName">再開するSEの名前</param>
        public void Resume(SEName seName)
        {
            AudioClip clip = _database.GetAudioClip(seName);
            foreach (var player in _audioPlayers)
            {
                if (player.AudioSource.clip == clip)
                {
                    player.Resume();
                }
            }
        }

        /// <summary>
        /// 全てのSEの再生を再開する
        /// </summary>
        public void ResumeAll()
        {
            foreach (var player in _audioPlayers)
            {
                player.Resume();
            }
        }
    }
}
