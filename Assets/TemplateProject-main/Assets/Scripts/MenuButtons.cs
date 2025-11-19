using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public static string sceneToLoad;

    public void QuitGame()
    {
        Application.Quit();
    } 
   
    public void StartGame(string sceneName)
    {
        sceneToLoad = sceneName;
        SceneManager.LoadScene(1);

    }

    public void LoadScene(int sceneNo)
    {
        SceneManager.LoadScene(sceneNo);

    }

    public void ContinueGame()
    {
        Time.timeScale = 1.0f;
    }

}
