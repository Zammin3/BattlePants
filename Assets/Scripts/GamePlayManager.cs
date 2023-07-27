using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public GameObject endingUI;
    public GameObject winUI;
    public GameObject loseUI;
    private List<Vector3> playerPos1 = new List<Vector3>() { 
        new Vector3(6f, -6f, 0),
        new Vector3(8f, -6f, 0),
        new Vector3(10f, -6f, 0),
        new Vector3(12f, -6f, 0)
    };
    private List<Vector3> playerPos2 = new List<Vector3>() {
        new Vector3(18f, -6f, 0),
        new Vector3(20f, -6f, 0),
        new Vector3(31f, -6f, 0),
        new Vector3(33f, -6f, 0)
    };

    private void Awake()
    {

    }
    

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Spawn", 0.2f);
        endingUI.SetActive(false);
        winUI.SetActive(false);
        loseUI.SetActive(false);
    }
    public void Spawn()
    {
        PlayerData myStatus = NetworkManager.instance.GetMyStatus();

        if (NetworkManager.instance.getCurrentMap() == "Game Play Map1")
        {
            PhotonNetwork.Instantiate("Pants Game Player Variant", playerPos1[myStatus.PlayerID - 1], Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate("Pants Game Player Variant", playerPos2[myStatus.PlayerID - 1], Quaternion.identity);
        }

    }


    // Update is called once per frame
    void Update()
    {
        List<PlayerData> currentPlayersStatus = NetworkManager.instance.GetPlayersStatus();
        PlayerData myStauts = NetworkManager.instance.GetMyStatus();

        for (int i = 0; i < currentPlayersStatus.Count; i++)
        {
            if (currentPlayersStatus[i].score2 && myStauts.PlayerID == currentPlayersStatus[i].PlayerID)
            {
                endingUI.SetActive(true);
                winUI.SetActive(true);
            }
            else if (currentPlayersStatus[i].score2 && myStauts.PlayerID != currentPlayersStatus[i].PlayerID)
            {
                endingUI.SetActive(true);
                loseUI.SetActive(true);
            }

        }
    }
}
