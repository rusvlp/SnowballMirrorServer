using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Match : NetworkBehaviour
{
    
    public string MatchID;
    public string Name;
    public SyncListGameObject Players = new();
   
    
    
    public Match(string matchID, GameObject playerHost)
    {
        // print("Match ctor is called " + this);
        this.MatchID = matchID;
        this.Players.Add(playerHost);
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}



[System.Serializable]
public class SyncListMatches : SyncList<Match>{}


