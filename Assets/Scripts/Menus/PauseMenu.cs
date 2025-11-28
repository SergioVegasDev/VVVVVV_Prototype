using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool gamePaused = false;
    public static event Action<bool> PausePlayer = delegate { };
    public static event Action RestartCheckPoint = delegate { };
    public void Pause()
    {
        PausePlayer.Invoke(true);
        gamePaused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }
    public void Resume()
    {
        PausePlayer.Invoke(false);
        gamePaused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
    public void Restart()
    {
        PausePlayer.Invoke(false);
        gamePaused = false;
        Time.timeScale = 1f;
        RestartCheckPoint.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void CloseGame()
    {
        RestartCheckPoint.Invoke();
        Application.Quit();
        Debug.Log("Closed");
    }
    public void OnUsePauseMenu()
    {
        if (gamePaused)
        {
            Resume();
        }
        else { Pause(); }
    }
    private void OnEnable()
    {
        Player.UsePauseMenu += OnUsePauseMenu;
    }
    private void OnDisable()
    {
        Player.UsePauseMenu -= OnUsePauseMenu;
    }
    private void OnDestroy()
    {
        Player.UsePauseMenu -= OnUsePauseMenu;
    }
}
