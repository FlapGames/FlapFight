using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
  public GameObject mapMenu;
  public GameObject mainMenu;
  public void QuitGame()
  {
    Application.Quit();
  }

  public void SetVolume(float volume)
  {
    Debug.Log(volume);
  }

  public void SwitchToMapMenu()
  {
    mainMenu.SetActive(false);
    mapMenu.SetActive(true);
  }
}
