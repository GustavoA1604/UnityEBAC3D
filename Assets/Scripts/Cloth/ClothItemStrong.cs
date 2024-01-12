using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothItemStrong : ClothItemBase
{
    public float defenseMultiplier = 1.5f;
    public override void Collect()
    {
        base.Collect();
        Player._instance.healthBase.ChangeDefense(defenseMultiplier, powerUpDuration);
    }
}
