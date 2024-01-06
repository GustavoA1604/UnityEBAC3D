using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform transformToLookAt;
    public bool active = true;

    private void OnValidate()
    {
        if (transformToLookAt == null)
        {
            transformToLookAt = FindObjectOfType<Player>().transform;
        }
    }

    public virtual void Update()
    {
        if (active)
        {
            transform.LookAt(transformToLookAt.position);
        }
    }
}
