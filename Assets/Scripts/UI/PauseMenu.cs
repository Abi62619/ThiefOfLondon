using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;

    public void PauseGame()
    {
        Time.timeScale = 0f; // Pause the game
        pauseMenuPanel.SetActive(true); // Show the pause menu
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; // Resume the game
        pauseMenuPanel.SetActive(false); // Hide the pause menu
    }

    public void QuitGame()
    {
        Time.timeScale = 1f; // Ensure the game is not paused
        Application.Quit(); // Quit the application
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Ensure the game is not paused
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reload the current scene
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Ensure the game is not paused
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene (replace with your main menu scene name)
    }
}