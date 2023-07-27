using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    private void Awake()
    {
        Spawn();
    }
    public void Spawn()
    {
        PhotonNetwork.Instantiate("Pants Game Player Variant", new Vector3(20f, -6f, 0), Quaternion.identity);
        // RespawnPanel.SetActive(false);

    }

    public void ScoreUp()
    {
        PlayerData myStauts = NetworkManager.instance.GetMyStatus();
        if (!myStauts.score1)
        {
            NetworkManager.instance.SetPlayerScores(true, false);
        }
        else
        {
            NetworkManager.instance.SetPlayerScores(true, true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
