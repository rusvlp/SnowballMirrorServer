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
#if DEVELOPMENT_BUILD

        this.KnownPlayers = generateKnownPlayers(5);

#endif

        Instance = this;
    }


    #if DEVELOPMENT_BUILD
    private SyncListGameObject generateKnownPlayers(int numberOfPlayers)
    {
        SyncListGameObject toRet = new SyncListGameObject();
        
        for (int i = 0; i < numberOfPlayers; i++)
        {

            GameObject goPl = Instantiate(UISessionEditWindow.Instance.playerPrefab);
            EmptyPlayer pl = goPl.GetComponent<EmptyPlayer>();
            
            pl.SetFields(i, i, "Mock", Status.InLobby);
        }

        return toRet;
    }
    #endif

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

