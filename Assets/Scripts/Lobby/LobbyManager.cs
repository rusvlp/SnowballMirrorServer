using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class LobbyManager : NetworkBehaviour
{

    public SyncListGameObject Players = new SyncListGameObject();

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
        
        Players.Add(p.gameObject);
        index = Players.Count;
        devicesListController.AddPlayer(p);
    }
}

[Serializable]
public class SyncListGameObject : SyncList<GameObject>{}