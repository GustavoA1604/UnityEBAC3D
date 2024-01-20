using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public List<MusicSetup> musicSetups;
    public List<SfxSetup> sfxSetups;
    public AudioSource musicSource;

    public static MusicSetup GetMusicByType(MusicType musicType)
    {
        return _instance.musicSetups.Find(i => i.musicType == musicType);
    }

    public static void PlayMusicByType(MusicType musicType)
    {
        var music = GetMusicByType(musicType);
        _instance.musicSource.clip = music.audioClip;
        _instance.musicSource.Play();
    }

    public static SfxSetup GetSfxByType(SfxType sfxType)
    {
        return _instance.sfxSetups.Find(i => i.sfxType == sfxType);
    }

    /* public static void PlaySfxByType(SfxType sfxType)
    {
        var sfx = GetSfxByType(sfxType);
        _instance.sfxSource.clip = sfx.audioClip;
        _instance.sfxSource.Play();
    } */
}

public enum MusicType
{
    TYPE_01,
    TYPE_02,
    TYPE_03,
}

[System.Serializable]
public class MusicSetup
{
    public MusicType musicType;
    public AudioClip audioClip;
}

public enum SfxType
{
    NONE,
    TYPE_01,
    TYPE_02,
    TYPE_03,
}

[System.Serializable]
public class SfxSetup
{
    public SfxType sfxType;
    public AudioClip audioClip;
}