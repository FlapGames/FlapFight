using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
  public Image currentHealthBar;

  float currentTime;
  float startingTime;
  float numberOfLives;


  [SerializeField] Text coundownText;

  public GameObject TimeModeDisplay;

  Color color = Color.red;

  public GameObject player;

  private void Start()
  {
    startingTime = 12f;

    numberOfLives = 1f;

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
        //ToDo
        coundownText.color = color;
      }
      //ToDo
      coundownText.text = currentTime.ToString("00:00");
    }
  }

  //ToDo
  public void updateHealthbar(float damage)
  {
    if (numberOfLives - damage <= 0)
    {
      numberOfLives = 0;
    }
    else
    {
      numberOfLives -= damage;
    }

    //player.GetComponent<PlayerController>().knockbackMultiplier
    currentHealthBar.rectTransform.localScale = new Vector3(numberOfLives, 1, 1);
  }
}
