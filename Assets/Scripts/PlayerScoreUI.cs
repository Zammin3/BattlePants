using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScoreUI : MonoBehaviour
{
    public TMP_Text round;

    public List<GameObject> playerInfoPanelList;
    public List<TMP_Text> playerStatusTextList;
    public List<GameObject> playerScoreList1;
    public List<GameObject> playerScoreList2;


    void Start()
    {
        foreach (GameObject panel in playerInfoPanelList)
        {
            panel.SetActive(false);
        }

        round.text = "1";
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
            playerScoreList1[i].SetActive(currentPlayersStatus[i].score1);
            playerScoreList2[i].SetActive(currentPlayersStatus[i].score2);

        }
        for (int i = currentPlayersStatus.Count; i < 4; i++)
        {
            playerInfoPanelList[i].SetActive(false);
        }
    }
}
