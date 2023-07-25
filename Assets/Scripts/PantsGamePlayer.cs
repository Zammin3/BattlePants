using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PantsGamePlayer : MonoBehaviour
{
    public Door door; // Door ������Ʈ�� ����
    private bool isInTrigger = false; // �÷��̾ ���� Ʈ���� ���� �ִ��� ǥ��
    private PhotonView photonView;

    private void Awake()
    {
        photonView =  GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            Camera cam = Camera.main;
            cam.transform.SetParent(transform);
            cam.transform.localPosition = new Vector3(0f, 0f, -10f);
        }
        
    }
    private void Start()
    {
       
        
    }

    private void Update()
    {
        if (gameObject.transform.position.y < -20f)
        {
            gameObject.transform.position = new Vector3(20f, -6f, 0f);
        }

        if (isInTrigger && Input.GetKeyDown(KeyCode.UpArrow))
        {
            door.OpenDoor(); // ��� ���ϴ�
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == door.gameObject)
            isInTrigger = true; // isInTrigger�� true�� ����
    }

    // �÷��̾ ���� Ʈ���ſ��� ������
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == door.gameObject)
            isInTrigger = false; // isInTrigger�� false�� ����
    }

}
