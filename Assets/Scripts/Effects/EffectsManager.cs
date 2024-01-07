using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class EffectsManager : MonoBehaviour
{
    public PostProcessVolume processVolume;
    [Header("Vignette")]
    public float vignetteFlashDuration = .1f;
    [SerializeField] private Vignette _vignette;

    private static EffectsManager _instance;

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

    public static void FlashColorVignette()
    {
        _instance.StartCoroutine(FlashColorVignetteCoroutine());
    }

    private static IEnumerator FlashColorVignetteCoroutine()
    {
        Vignette tmp;
        if (_instance.processVolume.profile.TryGetSettings<Vignette>(out tmp))
        {
            _instance._vignette = tmp;
        }

        ColorParameter c = new ColorParameter();

        float time = 0;
        while (time < _instance.vignetteFlashDuration)
        {
            c.value = Color.Lerp(Color.black, Color.red, time / _instance.vignetteFlashDuration);
            _instance._vignette.color.Override(c);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        time = 0;
        while (time < _instance.vignetteFlashDuration)
        {
            c.value = Color.Lerp(Color.red, Color.black, time / _instance.vignetteFlashDuration);
            _instance._vignette.color.Override(c);
            time += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }
}
