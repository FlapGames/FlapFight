using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
  float currentTime;
  float startingTime;
  
  [SerializeField] Text coundownText;

  public GameObject TimeModeDisplay;

  Color color = Color.red;

  private void Start()
  {
    startingTime = 59f;

    currentTime = startingTime;
  }

  private void Update()
  {
    if (MapMenu.timeMode)
    {
      if (currentTime >= 0f)
      {
        currentTime -= 1 * Time.deltaTime;
      }
      else
      {
        PlayerController.GameIsOver = true;
      }

      if (currentTime <= 10f)
      {
        coundownText.color = color;
      }
      coundownText.text = currentTime.ToString("00:00");
    }
  }
}
