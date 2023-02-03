using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float damage = 1;
    public float knockbackMag = 500;

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
