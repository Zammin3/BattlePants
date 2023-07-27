using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    public GameObject endingUI;
    public GameObject winUI;
    public GameObject loseUI;

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
        PhotonNetwork.Instantiate("Pants Game Player Variant", new Vector3(20f, -6f, 0), Quaternion.identity);
        // RespawnPanel.SetActive(false);

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
