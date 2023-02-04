using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    public float bulletForce = 5f;
    public float bulletDespawnTime = 1f;
    public float damage = 1f;
    private float timer = 0;
    public float knockbackMag = 100;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 dir = player.transform.position - transform.position;
        rb.velocity = new Vector2(dir.x, dir.y).normalized * bulletForce;

        float theta = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, theta);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > bulletDespawnTime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            HealthManager playerHealthManager = other.GetComponent<HealthManager>();
            Vector3 playerPosition = other.gameObject.transform.position;
            Vector2 knockbackDir = (Vector2)(playerPosition - transform.position).normalized;
            playerHealthManager.TakeDamage(damage, knockbackDir * knockbackMag);
            Destroy(gameObject);
        }
    }

}
