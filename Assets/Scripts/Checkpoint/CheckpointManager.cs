using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private static CheckpointManager _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public int lastCheckpoint = 0;
    public List<CheckpointBase> checkpoints;

    public static void SaveCheckpoint(int i)
    {
        if (i > _instance.lastCheckpoint)
        {
            _instance.lastCheckpoint = i;
        }
    }

    public static Vector3 GetRespawnPosition()
    {
        var checkpoint = _instance.checkpoints.Find(i => i.key == _instance.lastCheckpoint);
        return checkpoint.transform.position;
    }

    public static bool HasCheckpoint()
    {
        return _instance.lastCheckpoint > 0;
    }
}
