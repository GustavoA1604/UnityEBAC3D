using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothItemBase : MonoBehaviour
{
    public ClothType clothType;
    public string compareTag = "Player";
    public ParticleSystem myParticleSystem;
    [Header("Sound")]
    public AudioSource audioSourceOnCollect;
    private bool _isCollected = false;

    [Header("Item")]
    public GameObject graphicItem;

    public float powerUpDuration = 3f;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compareTag) && !_isCollected)
        {
            Collect();
        }

    }

    public virtual void Collect()
    {
        _isCollected = true;
        OnCollect();

        var setup = ClothManager.GetSetupByType(clothType);
        Player._instance.clothChanger.ChangeTexture(setup, powerUpDuration);

        StartCoroutine(HideObject());
    }

    private IEnumerator HideObject()
    {
        graphicItem.gameObject.SetActive(false);
        float sec = myParticleSystem == null ? 0 : myParticleSystem.main.duration;
        yield return new WaitForSeconds(sec);
        gameObject.SetActive(false);

    }

    protected virtual void OnCollect()
    {
        if (myParticleSystem != null)
        {
            myParticleSystem.Play();
        }
        if (audioSourceOnCollect != null)
        {
            audioSourceOnCollect?.Play();
        }
    }
}
