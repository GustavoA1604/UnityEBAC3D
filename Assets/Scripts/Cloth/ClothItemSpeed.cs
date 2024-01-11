using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothItemSpeed : ClothItemBase
{
    public float speedMultiplier = 1.5f;
    public override void Collect()
    {
        base.Collect();
        Player._instance.ChangeSpeed(speedMultiplier, powerUpDuration);
    }
}
