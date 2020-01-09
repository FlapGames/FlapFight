using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : Projectile
{
  private void OnTriggerEnter2D(Collider2D other)
  {
    Destroy(gameObject);
    //other.GetComponent<PlayerController>().TakeDamage(0.1f, rigidbody2D.position.x, rigidbody2D.position.y);
    Destroy(Instantiate(ProjectileExplosion, transform.position, transform.rotation), 2.5f);
  }
}
