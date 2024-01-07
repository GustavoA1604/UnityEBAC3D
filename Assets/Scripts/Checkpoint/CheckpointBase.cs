using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class CheckpointBase : MonoBehaviour
{
  public MeshRenderer meshRenderer;
  private bool _isEnabled = false;

  public int key = 1;
  //private static string _checkpointKey = "CheckpointKey";

  private void OnTriggerEnter(Collider other)
  {
    if (other.transform.CompareTag("Player"))
    {
      CheckCheckpoint();
    }
  }

  private void CheckCheckpoint()
  {
    if (!_isEnabled)
    {
      TurnOn();
    }
  }

  private void TurnOn()
  {
    meshRenderer.material.SetColor("_EmissionColor", Color.white);
    _isEnabled = true;
    SaveCheckpoint();
  }

  private void TurnOff()
  {
    meshRenderer.material.SetColor("_EmissionColor", new Color(.001f, .001f, .001f));
    _isEnabled = false;
  }

  private void SaveCheckpoint()
  {
    /* if (PlayerPrefs.GetInt(_checkpointKey, 0) < key)
    {
      PlayerPrefs.SetInt(_checkpointKey, key);
    } */
    CheckpointManager.SaveCheckpoint(key);
  }

}
