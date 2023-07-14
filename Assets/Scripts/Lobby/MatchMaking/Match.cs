using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Match : NetworkBehaviour
{
    
    public string MatchID;
    [SyncVar]
    public string Name;
    public SyncListGameObject Players = new();
    [SyncVar]    
    public int MaxPlayers;

    [SyncVar] public MatchStatus Status;
    
    
    public Match(string matchID, GameObject playerHost)
    {
        // print("Match ctor is called " + this);
        this.MatchID = matchID;
        this.Players.Add(playerHost);
    }

    public Match()
    {
        
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

public enum MatchStatus
{
    Running, 
    Ready
}
