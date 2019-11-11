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

    private float timeBetweenAttack;
    public float startTimeBetweenAttack;

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
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, ground);

        
        if((Input.GetKey(left) && Input.GetKey(right)) || (!Input.GetKey(left) && !Input.GetKey(right))){   //if both left and right are pressed, no movement on the x-axis
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

        if (Input.GetKeyDown(jump) && isGrounded)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpForce);
            SoundManagerScript.PlaySound("jump_salamisounds");
        }

        if (rigidbody2D.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(rigidbody2D.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetKeyDown(attackRanged))
        {
            GameObject projectileClone = (GameObject)Instantiate(Projectile, throwPoint.position, throwPoint.rotation);
            //Destroy(projectileClone, 0.5f);
            projectileClone.transform.localScale = transform.localScale;
            animator.SetTrigger("AttackRanged");
        }

        if (Input.GetKeyDown(attackMelee))
        {
            if (timeBetweenAttack <= 0)
            {
                timeBetweenAttack = startTimeBetweenAttack;
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(meleeAttackPosition.position, meleeAttackRange, enemies);
                for(int i = 0; i< enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<PlayerController>().TakeDamage(meleeDamage, rigidbody2D.position.x, rigidbody2D.position.y);
                }
            }
            else
            {
                timeBetweenAttack -= Time.deltaTime;
            }

        }

        if(knockedTime > 0)
        {
            rigidbody2D.velocity = knockbackVector;
            knockedTime -= Time.deltaTime;
        }


        animator.SetFloat("Speed", Mathf.Abs(rigidbody2D.velocity.x));
        animator.SetBool("Grounded", isGrounded);

    }

    public void TakeDamage(float damage, float enemyPositionX, float enemyPositionY)
    {
        knockbackMultiplier += damage;
        knockedTime = startKnockedTime;

        Debug.Log(rigidbody2D.position.x - enemyPositionX);
        Debug.Log(rigidbody2D.position.y - enemyPositionY);

        knockbackVector = new Vector2((rigidbody2D.position.x - enemyPositionX)* knockbackMultiplier, (rigidbody2D.position.y - enemyPositionY) * knockbackMultiplier * 0.5f);

    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackRange);
    }
}
