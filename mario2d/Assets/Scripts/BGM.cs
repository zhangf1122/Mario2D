using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public static BGM Instance;//创建单例类
    public AudioSource MusicPlayer;
    public AudioSource SoundPlayer;
    void Start()
    {
        Instance = this;
    }
    void Update()
    {
        
    }

    /// <summary>
    /// 播放音乐
    /// </summary>
    /// <param name="MusicName"></param>
    public void PlayMusic(string MusicName)
    {
        if (MusicPlayer.isPlaying == false)
        {
            AudioClip clip = Resources.Load<AudioClip>(name);
            MusicPlayer.clip = clip;
            MusicPlayer.Play();
        }
    }

    /// <summary>
    /// 暂停播放音乐
    /// </summary>
    public void PauseMusic()
    {
        MusicPlayer.Stop();
    }

    /// <summary>
    /// 播放音效
    /// </summary>
    public void PlaySound(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>(name);
        SoundPlayer.PlayOneShot(clip);
    }
}
