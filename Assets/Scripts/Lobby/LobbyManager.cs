using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class LobbyManager : NetworkBehaviour
{

    public SyncListGameObject KnownPlayers = new SyncListGameObject();
    public SyncListGameObject UnknownPlayers = new SyncListGameObject();

    [SerializeField] private DevicesListController devicesListController;
    
    public static LobbyManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPlayerToList(EmptyPlayer p, out int index)
    {

        if (PlayerPrefs.GetString(p.Fingerprint) == "")
        {
            UnknownPlayers.Add(p.gameObject);
            index = UnknownPlayers.Count;
        }
        else
        {
            KnownPlayers.Add(p.gameObject);
            index = KnownPlayers.Count;
        }
        
       
        //devicesListController.AddPlayer(p);
    }

    public void SetPlayerKnown(EmptyPlayer emptyPlayer, out int newIndex)
    {
        UnknownPlayers.Remove(emptyPlayer.gameObject);
        KnownPlayers.Add(emptyPlayer.gameObject);
        newIndex = KnownPlayers.Count;
    }
    
}

[Serializable]
public class SyncListGameObject : SyncList<GameObject>{}