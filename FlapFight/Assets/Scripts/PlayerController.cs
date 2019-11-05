using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode attack;

    private Rigidbody2D rigidbody2D;

    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask ground;

    public bool isGrounded;

    private Animator animator;

    public GameObject Projectile;
    public Transform throwPoint;


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

        if((Input.GetKey(left) && Input.GetKey(right)) || (!Input.GetKey(left) && !Input.GetKey(right))){
            rigidbody2D.velocity = new Vector2(0, rigidbody2D.velocity.y);
        }else if (Input.GetKey(left))
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

        }

        if (rigidbody2D.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(rigidbody2D.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (Input.GetKeyDown(attack))
        {
            GameObject projectileClone = (GameObject)Instantiate(Projectile, throwPoint.position, throwPoint.rotation);
            //Destroy(projectileClone, 0.5f);
            projectileClone.transform.localScale = transform.localScale;
            animator.SetTrigger("AttackRanged");
        }


        animator.SetFloat("Speed", Mathf.Abs(rigidbody2D.velocity.x));
        animator.SetBool("Grounded", isGrounded);

    }
}
