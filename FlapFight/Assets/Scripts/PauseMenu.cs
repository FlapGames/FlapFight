using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
  public static bool GameIsPaused = false;

  public GameObject Player1;
  public GameObject Player2;
  [SerializeField] Text Winner;

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

      CalculateWinner();

      restartMenuUI.SetActive(true);
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

  void CalculateWinner()
  {
    if (Player1.GetComponent<PlayerController>().PlayerIsDeath || Player2.GetComponent<PlayerController>().numberOfLives > Player1.GetComponent<PlayerController>().numberOfLives)
    {
      Winner.text = "PLAYER 2";
    }
    else if (Player2.GetComponent<PlayerController>().PlayerIsDeath || Player2.GetComponent<PlayerController>().numberOfLives > Player1.GetComponent<PlayerController>().numberOfLives)
    {
      Winner.text = "PLAYER 1";
    }
    else if (Player1.GetComponent<PlayerController>().numberOfLives == Player2.GetComponent<PlayerController>().numberOfLives)
    {
      Winner.text = "DRAW";
    }
  }

}
