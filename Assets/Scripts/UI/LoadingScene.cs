using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public Slider progressBar;       // Reference to the loading bar
    public TMP_Text progressText;        // Reference to the progress text
    public string sceneToLoad;       // Name of the scene to load

    private void Start()
    {
        sceneToLoad = MenuButtons.sceneToLoad;
        StartCoroutine(LoadSceneAsync());
    }

    IEnumerator LoadSceneAsync()
    {
        // Start loading the scene asynchronously
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);

        // Ensure the scene doesn’t activate until loading is complete
        operation.allowSceneActivation = false;

        // Loop until the loading is complete
        while (!operation.isDone)
        {
            // Update the progress bar and text
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;
            progressText.text = (progress * 100f).ToString("F0") + "%";

            // Activate the scene when it’s fully loaded
            if (operation.progress >= 0.9f)
            {
                progressText.text = "Press any key to continue"; // Optional: Wait for player input
                if (Input.anyKeyDown)
                {
                    operation.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}
