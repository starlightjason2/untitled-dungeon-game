using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    Rigidbody2D rb;
    public float health;
    public float Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                Defeated();
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
    }

    public void TakeDamage(float damage, Vector2 knockbackForce)
    {
        rb.AddForce(knockbackForce, ForceMode2D.Impulse);
        Health -= damage;
    }

    public void Defeated()
    {
        Destroy(gameObject);
    }
}
