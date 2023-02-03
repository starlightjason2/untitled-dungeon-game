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
        Debug.Log("attackright");
    }

    public void AttackLeft()
    {
        weaponCollider.enabled = true;
        transform.localPosition = new Vector2(rightAttackOffset.x * -1, rightAttackOffset.y);
        Debug.Log("attackleft");
    }

    public void StopAttack()
    {
        weaponCollider.enabled = false;
        Debug.Log("stop attack");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                Vector3 parentPosition = transform.parent.position;
                Vector3 weaponPosition = other.gameObject.transform.position;
                Vector2 knockbackDir = (Vector2)(weaponPosition - parentPosition).normalized;
                Debug.Log(parentPosition);
                Debug.Log(weaponPosition);
                enemy.TakeDamage(damage, knockbackDir * knockbackMag);
            }
        }
    }
}
