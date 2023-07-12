using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;

public class EmptyPlayer : NetworkBehaviour
{

    public int PlayerGlobalIndex;
    public int PlayerMatchIndex;
    public string Fingerprint;
    public string Name;
    public Status PlayerStatus = Status.InLobby;
    
    public static EmptyPlayer LocalPlayer;
    
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
