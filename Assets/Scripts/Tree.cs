using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col is PolygonCollider2D && col.gameObject.GetComponent<Knight>().isAttacking)
        {
            animator.SetBool("Shake", true);
            StartCoroutine(ExitShake());
        }
    }

    IEnumerator ExitShake()
    {
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("Shake", false);
    }

}
