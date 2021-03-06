﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;

    protected Rigidbody2D rigidbody2D;

    public GameObject ProjectileExplosion;

    // Start is called before the first frame update
    protected void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

  // Update is called once per frame
    protected void Update()
    {
        rigidbody2D.velocity = new Vector2(projectileSpeed * transform.localScale.x, 0);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {

        Destroy(gameObject);
        other.GetComponent<PlayerController>().TakeDamage(0.1f, rigidbody2D.position.x, rigidbody2D.position.y);
        Destroy(Instantiate(ProjectileExplosion, transform.position, transform.rotation),2.5f);
    }
}
