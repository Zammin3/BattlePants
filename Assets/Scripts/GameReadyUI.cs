using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameReadyManager : MonoBehaviour
{

    public List<TMP_Text> playerStatusTextList;
    // Start is called before the first frame update

    public List<GameObject> playerInfoPanelList;

    public List<GameObject> playerReadyPanelList;

   

    void Start()
    {
        foreach (GameObject panel in playerInfoPanelList)
        {
            panel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        List<PlayerData> currentPlayersStatus = NetworkManager.instance.GetPlayersStatus();
        

        // Loop through each status and update the corresponding UI text element
        for (int i = 0; i < currentPlayersStatus.Count; i++)
        {
           
            playerStatusTextList[i].text = currentPlayersStatus[i].Nickname;
            playerInfoPanelList[i].SetActive(true);
            playerReadyPanelList[i].SetActive(currentPlayersStatus[i].IsReady);

            
        }
        for (int i = currentPlayersStatus.Count; i < 4; i++) {
          playerInfoPanelList[i].SetActive(false);
        }
    }
    public void ReadyButtonClicked(bool isReady) {
        PlayerData myStauts = NetworkManager.instance.GetMyStatus();
        NetworkManager.instance.SetPlayerReady(!myStauts.IsReady);
    }
}
