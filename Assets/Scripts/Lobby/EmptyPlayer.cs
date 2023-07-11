using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class EmptyPlayer : NetworkBehaviour
{

    public int PlayerIndex;
    public string Fingerprint;
    
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
        LobbyManager.Instance.AddPlayerToList(this, out this.PlayerIndex);
        print("Player added, it's index " + this.PlayerIndex);
    }
}
