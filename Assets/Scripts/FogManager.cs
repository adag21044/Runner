using UnityEngine;

public class FogManager : MonoBehaviour
{
    [SerializeField] private bool enableFog = true; // Toggle fog on or off
    [SerializeField] private Color fogColor = Color.gray; // Fog color
    [SerializeField] private FogMode fogMode = FogMode.Linear; // Fog mode
    [SerializeField] private float fogStartDistance = 50f; // For Linear mode
    [SerializeField] private float fogEndDistance = 200f; // For Linear mode
    [SerializeField] private float fogDensity = 0.01f; // For Exponential modes

    private void Start()
    {
        // Enable or disable fog
        RenderSettings.fog = enableFog;

        if (enableFog)
        {
            RenderSettings.fogColor = fogColor;
            RenderSettings.fogMode = fogMode;

            if (fogMode == FogMode.Linear)
            {
                RenderSettings.fogStartDistance = fogStartDistance;
                RenderSettings.fogEndDistance = fogEndDistance;
            }
            else
            {
                RenderSettings.fogDensity = fogDensity;
            }
        }
    }
}
