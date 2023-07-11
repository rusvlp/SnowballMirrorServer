using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
    // Start is called before the first frame update

    public static CustomNetworkManager Instance; 
    
    void Start()
    {
       
        Instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnStartServer()
    {
        base.OnStartServer();
    }
    
    
}
