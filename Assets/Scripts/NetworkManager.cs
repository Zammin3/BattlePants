using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;


public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private TMP_InputField nicknameInputField;
    [SerializeField]
    private GameObject enterRoomUI;



    public static NetworkManager instance;

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

        // Photon View 컴포넌트 레퍼런스 얻기
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        Connect();
    }

    void Connect()
    {
        // 마스터 서버에 연결
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("마스터 서버에 연결됨");
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
        Debug.Log(name + "님이 게임에 참가하였습니다.");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log(otherPlayer.NickName + "님이 게임에서 나갔습니다.");
        playersReady--;
    }

    // 플레이어가 준비되었음을 서버에 알리는 함수
    public void PlayerReady()
    {
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
    }

    public void OnClickEnterGameButton()
    {
        if(nicknameInputField.text != "")
        {
            PlayerSettings.nickname = nicknameInputField.text; 
        }
        else
        {
            nicknameInputField.GetComponent<Animator>().SetTrigger("on");
        }
    }



    public void CreateRoom()
    {
        if (nicknameInputField.text != "")
        {
            
            Connect();
        }
    }

    
}
