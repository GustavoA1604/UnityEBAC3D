using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float timeToDestroy = 1f;
    public int damage = 1;
    public float speed = 50f;

    public List<string> tagsToHit;

    private void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (var t in tagsToHit)
        {
            if (collision.transform.CompareTag(t))
            {
                var damageable = collision.transform.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    Vector3 dir = collision.transform.position - transform.position;
                    dir = dir.normalized;
                    damageable.Damage(damage, dir);
                }
                Destroy(gameObject);
                break;
            }
        }
    }
}
