using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : Projectile
{

  bool bounceUp = false;
  float timeSinceSwap = 0;

  void Start()
  {
    rigidbody2D = GetComponent<Rigidbody2D>();
    projectileSpeed = 8f;
  }

  void Update()
  {
    if(bounceUp)
    {
      rigidbody2D.velocity = new Vector2(projectileSpeed * transform.localScale.x, 2* timeSinceSwap * 9.81f);
    }
    else
    {
      rigidbody2D.velocity = new Vector2(projectileSpeed * transform.localScale.x,3 * timeSinceSwap * -9.81f);
    }



    timeSinceSwap += Time.deltaTime;
    if(timeSinceSwap >= 0.4f && bounceUp == true)
    {
      bounceUp = !bounceUp;
      timeSinceSwap = 0;
    }
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    bounceUp = !bounceUp;
    timeSinceSwap = 0f;
  }
  private void OnTriggerEnter2D(Collider2D other)
  {
    if(other.transform.CompareTag("Player1") || other.transform.CompareTag("Player2"))
    {
      Destroy(gameObject);
      //other.GetComponent<PlayerController>().TakeDamage(0.2f, rigidbody2D.position.x, rigidbody2D.position.y);
      Destroy(Instantiate(ProjectileExplosion, transform.position, transform.rotation), 2.5f);
    }

  }
}
