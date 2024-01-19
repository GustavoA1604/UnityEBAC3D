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

    public static void ResetCheckpoints()
    {
        _instance.lastCheckpoint = 0;
        _instance.checkpoints.ForEach(i => i.TurnOff());
    }

    public static int GetLastCheckpoint()
    {
        return _instance.lastCheckpoint;
    }

    public static void SaveCheckpoint(int i)
    {
        if (i > _instance.lastCheckpoint)
        {
            _instance.lastCheckpoint = i;
        }
    }

    public static Vector3 GetRespawnPosition()
    {
        if (!HasCheckpoint())
        {
            return Player._instance.GetInitialPosition();
        }

        var checkpoint = _instance.checkpoints.Find(i => i.key == _instance.lastCheckpoint);
        return checkpoint.transform.position;
    }

    public static bool HasCheckpoint()
    {
        return _instance.lastCheckpoint > 0;
    }
}
