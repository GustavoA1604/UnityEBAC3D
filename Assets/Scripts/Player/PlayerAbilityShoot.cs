using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
  public GunBase prefabGun1;
  private GunBase _gun1;
  public GunBase prefabGun2;
  private GunBase _gun2;
  public Transform gunPosition;
  private GunBase _currentGun;
  public UIFillUpdater uiGunUpdater;
  public FlashColor flashColor;

  protected override void Init()
  {
    base.Init();

    CreateGun(ref _gun1, ref prefabGun1);
    CreateGun(ref _gun2, ref prefabGun2);
    SwitchToGun1();

    inputs.Gameplay.SwitchToGun1.performed += ctx => SwitchToGun1();
    inputs.Gameplay.SwitchToGun2.performed += ctx => SwitchToGun2();

    inputs.Gameplay.Shoot.performed += ctx => StartShoot();
    inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();
  }

  private void CreateGun(ref GunBase actualGun, ref GunBase prefabGun)
  {
    if (prefabGun != null)
    {
      actualGun = Instantiate(prefabGun, gunPosition);
      actualGun.positionToShoot = gunPosition;
      actualGun.transform.localPosition = Vector3.zero;
      actualGun.transform.localEulerAngles = Vector3.zero;
      actualGun.gameObject.SetActive(false);

      if (actualGun is GunShootLimit)
      {
        GunShootLimit limitGun = actualGun as GunShootLimit;
        limitGun.AddUIGunUpdater(uiGunUpdater);
      }
    }
  }

  private void StartShoot()
  {
    _currentGun.StartShoot();
    flashColor?.Flash();
  }

  private void CancelShoot()
  {
    _currentGun.StopShoot();
  }

  private void SwitchToGun1()
  {
    SwitchToGun(ref _gun1);
  }

  private void SwitchToGun2()
  {
    SwitchToGun(ref _gun2);
  }

  private void SwitchToGun(ref GunBase actualGun)
  {
    if (_currentGun != null)
    {
      _currentGun.gameObject.SetActive(false);
    }
    _currentGun = actualGun;
    _currentGun.gameObject.SetActive(true);
  }
}
