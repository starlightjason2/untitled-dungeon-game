using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 1;
    public float knockbackMag = 500;
    public float moveSpeed = 200f;
    public AggroZone aggroZone;
    SpriteRenderer spriteRenderer;
    public Rigidbody2D rb;
    Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }


    void FixedUpdate()
    {

        if (aggroZone.detectedObjs.Count > 0)
        {
            // move towards obj
            Collider2D detectedObj = aggroZone.detectedObjs[0];
            PlayerController playerController = detectedObj.GetComponent<PlayerController>();
            if (playerController.targetable)
            {
                Vector2 dir = (detectedObj.transform.position - transform.position).normalized;
                rb.AddForce(dir * moveSpeed * Time.fixedDeltaTime);

                // set sprite direction
                // set direction of sprite to movement dir
                if (dir.x < 0)
                {
                    spriteRenderer.flipX = false;
                }
                else if (dir.x > 0)
                {
                    spriteRenderer.flipX = true;
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Collider2D collider = other.collider;
        if (collider.tag == "Player")
        {
            HealthManager playerHealthManager = collider.GetComponent<HealthManager>();
            Vector3 playerPosition = collider.gameObject.transform.position;
            Vector2 knockbackDir = (Vector2)(playerPosition - transform.position).normalized;
            playerHealthManager.TakeDamage(damage, knockbackDir * knockbackMag);
        }
    }
}
