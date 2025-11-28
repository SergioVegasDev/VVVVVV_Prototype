using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private GameObject winMenu;

    public static event Action RestartCheckPoint = delegate { };
    public static event Action<bool> PausePlayer = delegate { };
    public void Restart()
    {
        Time.timeScale = 1f;
        RestartCheckPoint.Invoke();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        PausePlayer.Invoke(true);
        winMenu.SetActive(false);
    }
    public void CloseGame()
    {
        RestartCheckPoint.Invoke();
        Application.Quit();
        Debug.Log("Closed");
    }
    public void OnUsWinMenu()
    {
        winMenu.SetActive(true);
        PausePlayer.Invoke(true);
        Time.timeScale = 0;
    }
    private void OnEnable()
    {
        Player.UseWinMenu += OnUsWinMenu;
    }
    private void OnDisable()
    {
        Player.UseWinMenu -= OnUsWinMenu;
    }
    private void OnDestroy()
    {
        Player.UseWinMenu -= OnUsWinMenu;
    }
}
