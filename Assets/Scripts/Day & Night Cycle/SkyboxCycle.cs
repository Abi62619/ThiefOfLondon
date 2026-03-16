using UnityEngine;

public class SmoothSkyboxCycle : MonoBehaviour
{
    [Header("Skybox Materials")]
    [SerializeField] private Material sunriseSky;
    [SerializeField] private Material daySky;
    [SerializeField] private Material sunsetSky;
    [SerializeField] private Material nightSky;

    [Header("Cycle Settings")]
    [SerializeField] private float totalCycleMinutes = 30f;  // total cycle duration
    [SerializeField] private float transitionDuration = 30f; // seconds for smooth blend

    private Material currentSky;
    private Material nextSky;
    private float cycleTime;
    private float phaseLength;
    private float transitionTimer = 0f;
    private bool isTransitioning = false;

    void Start()
    {
        cycleTime = totalCycleMinutes * 60f;
        phaseLength = cycleTime / 4f;

        // Start with sunrise
        currentSky = new Material(sunriseSky);
        RenderSettings.skybox = currentSky;
    }

    void Update()
    {
        float time = Time.time % cycleTime;

        // Determine next skybox based on time
        if (time < phaseLength)
            nextSky = sunriseSky;
        else if (time < phaseLength * 2)
            nextSky = daySky;
        else if (time < phaseLength * 3)
            nextSky = sunsetSky;
        else
            nextSky = nightSky;

        // Start transition if skybox changes
        if (RenderSettings.skybox != nextSky && !isTransitioning)
        {
            StartCoroutine(TransitionSkybox(nextSky));
        }

        // Optional: slowly rotate skybox
        if (RenderSettings.skybox.HasProperty("_Rotation"))
        {
            float rotation = Time.time * 1f;
            RenderSettings.skybox.SetFloat("_Rotation", rotation);
        }
    }

    System.Collections.IEnumerator TransitionSkybox(Material targetSky)
    {
        isTransitioning = true;

        Material startSky = new Material(RenderSettings.skybox);

        float timer = 0f;

        while (timer < transitionDuration)
        {
            timer += Time.deltaTime;
            float t = timer / transitionDuration;

            // Lerp between the two skyboxes
            RenderSettings.skybox.Lerp(startSky, targetSky, t);

            yield return null;
        }

        RenderSettings.skybox = targetSky;
        isTransitioning = false;
    }
}