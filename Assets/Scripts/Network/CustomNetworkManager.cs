using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using Unity.VisualScripting;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
    // Start is called before the first frame update

    public static CustomNetworkManager Instance;
    
    [Header("Lobbying")]
    [Scene] public string GameScene;

    public static Action OnServerStarted;
    
    void Start()
    {
       
        Instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Action<NetworkConnectionToClient, GameManager.SceneLoadedMessage> OnSceneLoaded;


    public override void OnStartServer()
    {
        
        NetworkServer.RegisterHandler<GameManager.SceneLoadedMessage>(SceneLoaded);
        OnServerStarted.Invoke();
        // spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();
    }

    public void SceneLoaded(NetworkConnectionToClient conn, GameManager.SceneLoadedMessage msg)
    {
        OnSceneLoaded?.Invoke(conn, msg);
    }

    public void SendSceneLoadedMessage(GameManager.SceneLoadedMessage msg)
    {
        
      
    }
    
    public override void OnStartClient()
    {
        /*
        List<GameObject> spawnablePrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();

        foreach (GameObject prefab in spawnablePrefabs)
        {
            NetworkClient.RegisterPrefab(prefab);
        }
        */
    }

   

}
