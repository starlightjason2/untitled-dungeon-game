using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    Collider2D weaponCollider;
    Vector2 rightAttackOffset;
    public float damage = 3;
    public float knockbackMag = 100;

    private void Start()
    {
        weaponCollider = GetComponent<Collider2D>();
        rightAttackOffset = transform.position;
    }


    public void AttackRight()
    {
        weaponCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }

    public void AttackLeft()
    {
        weaponCollider.enabled = true;
        transform.localPosition = new Vector2(rightAttackOffset.x * -1, rightAttackOffset.y);
    }

    public void StopAttack()
    {
        weaponCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            HealthManager enemy = other.GetComponent<HealthManager>();
            if (enemy != null)
            {
                Vector3 parentPosition = transform.parent.position;
                Vector3 weaponPosition = other.gameObject.transform.position;
                Vector2 knockbackDir = (Vector2)(weaponPosition - parentPosition).normalized;
                enemy.TakeDamage(damage, knockbackDir * knockbackMag);
            }
        }
    }
}
