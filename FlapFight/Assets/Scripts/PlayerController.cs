using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
  public float knockbackMultiplier;
  private float knockedTime;
  public float startKnockedTime;
  private Vector2 knockbackVector;

  private float debuffTime;

  public float shieldDurability = 100;

  public float amountOfJumpsLeft;

  public float moveSpeed;
  public float jumpForce;

  private float timeBetweenMeleeAttack;
  public float startTimeBetweenMeleeAttack;

  private float timeBetweenRangedAttack;
  public float startTimeBetweenRangedAttack;
  private bool isPlayerShooting;

  public KeyCode left;
  public KeyCode right;
  public KeyCode jump;
  public KeyCode attackRanged;
  public KeyCode attackMelee;
  public KeyCode block;
  public KeyCode useItem;

  private Rigidbody2D rigidbody2D;

  public Transform groundCheckPoint;
  public float groundCheckRadius;
  public LayerMask ground;

  public Transform wallCheckPoint;
  public float wallCheckRadius;

  public bool isGrounded;
  public bool isTouchingWall;
  public bool isBlocking;

  private Animator animator;

  public GameObject Projectile;
  public Transform throwPoint;

  public Transform meleeAttackPosition;

  public float meleeAttackRange;
  public float meleeDamage;

  public GameObject Fireball;
  public GameObject Grenade;
  public GameObject Hourglass;

  public int currentItemID = 0;

  public LayerMask enemies;

  public static bool GameIsOver;

  public bool PlayerIsDeath;

  public Image currentHealthBar;
  public Image currentItem;

  public Sprite FireballSprite;
  public Sprite FreezeSprite;
  public Sprite GrenadeSprite;

  public float numberOfLives;
  //float damage = 0.1f;

  // Start is called before the first frame update
  void Start()
  {
    rigidbody2D = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();
    GameIsOver = false;
    Time.timeScale = 1f;
    numberOfLives = 1f;
    PlayerIsDeath = false;
  }

  // Update is called once per frame
  void Update()
  {
    //Death
    if (rigidbody2D.position.y < -6 || numberOfLives == 0)
    {
      PlayerIsDeath = true;
      GameIsOver = true;
      this.gameObject.SetActive(false);
    }

    //Check for Ground, needed for jumping

    isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, ground);

    isTouchingWall = Physics2D.OverlapCircle(wallCheckPoint.position, wallCheckRadius, ground);

    //Movement

    //Directional

    if ((Input.GetKey(left) && Input.GetKey(right)) || (!Input.GetKey(left) && !Input.GetKey(right)))
    {   //if both left and right are pressed, no movement on the x-axis
      rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
    }
    else if (Input.GetKey(left))
    {
      rigidbody2D.velocity = new Vector2(-moveSpeed, rigidbody2D.velocity.y);
    }
    else if (Input.GetKey(right))
    {
      rigidbody2D.velocity = new Vector2(moveSpeed, rigidbody2D.velocity.y);
    }
    else
    {
      rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
    }

    //Jumping

    if (Input.GetKeyDown(jump) && amountOfJumpsLeft > 0 && !isPlayerShooting)
    {
      amountOfJumpsLeft--;
      rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
      SoundManagerScript.PlaySound("Jumpsound");
      animator.SetTrigger("JumpInitiate");

    }

    if (isGrounded || isTouchingWall)
    {
      amountOfJumpsLeft = 1;
    }



    //Knockback

    if (knockedTime > 0)
    {
      rigidbody2D.velocity = knockbackVector;
      knockedTime -= Time.deltaTime;
    }

    //Attacks

    //AttackRanged

    if (timeBetweenRangedAttack <= 0 && isPlayerShooting)
    {

      isPlayerShooting = false;
      GameObject projectileClone = (GameObject)Instantiate(Projectile, throwPoint.position, throwPoint.rotation);
      //Destroy(projectileClone, 0.5f);
      projectileClone.transform.localScale = transform.localScale;

    }
    else if (timeBetweenRangedAttack <= 0 && Input.GetKeyDown(attackRanged))
    {
      if (!isGrounded)
      {
        animator.SetTrigger("AttackRangedJump");
      }
      else
      {
        animator.SetTrigger("AttackRanged");
      }

      isPlayerShooting = true;

      timeBetweenRangedAttack = startTimeBetweenRangedAttack;

    }
    else
    {
      timeBetweenRangedAttack -= Time.deltaTime;
    }


    //AttackMelee

    if (timeBetweenMeleeAttack <= 0)
    {
      if (Input.GetKeyDown(attackMelee))
      {
        animator.SetTrigger("AttackMeleePunchOne");
        timeBetweenMeleeAttack = startTimeBetweenMeleeAttack;
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(meleeAttackPosition.position, meleeAttackRange, enemies);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
          enemiesToDamage[i].GetComponent<PlayerController>().TakeDamage(meleeDamage, rigidbody2D.position.x, rigidbody2D.position.y);
        }
        SoundManagerScript.PlaySound("Melee");
      }
    }
    else
    {
      timeBetweenMeleeAttack -= Time.deltaTime;
    }

    //Attack Special

    if(currentItemID != 0)
    {
      

      if(Input.GetKeyDown(useItem))
      {
        if (currentItemID == 1)
        {
          GameObject projectileClone = (GameObject)Instantiate(Fireball, throwPoint.position, throwPoint.rotation);
          projectileClone.transform.localScale = transform.localScale;
        }
        else if (currentItemID == 2)
        {
          GameObject projectileClone = (GameObject)Instantiate(Grenade, throwPoint.position, throwPoint.rotation);
          projectileClone.transform.localScale = transform.localScale;
        }
        else
        {
          if (this.gameObject.transform.CompareTag("Player1"))
          {
            GameObject.FindWithTag("Player2").GetComponent<PlayerController>().ApplySlowness();
          }
          else
          {
            GameObject.FindWithTag("Player1").GetComponent<PlayerController>().ApplySlowness();
          }
        }
        currentItemID = 0;
      }
    }
    UpdateItemUI();

    //Blocking    

    if (Input.GetKey(block) && shieldDurability > 15)
    {
      isBlocking = true;
      if (shieldDurability > 0)
      {
        shieldDurability -= 0.16f;
      }

    }
    else
    {
      isBlocking = false;

      if (shieldDurability < 100)
      {
        shieldDurability += 0.08f;
      }
    }


    //Sprite flipping

    if (rigidbody2D.velocity.x < 0)
    {
      transform.localScale = new Vector3(-1, 1, 1);
    }
    else if (rigidbody2D.velocity.x > 0)
    {
      transform.localScale = new Vector3(1, 1, 1);
    }

    //Animator

    animator.SetFloat("Speed", Mathf.Abs(rigidbody2D.velocity.x));
    animator.SetBool("Grounded", isGrounded);
    animator.SetBool("Shooting", isPlayerShooting);

    if (!isGrounded && rigidbody2D.velocity.y < 0 && !isPlayerShooting)
    {
      animator.SetTrigger("Falling");
    }
    else if (!isGrounded && !isPlayerShooting)
    {
      animator.SetTrigger("Flying");
    }

    if (knockedTime > 0)
    {
      animator.SetTrigger("Knockbacked");

      if (rigidbody2D.velocity.x < 0)
      {
        transform.localScale = new Vector3(1, 1, 1);
      }
      else if (rigidbody2D.velocity.x > 0)
      {
        transform.localScale = new Vector3(-1, 1, 1);
      }
    }

    if (isBlocking && shieldDurability > 16)
    {
      animator.SetTrigger("Blocking");
    }


    //Debuffs

    if (debuffTime > 0)
    {
      debuffTime -= Time.deltaTime;
      moveSpeed = 5;
    }
    else
    {
      moveSpeed = 10;
    }
    
  }

  public void TakeDamage(float damage, float enemyPositionX, float enemyPositionY)
  {
    if (isBlocking)
    {
      shieldDurability -= 20;
      return;
    }
    knockbackMultiplier += damage;
    knockedTime = startKnockedTime;

    updateHealthbar();

    knockbackVector = new Vector2((rigidbody2D.position.x - enemyPositionX) * knockbackMultiplier, (rigidbody2D.position.y - enemyPositionY) * knockbackMultiplier * 0.5f);
  }

  public void PickUpItem(int itemID)
  {
    currentItemID = itemID;
  }

  public void ApplySlowness()
  {
    this.debuffTime = 5f;
  }

  public void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackRange);
  }

  
  public void updateHealthbar()
  {
    if (numberOfLives - knockbackMultiplier <= 0)
    {
      numberOfLives = 0;
      PlayerIsDeath = true;
      GameIsOver = true;
    }

    currentHealthBar.rectTransform.localScale = new Vector3(numberOfLives - knockbackMultiplier, 1, 1);
  }

  void UpdateItemUI()
  {
    if (currentItemID == 1)
    {
      currentItem.color = Color.white;
      currentItem.sprite = FireballSprite;
    }
    else if (currentItemID == 2)
    {
      currentItem.color = Color.white;
      currentItem.sprite = GrenadeSprite;
    }
    else if (currentItemID == 3)
    {
      currentItem.color = Color.white;
      currentItem.sprite = FreezeSprite;
    }
    else if (currentItemID == 0)
    {
      currentItem.color = Color.clear;
    }
  }
}