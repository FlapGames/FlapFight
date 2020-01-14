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
    SceneManager.LoadScene("SampleScene");
  }

  public void PlayMap2()
  {
    SceneManager.LoadScene("Landside");
  }

  public void PlayMap3()
  {
    SceneManager.LoadScene("Cave");
  }

  public void PlayMap4()
  {
    SceneManager.LoadScene("Mountains");
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
