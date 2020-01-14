using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : Projectile
{
  bool bounceUp = false;
  float timeSinceSwap = 0;

  float timeUntilExplosion;

  void Start()
  {
    rigidbody2D = GetComponent<Rigidbody2D>();

    projectileSpeed = 8f;

    timeUntilExplosion = 1.2f;
  }

  void Update()
  {
    if (bounceUp)
    {
      rigidbody2D.velocity = new Vector2(projectileSpeed * transform.localScale.x, Mathf.Pow((timeSinceSwap * 3 + 1), 2) * 1f);
    }
    else
    {
      rigidbody2D.velocity = new Vector2(projectileSpeed * transform.localScale.x, Mathf.Pow((timeSinceSwap * 3 + 1), 2) * -1.3f);
    }

    timeSinceSwap += Time.deltaTime;

    if (timeSinceSwap >= 0.4f && bounceUp == true)
    {
      bounceUp = !bounceUp;
      timeSinceSwap = 0;
    }

    timeUntilExplosion -= Time.deltaTime;

    if (timeUntilExplosion <= 0f)
    {
      SpawnExplosion();
    }
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    bounceUp = !bounceUp;
    timeSinceSwap = 0f;
  }
  private new void OnTriggerEnter2D(Collider2D other)
  {
    other.GetComponent<PlayerController>().TakeDamage(0.3f, rigidbody2D.position.x, rigidbody2D.position.y);
    SpawnExplosion();
  }

  private void SpawnExplosion()
  {
    Destroy(gameObject);
    Destroy(Instantiate(ProjectileExplosion, transform.position, transform.rotation), 0.3f);
  }
}