using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class EmptyPlayer : NetworkBehaviour
{


    public int PlayerGlobalIndex;
    
    [SyncVar]
    public int PlayerMatchIndex;

    public string Fingerprint;
    
    [SyncVar]
    public string Name;
    
    [SyncVar]
    public Status PlayerStatus = Status.InLobby;
    
 

    [SyncVar] public bool isClientSceneLoaded;
    
    
    public GameObject PlayerInGame;
    
    public static Action OnGameStarted;
    public static Action<GameObject> OnPlayerCreated;

    [SyncVar]
    public Scene SceneForPlayer;
    
    public static EmptyPlayer LocalPlayer;

#if DEVELOPMENT_BUILD
    public void SetFields(int playerGlobalIndex, int playerMatchIndex, string fingerprint, Status playerStatus)
    {
        this.PlayerGlobalIndex = playerGlobalIndex;
        this.PlayerMatchIndex = playerMatchIndex;
        this.Fingerprint = fingerprint;
        this.PlayerStatus = playerStatus;
    }
#endif

    void Start()
    {
        print("Player initialized");
        if (isLocalPlayer)
        {
            LocalPlayer = this;
            CmdAddPlayerToList();
            this.Fingerprint = SystemInfo.deviceUniqueIdentifier;
        }


        if (GameplayDevelopmentModeManager.Instance != null &&
            GameplayDevelopmentModeManager.Instance.addPlayerToMatchAfterConnecting)
        {
            MatchMaker.Instance.Matches[0].GetComponent<Match>().AddPlayerToMatch(this);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        CustomNetworkManager.OnSceneLoaded += SceneLoadedHandler;
        
    }

    private void OnDisable()
    {
        
    }

    public void SceneLoadedHandler(NetworkConnectionToClient conn, GameManager.SceneLoadedMessage msg)
    {
        if (this.connectionToClient == conn)
        {
            this.isClientSceneLoaded = msg.isLoaded;
        }
    }
    
    public void StartGame()
    {
        TargetStartGame();
    }

    [Server]
    public void StopGame()
    {
        NetworkServer.Destroy(PlayerInGame);
        TargetStopGame();
    }


    [TargetRpc]
    public void TargetStopGame()
    {
        print("Exiting game...");
        SceneManager.UnloadSceneAsync(2);
    }

    [TargetRpc]
    public void TargetStartGame()
    {
        print("Starting game...");
       // SceneManager.LoadScene(2, LoadSceneMode.Additive);
        OnGameStarted?.Invoke();
        CmdStartGame();
    }

    
    
    
    [Command]
    public void CmdStartGame()
    {
        print("Cmd Start Game is called");
        StartCoroutine(LoadSceneRoutine());
    }
    
    public IEnumerator LoadSceneRoutine()
    {
        // Wait until GameScene is loaded
        
        //yield return SceneManager.LoadSceneAsync(CustomNetworkManager.Instance.GameScene, LoadSceneMode.Additive);

        
        // idk why i added this, just want to be sure that client is ready
        yield return new WaitUntil(() => connectionToClient.isReady);
        
        connectionToClient.Send(new SceneMessage
        {
            sceneName = CustomNetworkManager.Instance.GameScene,
            sceneOperation = SceneOperation.LoadAdditive
        });
        
        
        
        yield return new WaitForEndOfFrame();

        StartCoroutine(SpawnPlayerPrefabRoutine());
    }


   
    
    [Server]
    public IEnumerator SpawnPlayerPrefabRoutine()
    {
        //RpcPrepareToSpawnSceneObjects();
        
        print("Spawn player prefab is called");
        Vector3 position = new Vector3(0, 0, 0);
        Quaternion rotation = new Quaternion(0, 0, 0, 0);
        
        GameObject playerPrefab = CustomNetworkManager.Instance.spawnPrefabs[1];

       
        print("Server received information, that client has loaded scene");
        
        this.PlayerInGame = Instantiate(playerPrefab, position, rotation);

        NetworkServer.Spawn(PlayerInGame, connectionToClient);
        yield return new WaitForEndOfFrame();
        SceneManager.MoveGameObjectToScene(this.PlayerInGame, SceneForPlayer);
        
    

        TargetPrint("Player spawned");
        yield break;

    }

    [TargetRpc]
    public void TargetPrint(string msg)
    {
        print(msg);
    }
    
    [TargetRpc]
    public void RpcPrepareToSpawnSceneObjects()
    {
        NetworkClient.PrepareToSpawnSceneObjects();
    }

    
    [Server]
    public void RegisterPlayerObject(GameObject playerObject)
    {
        OnPlayerCreated?.Invoke(playerObject);
    }
    
  
    [Server]
    public void SpawnOtherPlayers(List<GameObject> otherPlayers)
    {

        print("Spawn other players method is called");
        
        foreach (GameObject go in otherPlayers)
        {
            if (go == this.PlayerInGame)
            {
                continue;
            }
            
            print($"Spawning {go}");
            
            RpcSpawnOnClient(go);
            NetworkServer.Spawn(go, connectionToClient);
        }
    }

    [TargetRpc]
    public void RpcSpawnOnClient(GameObject player)
    {
        print($"Spawning {player}");
       // Instantiate(player);
    }
    
    [Command]
    public void CmdAddPlayerToList()
    {
        LobbyManager.Instance.AddPlayerToList(this, out this.PlayerGlobalIndex);
        DevicesListController.Instance.AddPlayer(this);
        print("Player added, it's index " + this.PlayerGlobalIndex);
    }

    public void SetKnown()
    {
        LobbyManager.Instance.SetPlayerKnown(this, out this.PlayerGlobalIndex);
        print("Player with fingerprint " + this.Fingerprint + " is known now");
    }
}

public enum Status
{
    InMatch,
    InGame,
    InLobby
}
