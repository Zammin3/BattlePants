using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class PantsGamePlayer : MonoBehaviour
{

    private PhotonView photonView;
    private bool isInTrigger = false; // �÷��̾ ���� Ʈ���� ���� �ִ��� ǥ��
    private bool isRealDoor = false;
    private List<Vector3> doorPosition = new List<Vector3>();

    System.Random random = new System.Random();

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


        GameObject doorsParent = GameObject.Find("Doors");
        
        foreach(Transform child in doorsParent.transform)
        {
            if (child != null)
            {
                Vector3 childPos = child.position;
                doorPosition.Add(childPos);
            }
        }

        
        int n = doorPosition.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            Vector3 temp = doorPosition[k];
            doorPosition[k] = doorPosition[n];
            doorPosition[n] = temp;
        }


    }

    private void Update()
    {
        if (gameObject.transform.position.y < -25f)
        {
            gameObject.transform.position = new Vector3(20f, -6f, 0f);
        }

        if (isInTrigger && Input.GetKeyDown(KeyCode.UpArrow) && !isRealDoor)
        {
            StartCoroutine(WaitAndSpawn());
        }

        if (isInTrigger && Input.GetKeyDown(KeyCode.UpArrow) && isRealDoor)
        {

        }
    }

    IEnumerator WaitAndSpawn()
    {
        yield return new WaitForSeconds(0.5f);
        int index = random.Next(doorPosition.Count - 1);
        gameObject.transform.position = doorPosition[index];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Door")
        {
            isInTrigger = true; // isInTrigger�� true�� ����
        }
        if(other.gameObject.transform.position == doorPosition[doorPosition.Count - 1])
        {
            isRealDoor = true;
        }
    }

    // �÷��̾ ���� Ʈ���ſ��� ������
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Door")
        {
            isInTrigger = false;
        }
    }

}
