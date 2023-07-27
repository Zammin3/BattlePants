using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class springCollision : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetBool("up", true);
        StartCoroutine(ResetAnimatorParameter());
    }

    IEnumerator ResetAnimatorParameter()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool("up", false);
    }
}
