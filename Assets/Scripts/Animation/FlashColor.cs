using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    [Header("Color")]
    public Color color = Color.red;
    public float duration = .1f;

    private Color _defaultColor;
    private Tween _currTween;

    public string colorParameter = "_EmissionColor";

    private void OnValidate()
    {
        if (meshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }
        if (skinnedMeshRenderer == null)
        {
            skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        }
    }

    private void Start()
    {
        if (meshRenderer != null)
            _defaultColor = meshRenderer.material.GetColor(colorParameter);
        else if (skinnedMeshRenderer != null)
            _defaultColor = skinnedMeshRenderer.material.GetColor(colorParameter);
        else
            Debug.Assert(false, "Flash Color needs at least one of mesh renderer or skinned mesh renderer");
    }

    [NaughtyAttributes.Button]
    public void Flash()
    {
        if (meshRenderer != null && (_currTween == null || !_currTween.IsActive()))
            _currTween = meshRenderer.material.DOColor(color, colorParameter, duration).SetLoops(2, LoopType.Yoyo);

        if (skinnedMeshRenderer != null && (_currTween == null || !_currTween.IsActive()))
            _currTween = skinnedMeshRenderer.material.DOColor(color, colorParameter, duration).SetLoops(2, LoopType.Yoyo);
    }
}
