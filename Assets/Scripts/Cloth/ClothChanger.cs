using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClothChanger : MonoBehaviour
{
    public SkinnedMeshRenderer mesh;

    public Texture2D texture;
    public string shaderIdName = "_EmissionMap";

    private Texture2D _defaultTexture;
    private float _currrentTextureCounter = 0;
    private ClothSetup _currentSetup = null;

    private void Awake()
    {
        _defaultTexture = (Texture2D)mesh.materials[0].GetTexture(shaderIdName);
    }

    private void Update()
    {
        if (_currrentTextureCounter > 0)
        {
            _currrentTextureCounter -= Time.deltaTime;
            if (_currrentTextureCounter < 0)
            {
                ResetTexture();
            }
        }
    }

    private void SetTexture(ClothSetup setup)
    {
        mesh.materials[0].SetTexture(shaderIdName, setup.texture);
    }

    public void ResetTexture()
    {
        mesh.materials[0].SetTexture(shaderIdName, _defaultTexture);
        _currentSetup = null;
        _currrentTextureCounter = 0;
    }

    public void ChangeTexture(ClothSetup setup, float duration)
    {
        if (duration > 0)
        {
            SetTexture(setup);
            _currrentTextureCounter = duration;
            _currentSetup = setup;
        }
    }

    public ClothSetup GetCurrentClothSetup()
    {
        return _currentSetup;
    }

    public float GetCurrentDuration()
    {
        return _currrentTextureCounter;
    }
}
