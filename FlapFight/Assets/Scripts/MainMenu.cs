using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
  public void QuitGame()
  {
    Application.Quit();
  }

  public void SetVolume(float volume)
  {
    Debug.Log(volume);
  }
}
