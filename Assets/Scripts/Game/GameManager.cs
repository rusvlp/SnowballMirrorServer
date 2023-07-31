using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoadedHandler;
    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoadedHandler;
    }
    
    
    public void SceneLoadedHandler(Scene scene, LoadSceneMode lsm)
    {
        print($"Scene {scene.name} is loaded");
        /*
        CustomNetworkManager.Instance.SendSceneLoadedMessage(new SceneLoadedMessage
        {
            isLoaded = false
        });
        */
    }


    public struct SceneLoadedMessage : NetworkMessage
    {
        public bool isLoaded;
    }
    
    public void StartGame()
    {
        
    }
}
