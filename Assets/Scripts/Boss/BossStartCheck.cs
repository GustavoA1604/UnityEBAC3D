using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStartCheck : MonoBehaviour
{
    public string tagToCheck = "Player";
    public GameObject bossCamera;

    private void Awake()
    {
        bossCamera.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag(tagToCheck))
        {
            TurnCameraOn();
        }
    }

    private void TurnCameraOn()
    {
        bossCamera.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 1f, 0f, .5f);
        Gizmos.DrawSphere(transform.position, transform.localScale.y);
    }
}
