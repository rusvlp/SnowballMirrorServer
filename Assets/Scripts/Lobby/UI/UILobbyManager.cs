using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class UILobbyManager : NetworkBehaviour
{
    [SerializeField]
    private GameObject ClientCanvas;
    [SerializeField]
    private GameObject ServerCanvas;
    
    // Start is called before the first frame update
    void Start()
    {
        if (isServerOnly)
        {
            ServerCanvas.SetActive(true);
        }
        else
        {
            ClientCanvas.SetActive(true);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
