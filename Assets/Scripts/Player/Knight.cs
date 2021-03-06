﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public float movementSpeed = 3f;
    private Vector3 jump;
    private Rigidbody2D rb;
    private bool inAir = false;
    private float attackCooldown = 0.5f;
    public bool isAttacking = false;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        jump = new Vector2(0.0f, 500f);
    }

    // Update is called once per frame
    void Update()
    {
        SetInAir();
        Move();
        Jump();
        Attack();
    }

    void SetInAir()
    {
        bool newInAir = rb.velocity.y != 0;
        if (!newInAir && inAir)
        {
            StartCoroutine(ExitJump());
        }
        inAir = newInAir;
    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        CheckDirectionIsCorrect(horizontalInput);
        transform.position = transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, 0, 0);
    }

    void CheckDirectionIsCorrect(float horizontalInput)
    {
        if (horizontalInput < 0 && transform.eulerAngles.y == 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (horizontalInput > 0 && transform.eulerAngles.y == 180)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }


    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !inAir)
        {
            animator.SetBool("Jump", true);
            rb.AddForce(jump);
            inAir = true;
        }
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            animator.SetBool("Attack", true);
            isAttacking = true;
            StartCoroutine(ExitAttack());
        }
    }

    IEnumerator ExitAttack()
    {
        yield return new WaitForSeconds(0.36f);
        animator.SetBool("Attack", false);
        yield return new WaitForSeconds(attackCooldown - 0.36f);
        isAttacking = false;
    }

    IEnumerator ExitJump()
    {
        yield return new WaitForSeconds(0.1f);
        if (rb.velocity.y == 0)
        {
            animator.SetBool("Jump", false);
        }
    }
}
