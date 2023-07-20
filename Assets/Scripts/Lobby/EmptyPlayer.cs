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
    public int PlayerMatchIndex;
    public string Fingerprint;
    public string Name;
    public Status PlayerStatus = Status.InLobby;
    public Guid GuidMatchId;

    public GameObject PlayerInGame;
    
    public static Action<Guid> OnGameStarted;
    public static Action<GameObject> OnPlayerCreated;
    
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        TargetStartGame();
    }

    public void StopGame()
    {

        TargetStopGame();
    }


    [TargetRpc]
    public void TargetStopGame()
    {
        print("Exiting game...");
        SceneManager.LoadScene(1);
    }

    [TargetRpc]
    public void TargetStartGame()
    {
        print("Starting game...");
        SceneManager.LoadScene(2, LoadSceneMode.Additive);
        OnGameStarted?.Invoke(GuidMatchId);
        CmdStartGame();
    }

    [Command]
    public void CmdStartGame()
    {
        print("Cmd Start Game is called");
        SpawnPlayerPrefab();
    }


    [Server]
    public void SpawnPlayerPrefab()
    {
        print("Spawn player prefab is called");
        Vector3 position = new Vector3(0, 2, 0);
        Quaternion rotation = new Quaternion(0, 0, 0, 0);
        
        GameObject playerPrefab = CustomNetworkManager.Instance.spawnPrefabs[0];
        
        print("Registering in game player object");
        this.PlayerInGame = Instantiate(playerPrefab, position, rotation);
        print($"Registered. {PlayerInGame}");
        PlayerInGame.GetComponent<NetworkMatch>().matchId = GuidMatchId;
        
        RegisterPlayerObject(PlayerInGame);
        
        NetworkServer.Spawn(PlayerInGame, connectionToClient);
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
