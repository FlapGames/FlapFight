using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

public class PauseMenu : MonoBehaviour
{
  public static bool GameIsPaused = false;
  
  public GameObject pauseMenuUI;
  public GameObject restartMenuUI;

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      if (GameIsPaused)
      {
        Resume();
      }
      else
      {
        Pause();
      }
    }

    if (PlayerController.GameIsOver)
    {
      Time.timeScale = 0f;

      restartMenuUI.SetActive(true);
    }
    else
    {
      restartMenuUI.SetActive(false);
    }
  }

  public void Resume()
  {
    Time.timeScale = 1f;

    GameIsPaused = false;

    pauseMenuUI.SetActive(false);
  }

  void Pause()
  {
    Time.timeScale = 0f;

    GameIsPaused = true;

    pauseMenuUI.SetActive(true);
  }

  public void LoadMenu()
  {
    Time.timeScale = 1f;

    SceneManager.LoadScene(0);
  }

  public void QuitGame()
  {
    Application.Quit();
  }

  public void Restart()
  {
    SceneManager.LoadScene(1);
  }
}
