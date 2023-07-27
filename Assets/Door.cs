using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private bool isInTrigger = false;

    private void Update()
    {
        if (gameObject.transform.position.y < -20f)
        {
            gameObject.transform.position = new Vector3(20f, -6f, 0f);
        }

        if (isInTrigger && Input.GetKeyDown(KeyCode.UpArrow))
        {
            OpenDoor(); // 도어를 엽니다
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isInTrigger = true; 
    }

    // 플레이어가 도어 트리거에서 나가면
    private void OnTriggerExit2D(Collider2D other)
    {
        isInTrigger = false;
    }

    public void OpenDoor()
    {
        animator.SetBool("open", true);
        StartCoroutine(ResetAnimatorParameter());
    }

    IEnumerator ResetAnimatorParameter()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool("open", false);
    }

}
