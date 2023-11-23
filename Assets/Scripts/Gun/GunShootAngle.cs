using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootAngle : GunShootLimit
{
    public int numberPerShoot = 4;
    public float angle = 15f;

    public override void Shoot()
    {
        float deltaAngle = numberPerShoot == 1 ? 0 : ((angle * 2) / (numberPerShoot - 1));
        float startAngle = numberPerShoot == 1 ? 0 : -angle;
        for (int i = 0; i < numberPerShoot; i++)
        {
            var projectile = Instantiate(prefabProjectile, positionToShoot);
            projectile.transform.localPosition = Vector3.zero;
            projectile.transform.localEulerAngles = Vector3.zero + Vector3.up * (startAngle + i * deltaAngle);
            projectile.speed = speed;
            projectile.transform.parent = null;
        }
    }
}
