using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;

    private Rigidbody2D rigidbody2D;

    public GameObject ProjectileExplosion;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.velocity = new Vector2(projectileSpeed * transform.localScale.x, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(Instantiate(ProjectileExplosion, transform.position, transform.rotation),2.5f);

        Destroy(gameObject);
    }
}
