using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] private GameObject gameOverMenu;
    public static event Action RestartCheckPoint = delegate { };
    public void TryAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameOverMenu.SetActive(false);
       
    }
    public void CloseGame()
    {
        RestartCheckPoint.Invoke();
        Application.Quit();
        Debug.Log("Closed");
    }
    public void OnUseGameOverMenu()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
    }
    private void OnEnable()
    {
        Player.UseGameOverMenu += OnUseGameOverMenu;
    }
    private void OnDisable()
    {
        Player.UseGameOverMenu -= OnUseGameOverMenu;
    }
    private void OnDestroy()
    {
        Player.UseGameOverMenu -= OnUseGameOverMenu;
    }
}
