using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Lobby.MatchMaking;
using Mirror.Examples.CCU;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UISessionEditWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text sessionNameTmp;
    [SerializeField] private TMP_Text sessionMatchIdTmp;
    [SerializeField] private Transform registeredPlayersParent;
    [SerializeField] private GameObject editMatchPlayerPrefab;
    [SerializeField] private Transform playersInSessionParent;
    [SerializeField] private TMP_Text numberOfPlayersTmp;
    [SerializeField] private Color inSessionColor;
    [SerializeField] private TMP_Text StartStopButton_tmp;
    
    
    //DEVELOPMENT_BUILD
    public GameObject playerPrefab;
    
    [SerializeField]
    private Match _match;
    public List<UIEditMatchPlayer> UiPlayersToMoveToSession = new List<UIEditMatchPlayer>();
    public List<UIEditMatchPlayer> UiPlayersToMoveToGlobal = new List<UIEditMatchPlayer>();
    
    public List<UIEditMatchPlayer> UiPlayersInSession = new List<UIEditMatchPlayer>();
    public List<UIEditMatchPlayer> UiPlayersGlobal = new List<UIEditMatchPlayer>(); 
    
    [SerializeField]
    private int currentPlayersInSession = 0;

    public static UISessionEditWindow Instance;
    
    

    public Match Session
    {
        get => _match;
        set
        {
            sessionNameTmp.text = value.Name;
            sessionMatchIdTmp.text = value.MatchID;
            currentPlayersInSession = value.Players.Count;
            numberOfPlayersTmp.text = $"{currentPlayersInSession}/{value.MaxPlayers}";
            _match = value;
        }
    }

    public void Open()
    {
       #if DEVELOPMENT_BUILD

        List<GameObject> knownGameObjects = generateMockPlayers(5, Status.InLobby);
        
        #else
        
        List<GameObject> knownGameObjects = LobbyManager.Instance.KnownPlayers.Select(m => m).ToList();
        
        #endif

        

        LobbyManager manager = LobbyManager.Instance;
        List<EmptyPlayer> knownPlayers = knownGameObjects
            .Select(gObj => gObj.GetComponent<EmptyPlayer>())
            .ToList()
            .FindAll(player => player.PlayerStatus == Status.InLobby);

        foreach (EmptyPlayer ep in knownPlayers)
        {
            GameObject pl = Instantiate(editMatchPlayerPrefab, registeredPlayersParent);
            UIEditMatchPlayer uiPl = pl.GetComponent<UIEditMatchPlayer>();
            uiPl.Player = ep;
            UiPlayersGlobal.Add(uiPl);
        }
      

        
        
     
        
        foreach (GameObject epGo in _match.Players)
        {
            EmptyPlayer ep = epGo.GetComponent<EmptyPlayer>();
            
            GameObject pl = Instantiate(editMatchPlayerPrefab, playersInSessionParent);
            UIEditMatchPlayer uiPl = pl.GetComponent<UIEditMatchPlayer>();
            uiPl.Player = ep;
            uiPl.Status = UIPlayerStatus.InSession;
            uiPl.ChangeBackgroundColor(this.inSessionColor);
            UiPlayersInSession.Add(uiPl);
        }


        if (_match.Status == MatchStatus.Running)
        {
            StartStopButton_tmp.text = "Остановить";
        } else if (_match.Status == MatchStatus.Ready)
        {
            StartStopButton_tmp.text = "Запустить";
        }
    }

    public void Close()
    {
        UiPlayersToMoveToSession = new List<UIEditMatchPlayer>();
        UiPlayersToMoveToGlobal = new List<UIEditMatchPlayer>();

        foreach (UIEditMatchPlayer pl in UiPlayersGlobal)
        {
            Destroy(pl.gameObject);
        }

        foreach (UIEditMatchPlayer pl in UiPlayersInSession)
        {
            Destroy(pl.gameObject);
        }

        UiPlayersGlobal = new List<UIEditMatchPlayer>();
        UiPlayersInSession = new List<UIEditMatchPlayer>();
    }

    
    public void MoveToSession()
    {
        foreach (UIEditMatchPlayer player in UiPlayersToMoveToSession)
        {
            player.gameObject.transform.SetParent(playersInSessionParent);
            player.Status = UIPlayerStatus.InSession;
            player.Check();
            UiPlayersInSession.Add(player);
            UiPlayersGlobal.Remove(player);
        }

        currentPlayersInSession += UiPlayersToMoveToSession.Count;

        numberOfPlayersTmp.text = $"{currentPlayersInSession}/{_match.MaxPlayers}";
        UiPlayersToMoveToSession = new List<UIEditMatchPlayer>();
    }

    public void MoveToGlobal()
    {
        foreach (UIEditMatchPlayer player in UiPlayersToMoveToGlobal) 
        {
            player.gameObject.transform.SetParent(registeredPlayersParent);
            player.Status = UIPlayerStatus.Global;
            player.Check();
            UiPlayersInSession.Remove(player);
            UiPlayersGlobal.Add(player);           
        }
        
        currentPlayersInSession -= UiPlayersToMoveToGlobal.Count;
        numberOfPlayersTmp.text = $"{currentPlayersInSession}/{_match.MaxPlayers}";

        UiPlayersToMoveToGlobal = new List<UIEditMatchPlayer>();
    }


    public void Apply()
    {

        

        foreach (UIEditMatchPlayer uiPl in UiPlayersGlobal)
        {
            
            uiPl.ResetBackgroundColor();
            if (_match.Players.Contains(uiPl.Player.gameObject))
            {
                _match.RemovePlayerFromMatch(uiPl.Player);
                print($"Player with index {uiPl.Player.PlayerGlobalIndex} was removed from match {_match.MatchID}");
            }
            
           
        }
        
        
        foreach (UIEditMatchPlayer uiPl in UiPlayersInSession)
        {
            uiPl.Player.PlayerStatus = Status.InMatch;
            uiPl.ChangeBackgroundColor(this.inSessionColor);

            if (!_match.Players.Contains(uiPl.Player.gameObject))
            {
                _match.AddPlayerToMatch(uiPl.Player);
            }
            
            
            
        }
        
    }

   
    public void StartStopMatch()
    {
        if (_match.Status == MatchStatus.Ready)
        {
            // Запускаем матч
            StartStopButton_tmp.text = "Остановить";
            MatchMaker.Instance.StartMatch(_match);
            
           
        }
        else
        {
            // Стопаем матч
            StartStopButton_tmp.text = "Запустить";
            _match.StopMatch();
        }
    }
    
    #if DEVELOPMENT_BUILD
    private List<GameObject> generateMockPlayers(int numberOfPlayers, Status status)
    {
        List<GameObject> toRet = new();

        for (int i = 0; i < numberOfPlayers; i++)
        {
            GameObject player = Instantiate(Instance.playerPrefab);
            EmptyPlayer emptyPlayer = player.GetComponent<EmptyPlayer>();
            emptyPlayer.SetFields(i, i, "Mock", status);
            toRet.Add(player);
        }
        
        return toRet;
    }


    
    
    #endif
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        
        #if DEVELOPMENT_BUILD
        Match match = new Match
        {
            MaxPlayers = 15,
            MatchID = "Mock", 
            Name = "Mock",
            Status = MatchStatus.Ready
        };

        match.Players = generateMockPlayers(5, Status.InMatch).Select(m => m).ToSyncList();
        
       
        
        foreach (GameObject go in match.Players)
        {
            EmptyPlayer ep = go.GetComponent<EmptyPlayer>();
            print($"{ep.Name}, id: {ep.PlayerGlobalIndex}");
        }

        this.Session = match;
        
        Open();
        
        //print(_match.Players);
        #endif
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
