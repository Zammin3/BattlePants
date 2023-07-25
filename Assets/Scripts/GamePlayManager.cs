using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }
    public void Spawn()
    {
        PhotonNetwork.Instantiate("Pants Game Player Variant", new Vector3(43.5f, 35.2f, 0), Quaternion.identity);
       // RespawnPanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
