using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase prefabProjectile;
    public Transform positionToShoot;
    public SfxType sfxType = SfxType.NONE;
    public float timeBetweenShoots = .2f;
    public float speed = 50f;
    private Coroutine _currentCoroutine = null;

    protected virtual IEnumerator ShootCorountine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(timeBetweenShoots);
        }
    }

    public virtual void Shoot()
    {
        var projectile = Instantiate(prefabProjectile);
        projectile.transform.position = positionToShoot.position;
        projectile.transform.rotation = positionToShoot.rotation;
        projectile.speed = speed;
        SfxPool.Play(sfxType);
    }

    public void StartShoot()
    {
        StopShoot();
        _currentCoroutine = StartCoroutine(ShootCorountine());
    }

    public void StopShoot()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }
    }
}
