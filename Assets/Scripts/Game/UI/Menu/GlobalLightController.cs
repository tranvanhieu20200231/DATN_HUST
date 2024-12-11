using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class GlobalLightController : MonoBehaviour
{
    private Light2D globalLight;

    private void Start()
    {
        globalLight = GetComponent<Light2D>();

        float brightness = PlayerPrefs.GetFloat("Brightness", 0.25f);
        UpdateGlobalLight(brightness);
    }

    public void UpdateGlobalLight(float brightness)
    {
        if (globalLight != null)
        {
            globalLight.intensity = brightness;
        }
        else
        {
            Debug.LogWarning("Global Light 2D không được gán!");
        }
    }
}
