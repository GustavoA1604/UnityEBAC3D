using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBase : MonoBehaviour
{
    public ItemType itemType;

    public string compareTag = "Player";
    public ParticleSystem myParticleSystem;
    public GameObject graphicItem;

    [Header("Sound")]
    public AudioSource audioSourceOnCollect;
    private bool _isCollected = false;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compareTag) && !_isCollected)
        {
            Collect();
        }

    }

    protected virtual void Collect()
    {
        _isCollected = true;
        OnCollect();
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
        ItemManager.AddItem(itemType);
    }

}
