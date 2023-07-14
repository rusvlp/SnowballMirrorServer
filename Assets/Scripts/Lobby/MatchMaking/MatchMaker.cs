using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Lobby.MatchMaking;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;

public class MatchMaker : NetworkBehaviour
{
    [SerializeField] private Match _matchPrefab;

    public SyncListMatches Matches = new();
    public SyncListString MatchIDs = new();

    public static MatchMaker Instance;

    public Match CreateMatch(string name, string matchID, int maxPlayers)
    {

        if (!MatchIDs.Contains(matchID))
        {
            Match match = Instantiate(_matchPrefab);
            match.MatchID = matchID;
            match.MaxPlayers = maxPlayers;
            match.Name = name;
            
            
            match.Status = MatchStatus.Ready;
            
            match.GetComponent<NetworkMatch>().matchId = matchID.toGuid();
            
            Matches.Add(match);
            MatchIDs.Add(matchID);
            
            print($"Match with name {name} and {matchID} created successfully");
            return match;
        }
        else
        {
            print($"Match with {matchID} is already exists");
        }

        return null;


    }

    public bool AddPlayerToMatch(EmptyPlayer player, string matchId, out int indexInMatch)
    {
        indexInMatch = -1;

        if (player.PlayerStatus != Status.InLobby)
        {
            print("Player is already in game or match");
            return false;
        }
        
        if (MatchIDs.Contains(matchId))
        {
            Match match = Matches.Find(m => m.MatchID.Equals(matchId));
            match.Players.Add(player.gameObject);
            player.PlayerStatus = Status.InMatch;
            indexInMatch = match.Players.Count;
        }
        return false;
    }
    
    void Start()
    {
        Instance = this;
    }

    
    void Update()
    {
        
    }
    
    
    
    public static string GetRandomMatchID()
    {
        string _id = string.Empty;

        for (int i = 0; i < 5; i++)
        {
            int random = UnityEngine.Random.Range(0, 36);
            if (random < 26)
            {
                _id += (char)(random + 65);
            } else
            {
                _id += (random - 26).ToString();
            }
        }

        //print("Random Match ID iz: " + _id);
        return _id;

    }
}




[System.Serializable]
public class SyncListString : SyncList<string>{}


