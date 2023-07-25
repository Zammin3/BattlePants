using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PantsGamePlayer : MonoBehaviour
{
    public Door door; // Door 컴포넌트의 참조
    private bool isInTrigger = false; // 플레이어가 도어 트리거 내에 있는지 표시

    private void Start()
    {
        Camera cam = Camera.main;
        cam.transform.SetParent(transform);
        cam.transform.localPosition = new Vector3(0f, 0f, -10f);
        
    }

    private void Update()
    {
        if (gameObject.transform.position.y < -20f)
        {
            gameObject.transform.position = new Vector3(20f, -6f, 0f);
        }

        if (isInTrigger && Input.GetKeyDown(KeyCode.UpArrow))
        {
            door.OpenDoor(); // 도어를 엽니다
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == door.gameObject)
            isInTrigger = true; // isInTrigger를 true로 설정
    }

    // 플레이어가 도어 트리거에서 나가면
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == door.gameObject)
            isInTrigger = false; // isInTrigger를 false로 설정
    }

}
