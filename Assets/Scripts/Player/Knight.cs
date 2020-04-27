using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    public float movementSpeed = 3f;
    private Vector3 jump;
    private Rigidbody2D rb;
    private bool isGrounded = true;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        jump = new Vector2(0.0f, 500f);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));

        if (horizontalInput < 0 && transform.eulerAngles.y == 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (horizontalInput > 0 && transform.eulerAngles.y == 180)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            animator.SetBool("Jump", true);
            rb.AddForce(jump);
            isGrounded = false;
        }

        transform.position = transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, 0, 0);
    }

    void OnCollisionEnter2D()
    {
        StartCoroutine(ExitJump());
    }

    void OnCollisionExit2D()
    {
        isGrounded = false;
    }

    IEnumerator ExitJump()
    {
        isGrounded = true;
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("Jump", false);
    }
}
