using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
    // Start is called before the first frame update

    public static CustomNetworkManager Instance;
    
    [Header("Lobbying")]
    [Scene] public string GameScene;
    
    void Start()
    {
       
        Instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   // public override void OnStartServer() => spawnPrefabs = Resources.LoadAll<GameObject>("SpawnablePrefabs").ToList();

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
