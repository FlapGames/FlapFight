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

  //[SerializeField] Image ItemPlayer1;
  //[SerializeField] Image ItemPlayer2;

  //public GameObject player1;
  //public GameObject player2;

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
    UpdateSprite();

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

  //void UpdateItemUI()
  //{
  //  if(player1.GetComponent<PlayerController>().currentItemID == 1)
  //  {
  //    ItemPlayer1.color = Color.white;
  //    ItemPlayer1.sprite = FireballSprite;
  //  }
  //  else if(player1.GetComponent<PlayerController>().currentItemID == 2)
  //  {
  //    ItemPlayer1.color = Color.white;
  //    ItemPlayer1.sprite = GrenadeSprite;
  //  }
  //  else if(player1.GetComponent<PlayerController>().currentItemID == 3)
  //  {
  //    ItemPlayer1.color = Color.white;
  //    ItemPlayer1.sprite = FreezeSprite;
  //  }
  //  else if(player1.GetComponent<PlayerController>().currentItemID == 0)
  //  {
  //    ItemPlayer1.color = Color.clear;
  //  }

  //  if (player2.GetComponent<PlayerController>().currentItemID == 1)
  //  {
  //    ItemPlayer2.color = Color.white;
  //    ItemPlayer2.sprite = FireballSprite;
  //  }
  //  else if (player2.GetComponent<PlayerController>().currentItemID == 2)
  //  {
  //    ItemPlayer2.color = Color.white;
  //    ItemPlayer2.sprite = GrenadeSprite;
  //  }
  //  else if (player2.GetComponent<PlayerController>().currentItemID == 3)
  //  {
  //    ItemPlayer2.color = Color.white;
  //    ItemPlayer2.sprite = FreezeSprite;
  //  }
  //  else if(player2.GetComponent<PlayerController>().currentItemID == 0)
  //  {
  //    ItemPlayer2.color = Color.clear;
  //  }

  //}
}
