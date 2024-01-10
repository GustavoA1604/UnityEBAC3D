using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMagneticTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CoinBase i = other.transform.GetComponent<CoinBase>();
        if (i != null)
        {
            i.gameObject.AddComponent<Magnetic>();
        }
    }
}
