#define test
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PhotonView))]
public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager instance;
    public event System.Action onGameStart;
    public System.Action OnChangeLobby;
    private RobotData selectPrefab;

    [Header("Time")]
    // [SerializeField] float gamePlayTime;
    [SerializeField] private float playTime; //설정해준 플레이타임(current=150/s)
    [SerializeField] TextRound[] timeText; //외부에서 넣은 텍스트 모두 넣기.
    private bool isGameStart; //게임이 실행 중 이라면
    private readonly string padding = "                                          ";

    [Header("Spawn")]
    [SerializeField] GameObject mechPrefab;
    [SerializeField] List<Transform> spawnPoint;
    [SerializeField] GameObject networkObjectPool;
    public ZoneSize zs;
    public InsideSize insidezs;

    public GameObject myMech { get; private set; }
    private List<PhotonView> players = new List<PhotonView>();
    private int playerCount;
    private int prevSecond;
    public AudioSource ingameBgm;

#if test
    public override void OnConnectedToMaster() => PhotonNetwork.JoinLobby();
    public override void OnJoinedLobby() => PhotonNetwork.CreateRoom("TestRoom", new RoomOptions{MaxPlayers = 1, IsVisible = false, IsOpen = false}, TypedLobby.Default);
    public override void OnCreatedRoom() => InitGame();
#endif

    public void Init(UserInfo userInfo)
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;

        DataManager.GetInstance().LoadDatas();
        selectPrefab = DataManager.GetInstance().dicRobotDatas[userInfo.userId];
        
        if (PhotonNetwork.IsConnected){
            if(userInfo == null)
            { 
               selectPrefab = DataManager.GetInstance().dicRobotDatas[1];
                throw new System.Exception("Do UserInfo");
            }
            InitGame();
        }
        else
        {
            #if test
            Debug.LogWarning("GameManager is in test mode");
            if (PhotonNetwork.IsConnected == false) 
            {
                PhotonNetwork.ConnectUsingSettings();
            }
            else
            {
                InitGame();
            }
            #else
            throw new System.Exception("Not Connected to Photon Server");
            #endif
        }
    }

    void InitGame()
    {

        Transform spawn = spawnPoint[PhotonNetwork.IsMasterClient ? 0 : 1];

        myMech = PhotonNetwork.Instantiate(selectPrefab.inGame_name, spawn.position, spawn.rotation);
        Instantiate(networkObjectPool);
        zs.Init();
        insidezs.Init();
        photonView.RPC("Ready", RpcTarget.MasterClient);
        if (PhotonNetwork.IsMasterClient)
            StartCoroutine(CheckBeforeStart());
        
        // playTime = 150f; //플레이타임 시간 설정
    }

    void FixedUpdate()
    {
        if (isGameStart == true)
        {
            playTime -= Time.fixedDeltaTime; //시간이 떨어지게 해준다.
            if (playTime <= 0)
            {
                if (PhotonNetwork.IsMasterClient)
                    photonView.RPC("OnTimeOver", RpcTarget.AllViaServer);

                for (int i = 0; i < timeText.Length; i++)
                    timeText[i].text = "TimeOver" + padding; //스페이스바 필요
                isGameStart = false;
                return;
            }

            int min = (int)(playTime / 60);
            int second = (int)(playTime % 60);
            if (prevSecond != second) // 초가 달라질때만 text 업데이트
            {
                prevSecond = second;
                string time = string.Format("{0:00}", min) + ":" + string.Format("{0:00}", second) + padding; //스페이스바 필요
                for (int i = 0; i < timeText.Length; i++)
                    timeText[i].text = time;
            }
        }
    }

    public void RegisterMech(PhotonView mech) => players.Add(mech);
    public void OnPlayerDeath() => photonView.RPC("Death", RpcTarget.All);    
    public override void OnPlayerLeftRoom(Player otherPlayer) => ShowResult(true);
    public override void OnLeftRoom(){
       
    }
    void CompareHp(float myhp, float enemyhp) // 적과 내 HP를  비교
    {
        if (myhp > enemyhp) //내 HP가 상대 HP보다 크면 VICTORY
            ShowResult(true);
        else if (myhp == enemyhp && PhotonNetwork.IsMasterClient) //동일하면 마스터 승리
            ShowResult(true);
        else
            ShowResult(false);
    }

    void ShowResult(bool isWinner)
    {
        if(isWinner){
            for(int i = 0 ; i < players.Count; i++){
              if(!players[i].Mine){
                    players[i].GetComponent<Status>().OnRemotePlayerDeath();
                }
            }
        }
        isGameStart = false;
        ResultUI ru = myMech.GetComponentInChildren<ResultUI>();
        #if UNITY_EDITOR
            if (ru == null) ru = GameObject.FindObjectOfType<ResultUI>();
        #endif
        ru?.ShowResult(isWinner);
        StartCoroutine(LeaveRoom());
    }

#region Coroutine
    IEnumerator CheckBeforeStart()
    {
        while (playerCount != players.Count || players.Count != PhotonNetwork.CurrentRoom.PlayerCount)
            yield return null;


        photonView.RPC("GameStart", RpcTarget.AllViaServer);
    }
    IEnumerator StartCountDown()
    {
        yield return new WaitForSecondsRealtime(14f);

        foreach (var door in GameObject.FindObjectsOfType<DoorSystem>())
        {
            door.Open();
            StartCoroutine(Deactivate(door.transform.root.gameObject));
        }

        onGameStart?.Invoke();
    }
    IEnumerator Deactivate(GameObject obj)
    {
        yield return new WaitForSecondsRealtime(3f);
        LightManager.instance.OnDefultLight();
        obj.SetActive(false);
    }
    IEnumerator LeaveRoom()
    {
        yield return new WaitForSeconds(5F);
        myMech.GetComponentInChildren<BlackBackGround>().StartChangeSceanBlackBackGround();
        yield return new WaitForSeconds(3F);
        PhotonNetwork.LeaveRoom();
        yield return new WaitForSeconds(1F);
        PhotonNetwork.LoadLevel(3);
        PhotonNetwork._AsyncLevelLoadingOperation.allowSceneActivation = false;
        PhotonNetwork._AsyncLevelLoadingOperation.completed += (obj) =>
        {
            OnChangeLobby();
        };
        PhotonNetwork._AsyncLevelLoadingOperation.allowSceneActivation = true;
    }
#endregion
    
#region RPC
    [PunRPC] private void Ready() => playerCount++;
    [PunRPC] private void GameStart()
    {
        isGameStart = true;
        ingameBgm.enabled = true;
        StartCoroutine(StartCountDown());
    } 
    [PunRPC] private void OnTimeOver()
    {
        isGameStart = false;
        Status status = myMech.GetComponent<Status>();
        status.lockHp = true;
        photonView.RPC("RcvHp", RpcTarget.Others, status.HP);
    }
    [PunRPC] private void RcvHp(float enemyHp, PhotonMessageInfo info) => CompareHp(myMech.GetComponent<Status>().HP, enemyHp);
    [PunRPC] private void Death(PhotonMessageInfo info)
    {
        if (info.Sender.IsLocal == false)
            ShowResult(true);
        else
            ShowResult(false);
    }
#endregion
}
