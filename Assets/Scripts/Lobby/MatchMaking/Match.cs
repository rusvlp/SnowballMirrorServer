using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using Mirror.Examples.CCU;
using Unity.VisualScripting;
using UnityEngine;

public class Match : NetworkBehaviour
{

    public string MatchID;
    [SyncVar] public string Name;
    public SyncListGameObject Players = new();
    [SyncVar] public int MaxPlayers;

    [SyncVar] public MatchStatus Status;

    [SerializeField] private List<GameObject> playersInGame = new();

    private bool isOtherPlayersSpawned = false;
    private bool isMatchStarted = false;
    public static Action<string, MatchStatus> OnStatusChanged;

    private void OnEnable()
    {
        EmptyPlayer.OnPlayerCreated += RegisterPlayerObjectHandler;
    }

    private void OnDestroy()
    {
        EmptyPlayer.OnPlayerCreated -= RegisterPlayerObjectHandler;
    }

    public void RegisterPlayerObjectHandler(GameObject player)
    {
        this.playersInGame.Add(player);
    }


    private void Update()
    {
        
        if (Players.Count == playersInGame.Count && !isOtherPlayersSpawned && isMatchStarted)
        {
            print(Players.Count + " " + playersInGame.Count);
            print("Calling SpawnPlayerGameObjectsInMatch");
        //    SpawnPlayerGameObjectsInMatch();
            isOtherPlayersSpawned = true;
        }
    }

    private void SpawnPlayerGameObjectsInMatch()
    {

        foreach (GameObject epGo in Players)
        {
            EmptyPlayer ep = epGo.GetComponent<EmptyPlayer>();
            ep.SpawnOtherPlayers(playersInGame);
        }
        
        
    }
    
    public Match(string matchID, GameObject playerHost)
    {
        // print("Match ctor is called " + this);
        this.MatchID = matchID;
        this.Players.Add(playerHost);
    }

    public void StartMatch()
    {
        this.Status = MatchStatus.Running;

       
        
        foreach (GameObject epGo in Players)
        {
            EmptyPlayer ep = epGo.GetComponent<EmptyPlayer>();
            print(ep.Name);
            ep.GetComponent<NetworkMatch>().matchId = GetComponent<NetworkMatch>().matchId;
            ep.GuidMatchId = GetComponent<NetworkMatch>().matchId;
            
            ep.StartGame();
            ep.PlayerStatus = global::Status.InGame;
           
          
        }

        isMatchStarted = true;
        print($"Match {MatchID} is started");
        
        OnStatusChanged?.Invoke(MatchID, MatchStatus.Running);
    }

    public void AddPlayerToMatch(EmptyPlayer ep)
    {
        if (!Players.Contains(ep.gameObject))
        {
            Players.Add(ep.gameObject);
            if (ep.PlayerStatus != global::Status.InGame && Status == MatchStatus.Running)
            {
                
                ep.PlayerStatus = global::Status.InGame;
                ep.GetComponent<NetworkMatch>().matchId = GetComponent<NetworkMatch>().matchId;
                ep.GuidMatchId = GetComponent<NetworkMatch>().matchId;
                ep.StartGame();
            }
        }
    }

    public void RemovePlayerFromMatch(EmptyPlayer ep)
    {
        if (Players.Contains(ep.gameObject))
        {
            print($"{MatchID} contains player {ep.PlayerGlobalIndex}");
            Players.Remove(ep.gameObject);
            
            
            if (ep.PlayerStatus == global::Status.InGame && Status == MatchStatus.Running)
            {
                ep.StopGame();
                ep.PlayerStatus = global::Status.InLobby;
                print($"{ep.PlayerMatchIndex} removing from session");
                ep.GetComponent<NetworkMatch>().matchId = Guid.Empty;
                ep.GuidMatchId = Guid.Empty;
            }
        }
    }

    public void StopMatch()
    {
        foreach (GameObject epGo in Players)
        {
            EmptyPlayer ep = epGo.GetComponent<EmptyPlayer>();
            ep.PlayerStatus = global::Status.InMatch;
            ep.StopGame();
        }
        
        
        this.Status = MatchStatus.Ready;
        print($"Match {MatchID} is stopped");
        OnStatusChanged?.Invoke(MatchID, MatchStatus.Ready);
    }
    
    public Match()
    {
        
    }
    
    // Start is called before the first frame update
    void Start()
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
