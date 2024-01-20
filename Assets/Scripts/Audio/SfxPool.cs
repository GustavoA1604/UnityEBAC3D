using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxPool : MonoBehaviour
{
    private static SfxPool _instance;

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
        CreatePool();
    }

    private List<AudioSource> _audioSourceList;

    public int poolSize = 10;

    private int _index = 0;

    private void CreatePool()
    {
        _audioSourceList = new List<AudioSource>();

        for (int i = 0; i < 10; i++)
        {
            CreateAudioSourceItem();
        }
    }

    private void CreateAudioSourceItem()
    {

        GameObject go = new GameObject("SFX_Pool");
        go.transform.SetParent(gameObject.transform);
        _audioSourceList.Add(go.AddComponent<AudioSource>());
    }

    private void PlayInternal(SfxType sfxType)
    {
        SfxSetup sfxSetup = SoundManager.GetSfxByType(sfxType);
        _audioSourceList[_index].clip = sfxSetup.audioClip;
        _audioSourceList[_index].Play();
        _index = (_index + 1) % poolSize;
    }

    public static void Play(SfxType sfxType)
    {
        if (sfxType != SfxType.NONE)
        {
            _instance.PlayInternal(sfxType);
        }
    }
}
