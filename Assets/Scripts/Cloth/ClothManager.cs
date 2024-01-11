using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClothType
{
    SPEED,
}

public class ClothManager : MonoBehaviour
{
    private static ClothManager _instance;
    public List<ClothSetup> clothSetups;

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

    public static ClothSetup GetSetupByType(ClothType cloth)
    {
        return _instance.clothSetups.Find(i => i.clothType == cloth);
    }
}

[System.Serializable]
public class ClothSetup
{
    public ClothType clothType;
    public Texture2D texture;
}