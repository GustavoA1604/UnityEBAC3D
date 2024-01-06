using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootLimit : GunBase
{
  public List<UIFillUpdater> uIFillUpdaters;
  public float maxShoot = 5f;
  public float timeToReload = 1f;

  private float _currentShoots;
  private bool _reloading = false;

  private void OnDisable()
  {
    _reloading = false;
  }

  private void OnEnable()
  {
    UpdateUI(maxShoot, maxShoot - _currentShoots);
    CheckReload();
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
    uIFillUpdaters.ForEach(i => i.UpdateValue(max, current));
  }

  public void AddUIGunUpdater(UIFillUpdater ui)
  {
    if (ui != null)
    {
      uIFillUpdaters.Add(ui);
    }
  }
}