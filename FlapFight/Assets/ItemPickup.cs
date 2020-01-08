using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
  public int itemID = 0;
  SpriteRenderer spriteRenderer;
  BoxCollider2D boxCollider;

  public float startTimeInvisible;
  float currentTimeInvisible;
  bool invisible;

  public void OnTriggerEnter2D(Collider2D collision)
  {
    PlayerController player = collision.GetComponent<PlayerController>();
    Debug.Log("trigger");
    if (player != null)
    {
      player.PickUpItem(itemID);
      currentTimeInvisible = startTimeInvisible;
      invisible = true;
    }


  }

  // Start is called before the first frame update
  void Start()
  {
    boxCollider = gameObject.GetComponent<BoxCollider2D>();
    spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    SetRandomItem();
  }

  // Update is called once per frame
  void Update()
  {

    //Debug.Log(currentTimeInvisible);
    if (invisible)
    {
      spriteRenderer.enabled = false;
      boxCollider.enabled = false;
      currentTimeInvisible -= Time.deltaTime;
      if (currentTimeInvisible <= 0)
      {
        invisible = false;
        spriteRenderer.enabled = true;
        boxCollider.enabled = true;
        SetRandomItem();
      }
    }
  }

  void SetRandomItem()
  {
    itemID = (int)Random.Range(0f, 3f);
  }
}
