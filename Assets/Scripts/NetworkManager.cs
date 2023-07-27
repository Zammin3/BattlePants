using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    

    public static NetworkManager instance;

    public string currentMap = "Game Play Map2";

    // Photon View 컴포넌트
    public PhotonView PV;

    // 모든 플레이어가 준비했는지 확인하기 위한 변수
    private int playersReady = 0;

    // 방에 들어올 수 있는 최대 플레이어 수
    private const int MaxPlayers = 4;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        //Photon View 컴포넌트 레퍼런스 얻기
       //PV = GetComponent<PhotonView>();
    }

    void Start()
    {

    }

    public void Connect(string nickname)
    {
        // 마스터 서버에 연결
        PhotonNetwork.NickName=nickname;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("마스터 서버에 연결됨 ");
        JoinRoom();

    }

    void JoinRoom()
    {
        // 방 이름을 설정하고 방에 참가 요청
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.MaxPlayers = (byte)MaxPlayers; // 최대 플레이어 수 설정
        PhotonNetwork.JoinOrCreateRoom("게임방", roomOptions, TypedLobby.Default);
    }


    public override void OnJoinedRoom()
    {
        Debug.Log("방에 참가함");
        // 다른 플레이어에게 알림
        PV.RPC("NotifyUserJoined", RpcTarget.OthersBuffered, PhotonNetwork.LocalPlayer.NickName);
    }

    [PunRPC]
    void NotifyUserJoined(string name)
    {
        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            Debug.Log("Player ID: " + playerInfo.Key + ", NickName: " + playerInfo.Value.NickName);
        }

        Debug.Log("Player ID: " + PhotonNetwork.LocalPlayer.ActorNumber);
        Debug.Log(name + "님이 게임에 참가하였습니다.");

        
    }


    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(otherPlayer.NickName + "님이 게임에서 나갔습니다.");
        playersReady--;
    }

    // 플레이어가 준비되었음을 서버에 알리는 함수


    public void SetPlayerReady(bool isReady)
    {
        ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
        properties.Add("isReady", isReady);
        PhotonNetwork.LocalPlayer.SetCustomProperties(properties);
    }

    public void SetPlayerScores(bool score1, bool score2)
    {
        ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
        properties.Add("score1", score1);
        properties.Add("score2", score2);
        PhotonNetwork.LocalPlayer.SetCustomProperties(properties);
    }


    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (changedProps.ContainsKey("isReady"))
        {
            CheckAllPlayersReady();
        }
    }
    private void CheckAllPlayersReady()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            object isReady;
            if (player.CustomProperties.TryGetValue("isReady", out isReady))
            {
                if ((bool)isReady == false)
                {
                    return; // 한 명 이상의 플레이어가 아직 준비되지 않았으므로 리턴
                }
            }
            else
            {
                return; // 한 명 이상의 플레이어가 아직 준비 상태를 설정하지 않았으므로 리턴
            }
        }
        // 모든 플레이어가 준비되었으므로 게임을 시작
        PlayerReady();
    }
    public void PlayerReady()
    {
        Debug.Log("게임 시작!");
        PV.RPC("NotifyPlayerReady", RpcTarget.AllBuffered);
    }

    [PunRPC]
    void NotifyPlayerReady()
    {
        playersReady++;
        if (playersReady == PhotonNetwork.CurrentRoom.PlayerCount)
        {
            // 모든 플레이어가 준비되었다면 게임을 시작
            StartGame();
        }
    }
    void StartGame()
    {
        // 게임 시작 로직 (씬 전환 등)
        Debug.Log("모든 플레이어가 준비되었습니다. 게임을 시작합니다.");

        foreach (Photon.Realtime.Player player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            Debug.Log("Player ID: " + player.ActorNumber + ", NickName: " + player.NickName);

            foreach (DictionaryEntry entry in player.CustomProperties)
            {
                Debug.Log("Player ID: " + player.ActorNumber + ", Key: " + entry.Key.ToString() + ", Value: " + entry.Value.ToString());
            }
        }
        SceneManager.LoadScene("Game Play Map2");
    }


    public List<PlayerData> GetPlayersStatus()
    {
        List<PlayerData> playersStatus = new List<PlayerData>();

        // Check if CurrentRoom is not null
        if (PhotonNetwork.CurrentRoom != null)
        {
            foreach (Photon.Realtime.Player player in PhotonNetwork.CurrentRoom.Players.Values)
            {
                // Check if player is not null
                if (player != null)
                {
                    PlayerData playerData = new PlayerData();
                    playerData.Nickname = player.NickName;
                    playerData.PlayerID = player.ActorNumber;

                    // Check if CustomProperties is not null
                    if (player.CustomProperties != null && player.CustomProperties.ContainsKey("isReady"))
                    {
                        playerData.IsReady = (bool)player.CustomProperties["isReady"];
                    }
                    else
                    {
                        playerData.IsReady = false;
                    }
                    // 플레이어의 "score1"과 "score2" 상태를 확인하고 설정합니다.
                    if (player.CustomProperties != null && player.CustomProperties.ContainsKey("score1"))
                    {
                        playerData.score1 = (bool)player.CustomProperties["score1"];
                    }
                    else
                    {
                        playerData.score1 = false;
                    }

                    if (player.CustomProperties != null && player.CustomProperties.ContainsKey("score2"))
                    {
                        playerData.score2 = (bool)player.CustomProperties["score2"];
                    }
                    else
                    {
                        playerData.score2 = false;
                    }

                    playersStatus.Add(playerData);
                }
            }
        }

        return playersStatus;
    }

    public PlayerData GetMyStatus()
    {
        PlayerData myData = new PlayerData();

        // 현재 로컬 플레이어의 상태를 가져옵니다.
        Photon.Realtime.Player localPlayer = PhotonNetwork.LocalPlayer;

        // PlayerData 인스턴스에 닉네임과 ID를 설정합니다.
        myData.Nickname = localPlayer.NickName;
        myData.PlayerID = localPlayer.ActorNumber;

        // 플레이어의 "isReady" 상태를 확인하고 설정합니다.
        if (localPlayer.CustomProperties.ContainsKey("isReady"))
        {
            myData.IsReady = (bool)localPlayer.CustomProperties["isReady"];
        }
        else
        {
            myData.IsReady = false;
        }
        // 플레이어의 "score1"과 "score2" 상태를 확인하고 설정합니다.
        if (localPlayer.CustomProperties.ContainsKey("score1"))
        {
            myData.score1 = (bool)localPlayer.CustomProperties["score1"];
        }
        else
        {
            myData.score1 = false;
        }

        if (localPlayer.CustomProperties.ContainsKey("score2"))
        {
            myData.score2 = (bool)localPlayer.CustomProperties["score2"];
        }
        else
        {
            myData.score2 = false;
        }



        return myData;
    }

    public void ExitRoom()
    {
        // 방을 떠남
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        // 방을 떠난 후 실행할 코드를 여기에 작성합니다.
        // 예를 들어 메인 화면으로 씬을 전환하거나 필요한 초기화 작업을 수행할 수 있습니다.

        // 메인 화면 씬 이름을 "MainScene"으로 가정하고, LoadScene 함수로 씬을 전환합니다.
        Destroy(gameObject);
        SceneManager.LoadScene("MainMenuScene");
    }

    public void SetChangeScene()
    {
        
        PV.RPC("ChangeScene", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void ChangeScene()
    {
        Debug.Log("씬 변경 시작!");
        // 실제 문을 열 때 다음 맵을 결정합니다.
        if (currentMap == "Game Play Map1")
        {
            currentMap = "Game Play Map2";
        }
        else
        {
            currentMap = "Game Play Map1";
        }

        SceneManager.LoadScene(currentMap);
    }

}

public class PlayerData {
    public string Nickname { get; set; }
    public int PlayerID { get; set; }
    public bool IsReady { get; set; }
    public bool score1 { get; set; }
    public bool score2 { get; set; }
}
