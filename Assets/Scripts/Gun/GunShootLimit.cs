using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{
  public float maxShoot = 5f;
  public float timeToReload = 1f;

  private float _currentShoots;
  private bool _reloading = false;

  protected override IEnumerator ShootCorountine()
  {
    if (_reloading) yield break;

    while (true)
    {
      Shoot();
      _currentShoots++;
      CheckReload();
      yield return new WaitForSeconds(timeBetweenShoots);
    }
  }

  private void CheckReload()
  {
    if (_currentShoots >= maxShoot)
    {
      StopShoot();
      StartReload();
    }
  }

  private void StartReload()
  {
    _reloading = true;
    StartCoroutine(ReloadCoroutine());
  }

  IEnumerator ReloadCoroutine()
  {
    float time = 0;
    while (time < timeToReload)
    {
      time += Time.deltaTime;
      yield return new WaitForEndOfFrame();
    }
    _currentShoots = 0;
    _reloading = false;
  }
}