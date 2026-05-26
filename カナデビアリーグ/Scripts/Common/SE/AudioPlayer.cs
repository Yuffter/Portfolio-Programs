using UnityEngine;

namespace MinutesGame.Common.SE
{
    /// <summary>
    /// オーディオ再生クラス
    /// </summary>
    public struct AudioPlayer
    {
        private AudioSource _audioSource;
        public AudioSource AudioSource => _audioSource;
        public AudioPlayer(AudioSource audioSource)
        {
            _audioSource = audioSource;
        }

        /// <summary>
        /// オーディオを再生する
        /// <para>clip: 再生するオーディオクリップ</para>
        /// <para>volume: 音量</para>
        /// <para>pitch: ピッチ</para>
        /// <para>loop: ループ再生するかどうか</para>
        /// </summary>
        /// <param name="clip">再生するオーディオクリップ</param>
        /// <param name="volume">音量</param>
        /// <param name="pitch">ピッチ</param>
        /// <param name="loop">ループ再生するかどうか</param>
        public void Play(AudioClip clip, float volume = 1f, float pitch = 1f, bool loop = false)
        {
            _audioSource.clip = clip;
            _audioSource.volume = volume;
            _audioSource.pitch = pitch;
            _audioSource.loop = loop;
            _audioSource.Play();
        }

        /// <summary>
        /// オーディオを停止する
        /// </summary>
        public void Stop()
        {
            _audioSource.Stop();
            _audioSource.clip = null;
        }

        /// <summary>
        /// オーディオを一時停止する
        /// </summary>
        public void Pause()
        {
            _audioSource.Pause();
        }

        /// <summary>
        /// オーディオの再生を再開する
        /// </summary>
        public void Resume()
        {
            _audioSource.UnPause();
        }

        /// <summary>
        /// オーディオが再生中かどうか
        /// </summary>
        /// <returns></returns>
        public bool IsPlaying()
        {
            return _audioSource.isPlaying;
        }
    }
}
