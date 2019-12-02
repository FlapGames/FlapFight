using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float knockbackMultiplier;
    private float knockedTime;
    public float startKnockedTime;
    private Vector2 knockbackVector;

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

    private Rigidbody2D rigidbody2D;

    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask ground;

    public bool isGrounded;

    private Animator animator;

    public GameObject Projectile;
    public Transform throwPoint;

    public Transform meleeAttackPosition;
    public float meleeAttackRange;
    public float meleeDamage;

    public LayerMask enemies;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //Death

        if(rigidbody2D.position.y < -6)
        {
            Destroy(this.gameObject);
        }
        
        //Check for Ground, needed for jumping

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, ground);


        //Movement

            //Directional

        if ((Input.GetKey(left) && Input.GetKey(right)) || (!Input.GetKey(left) && !Input.GetKey(right))){   //if both left and right are pressed, no movement on the x-axis
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

        if (Input.GetKeyDown(jump) && isGrounded)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
            SoundManagerScript.PlaySound("jump_salamisounds");
            animator.SetTrigger("JumpInitiate");

        }



        //Knockback

        if (knockedTime > 0)
        {
            rigidbody2D.velocity = knockbackVector;
            knockedTime -= Time.deltaTime;
        }

        //Attacks

            //AttackRanged

        if(timeBetweenRangedAttack <= 0 && isPlayerShooting)
        {
            
            isPlayerShooting = false;
            GameObject projectileClone = (GameObject)Instantiate(Projectile, throwPoint.position, throwPoint.rotation);
            //Destroy(projectileClone, 0.5f);
            projectileClone.transform.localScale = transform.localScale;

        } 
        else if(timeBetweenRangedAttack <= 0 && Input.GetKeyDown(attackRanged))
        {
            animator.SetTrigger("AttackRanged");
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
                timeBetweenMeleeAttack = startTimeBetweenMeleeAttack;
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(meleeAttackPosition.position, meleeAttackRange, enemies);
                for(int i = 0; i< enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<PlayerController>().TakeDamage(meleeDamage, rigidbody2D.position.x, rigidbody2D.position.y);
                }
                animator.SetTrigger("AttackMeleePunchOne");
            }
        }
        else
        {
            timeBetweenMeleeAttack -= Time.deltaTime;
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

    }

    public void TakeDamage(float damage, float enemyPositionX, float enemyPositionY)
    {
        knockbackMultiplier += damage;
        knockedTime = startKnockedTime;

        //Debug.Log(rigidbody2D.position.x - enemyPositionX);
        //Debug.Log(rigidbody2D.position.y - enemyPositionY);

        knockbackVector = new Vector2((rigidbody2D.position.x - enemyPositionX)* knockbackMultiplier, (rigidbody2D.position.y - enemyPositionY) * knockbackMultiplier * 0.5f);

    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackRange);
    }
}
