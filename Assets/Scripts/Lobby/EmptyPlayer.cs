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
