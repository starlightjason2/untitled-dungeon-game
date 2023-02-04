using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float idleFriction = 0.9f;
    public float maxSpeed = 5f;
    public bool targetable = true;
    public MeleeAttack meleeAttack;


    Vector2 movementInput;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            if (movementInput != Vector2.zero)
            {
                bool movedSuccessfully = TryMove(movementInput);

                if (!movedSuccessfully)
                {
                    movedSuccessfully = TryMove(new Vector2(movementInput.x, 0));

                    if (!movedSuccessfully)
                    {
                        movedSuccessfully = TryMove(new Vector2(0, movementInput.y));
                    }
                }

                animator.SetBool("isMoving", movedSuccessfully);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }

            // set direction of sprite to movement dir
            if (movementInput.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (movementInput.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    private bool TryMove(Vector2 dir)
    {
        if (dir == Vector2.zero) return false;
        rb.AddForce(dir * moveSpeed * Time.fixedDeltaTime);

        if (rb.velocity.magnitude > maxSpeed)
        {
            float limitedSpeed = Mathf.Lerp(rb.velocity.magnitude, maxSpeed, idleFriction);
            rb.velocity = rb.velocity.normalized * limitedSpeed;
        }
        return true;
    }

    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    void OnFire()
    {
        animator.SetTrigger("meleeAttack");
    }

    public void MeleeAttack()
    {
        LockMovement();
        if (spriteRenderer.flipX == true)
        {
            meleeAttack.AttackLeft();
        }
        else
        {
            meleeAttack.AttackRight();
        }
    }

    public void EndMeleeAttack()
    {
        UnlockMovement();
        meleeAttack.StopAttack();
    }

    public void LockMovement()
    {
        canMove = false;
    }
    public void UnlockMovement()
    {
        canMove = true;
    }

    public void Death()
    {
        canMove = false;
        targetable = false;
    }
}
