using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    public float health;
    public float Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                animator.SetTrigger("death");
            }
            else
            {
                animator.SetTrigger("hit");

            }
        }
        get
        {
            return health;
        }
    }

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    public void TakeDamage(float damage, Vector2 knockbackForce)
    {
        rb.AddForce(knockbackForce, ForceMode2D.Impulse);
        Health -= damage;
    }

    public void Defeated()
    {
        PlayerController playerController = GetComponent<PlayerController>();
        if (playerController)
        {
            playerController.Death();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
