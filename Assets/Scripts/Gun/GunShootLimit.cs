using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{
  public List<UIGunUpdater> uIGunUpdaters;
  public float maxShoot = 5f;
  public float timeToReload = 1f;

  private float _currentShoots;
  private bool _reloading = false;

  private void Awake()
  {
    GetAllUIs();
  }

  protected override IEnumerator ShootCorountine()
  {
    if (_reloading) yield break;

    while (true)
    {
      Shoot();
      _currentShoots++;
      CheckReload();
      UpdateUI(maxShoot, maxShoot - _currentShoots);
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
      UpdateUI(timeToReload, time);
      time += Time.deltaTime;
      yield return new WaitForEndOfFrame();
    }
    UpdateUI(1, 1);
    _currentShoots = 0;
    _reloading = false;
  }

  private void UpdateUI(float max, float current)
  {
    uIGunUpdaters.ForEach(i => i.UpdateValue(max, current));
  }

  private void GetAllUIs()
  {
    uIGunUpdaters = GameObject.FindObjectsOfType<UIGunUpdater>().ToList();
  }
}