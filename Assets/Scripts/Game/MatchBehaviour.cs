using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class MatchBehaviour : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        EmptyPlayer.OnGameStarted += SetGuidMatchId;
    }

    private void OnDisable()
    {
        EmptyPlayer.OnGameStarted -= SetGuidMatchId;
    }

    private void SetGuidMatchId(Guid guidMatchId)
    {
        GetComponent<NetworkMatch>().matchId = guidMatchId;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
