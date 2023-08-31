using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mirror;
using Mirror.Examples.CCU;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Match : NetworkBehaviour
{

    public string MatchID;
    [SyncVar] public string Name;
    public SyncListGameObject Players = new();
    [SyncVar] public int MaxPlayers;

    [SyncVar] public MatchStatus Status;

    [SyncVar] public Scene GameScene; 


    [SerializeField] private List<GameObject> playersInGame = new();

    //private bool isOtherPlayersSpawned = false;
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
        StartCoroutine(StartMatchRoutine(this));
    }


    private IEnumerator StartMatchRoutine(Match match)
    {
        // Wait until scene is loaded on server
        
        yield return SceneManager.LoadSceneAsync(CustomNetworkManager.Instance.GameScene, new LoadSceneParameters { loadSceneMode = LoadSceneMode.Additive, localPhysicsMode = LocalPhysicsMode.Physics3D });

        match.GameScene = SceneManager.GetSceneAt(MatchMaker.Instance.LoadedScenes.Count + 1);
        MatchMaker.Instance.LoadedScenes.Add(match.GameScene);
        
        // SceneManager.MoveGameObjectToScene(match.gameObject, match.GameScene);
        
        foreach (GameObject epGo in match.Players)
        {
            EmptyPlayer ep = epGo.GetComponent<EmptyPlayer>();
            ep.SceneForPlayer = match.GameScene;
            ep.StartGame();
        }
         
       
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

                ep.SceneForPlayer = this.GameScene;
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
             
            }
        }
    }

    public void StopMatch()
    {
        StartCoroutine(StopMatchRoutine());

    }

    public IEnumerator StopMatchRoutine()
    {
        foreach (GameObject epGo in Players)
        {
            EmptyPlayer ep = epGo.GetComponent<EmptyPlayer>();
            ep.PlayerStatus = global::Status.InMatch;
            ep.StopGame();
        }

        this.Status = MatchStatus.Stopping;

        MatchMaker.Instance.LoadedScenes.Remove(GameScene); 
        
        yield return SceneManager.UnloadSceneAsync(GameScene);
        
        
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

    
    /*
  public void StartMatch()
  {
      this.Status = MatchStatus.Running;

     
      
      foreach (GameObject epGo in Players)
      {
          EmptyPlayer ep = epGo.GetComponent<EmptyPlayer>();
          print(ep.Name);
          //ep.GetComponent<NetworkMatch>().matchId = GetComponent<NetworkMatch>().matchId;
        
          
          ep.StartGame();
          ep.PlayerStatus = global::Status.InGame;
         
        
      }

      isMatchStarted = true;
      print($"Match {MatchID} is started");
      
      OnStatusChanged?.Invoke(MatchID, MatchStatus.Running);
  }
  */
   
}




public enum MatchStatus
{   
    Starting,
    Running, 
    Stopping,
    Ready
}
