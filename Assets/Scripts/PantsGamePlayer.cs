using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;

public class PantsGamePlayer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nicknameTextMeshPro;
    [SerializeField] Canvas canvas;

    private PhotonView photonView;
    private bool isInTrigger = false; 
    private bool isRealDoor = false;
    private Vector3 curDoor;
    private List<Vector3> doorPosition = new List<Vector3>();

    System.Random random = new System.Random();

    private void Awake()
    {
        photonView =  GetComponent<PhotonView>();
   
        nicknameTextMeshPro.text = photonView.IsMine ? PhotonNetwork.NickName : photonView.Owner.NickName;
        nicknameTextMeshPro.color = photonView.IsMine ? new Color(123 / 255f, 193 / 255f, 178 / 255f) : new Color(249/255f,108/255f,108/255f);
        if (photonView.IsMine)
        {
            Camera cam = Camera.main;
            cam.transform.SetParent(transform);
            cam.transform.localPosition = new Vector3(0f, 0f, -10f);

           
        }
        else {
            
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
        if (gameObject.transform.position.y < -35f)
        {
            gameObject.transform.position = new Vector3(20f, -6f, 0f);
        }

        if (isInTrigger && Input.GetKeyDown(KeyCode.UpArrow) && !isRealDoor)
        {
            //StartCoroutine(WaitAndSpawn());

            PlayerData myStatus = NetworkManager.instance.GetMyStatus();
            if (!myStatus.score1)
            {
                NetworkManager.instance.SetPlayerScores(true, false);
                NetworkManager.instance.SetRound((int)PhotonNetwork.CurrentRoom.CustomProperties["round"] + 1);
            }
            else
            {
                NetworkManager.instance.SetPlayerScores(true, true);
                NetworkManager.instance.SetRound((int)PhotonNetwork.CurrentRoom.CustomProperties["round"] + 1);
            }

            NetworkManager.instance.SetChangeScene();
            isRealDoor = false;
        }

        else if (isInTrigger && Input.GetKeyDown(KeyCode.UpArrow) && isRealDoor)
        {
            Debug.Log("OPEN THE DOOR");

            PlayerData myStatus = NetworkManager.instance.GetMyStatus();
            if (!myStatus.score1)
            {
                NetworkManager.instance.SetPlayerScores(true, false);
                NetworkManager.instance.SetRound((int)PhotonNetwork.CurrentRoom.CustomProperties["round"] + 1);
            }
            else
            {
                NetworkManager.instance.SetPlayerScores(true, true);
                NetworkManager.instance.SetRound((int)PhotonNetwork.CurrentRoom.CustomProperties["round"] + 1);
            }

            NetworkManager.instance.SetChangeScene();
            isRealDoor = false;
        }
        if (transform.localScale.x < 0)//    부모가 좌우 대칭되었다, 캔버스도 좌우 대칭 시키기 
        {
            canvas.transform.localScale = new Vector3(-Mathf.Abs(canvas.transform.localScale.x), canvas.transform.localScale.y, canvas.transform.localScale.z);
        }
        else {
            canvas.transform.localScale = new Vector3(Mathf.Abs(canvas.transform.localScale.x), canvas.transform.localScale.y, canvas.transform.localScale.z);
        }
   
    }

    IEnumerator WaitAndSpawn()
    {
        int index = random.Next(doorPosition.Count - 2);
        yield return new WaitForSeconds(0.5f);
        while (doorPosition[index] == curDoor) {
            index = random.Next(doorPosition.Count - 2);
        }
        gameObject.transform.position = doorPosition[index];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Door")
        {
            isInTrigger = true; 
            curDoor = other.gameObject.transform.position;
        }
        if(doorPosition.Count > 0 && other.gameObject.transform.position == doorPosition[doorPosition.Count - 1])
        {
            isRealDoor = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Door")
        {
            isInTrigger = false;
        }
    }

}
