using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
  public GunBase prefabGunBase;
  public Transform gunPosition;
  private GunBase _currentGun;

  protected override void Init()
  {
    base.Init();

    CreateGun();

    inputs.Gameplay.Shoot.performed += ctx => StartShoot();
    inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();
  }

  private void CreateGun()
  {
    _currentGun = Instantiate(prefabGunBase, gunPosition);
    _currentGun.positionToShoot = gunPosition;
    _currentGun.transform.localPosition = Vector3.zero;
    _currentGun.transform.localEulerAngles = Vector3.zero;
  }

  private void StartShoot()
  {
    Debug.Log("StartShoot");
    _currentGun.StartShoot();
  }

  private void CancelShoot()
  {
    Debug.Log("CancelShoot");
    _currentGun.StopShoot();
  }
}
