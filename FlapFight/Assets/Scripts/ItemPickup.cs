using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
  public int itemID = 0;

  public Sprite FireballSprite;
  public Sprite FreezeSprite;
  public Sprite GrenadeSprite;

  SpriteRenderer spriteRenderer;
  BoxCollider2D boxCollider;

  public float startTimeInvisible;
  float currentTimeInvisible;
  bool invisible;

  public void OnTriggerEnter2D(Collider2D collision)
  {
    PlayerController player = collision.GetComponent<PlayerController>();
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
    UpdateSprite();

  }

  // Update is called once per frame
  void Update()
  {
    if (invisible)
    {
      spriteRenderer.enabled = false;
      boxCollider.enabled = false;
      currentTimeInvisible -= Time.deltaTime;
      if (currentTimeInvisible <= 0)
      {
        SetRandomItem();
        UpdateSprite();
        invisible = false;
        spriteRenderer.enabled = true;
        boxCollider.enabled = true;
      }
    }
  }

  void SetRandomItem()
  {
    itemID = (int)Random.Range(1f, 4f);
  }

  void UpdateSprite()
  {
    if(itemID == 1)
    {
      spriteRenderer.sprite = FireballSprite;
    }
    else if (itemID == 2)
    {
      spriteRenderer.sprite = GrenadeSprite;
    }
    else if (itemID == 3)
    {
      spriteRenderer.sprite = FreezeSprite;
    }
  }
}
