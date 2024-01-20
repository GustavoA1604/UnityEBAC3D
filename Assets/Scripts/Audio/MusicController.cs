using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    public Image menuSoundImage;
    public AudioMixer audioMixer;

    public bool _isOn = true;

    public void SwitchAudio()
    {
        menuSoundImage.sprite = _isOn ? soundOffSprite : soundOnSprite;
        audioMixer.SetFloat("Volume", _isOn ? -80 : 0);
        _isOn = !_isOn;
    }
}
