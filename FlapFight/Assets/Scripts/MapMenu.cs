using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMenu : MonoBehaviour
{
  public static bool survivalMode;
  public static bool timeMode;

  public void PlayMap1()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
  }

  public void PlayMap2()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
  }

  public void PlayMap3()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
  }

  public void PlayMap4()
  {
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
  }

  public void TimeMode()
  {
    survivalMode = false;
    timeMode = true;
  }

  public void SurvivalMode()
  {
    timeMode = false;
    survivalMode = true;
  }
}
