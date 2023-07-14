using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISessionEditWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text sessionNameTmp;
    [SerializeField] private TMP_Text sessionMatchIdTmp;
    [SerializeField] private Transform registeredPlayersParent;
    [SerializeField] private GameObject editMatchPlayerPrefab;
    [SerializeField] private Transform playersInSessionParent;
    [SerializeField] private TMP_Text numberOfPlayersTmp;
    private Match _match = new Match
    {
        MaxPlayers = 15,
        Players = new SyncListGameObject(),
        MatchID = "Mock", 
        Name = "Mock"
    };

    public List<UIEditMatchPlayer> UiPlayersToMoveToSession = new List<UIEditMatchPlayer>();
    public List<UIEditMatchPlayer> UiPlayersToMoveToGlobal = new List<UIEditMatchPlayer>();

    public List<UIEditMatchPlayer> UiPlayersInSession = new List<UIEditMatchPlayer>();
    public List<UIEditMatchPlayer> UiNewPlayersInSession = new List<UIEditMatchPlayer>();
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
        LobbyManager manager = LobbyManager.Instance;
        List<EmptyPlayer> knownPlayers = manager
            .KnownPlayers
            .Select(gObj => gObj.GetComponent<EmptyPlayer>())
            .ToList()
            .FindAll(player => player.PlayerStatus == Status.InLobby);

        foreach (EmptyPlayer ep in knownPlayers)
        {
            GameObject pl = Instantiate(editMatchPlayerPrefab, registeredPlayersParent);
            UIEditMatchPlayer uiPl = pl.GetComponent<UIEditMatchPlayer>();
            uiPl.Player = ep;
        }

        foreach (GameObject epGo in _match.Players)
        {
            EmptyPlayer ep = epGo.GetComponent<EmptyPlayer>();

            GameObject pl = Instantiate(editMatchPlayerPrefab, playersInSessionParent);
            UIEditMatchPlayer uiPl = pl.GetComponent<UIEditMatchPlayer>();
            uiPl.Player = ep;
            UiPlayersInSession.Add(uiPl);
        }
    }

    public void MoveToSession()
    {
        foreach (UIEditMatchPlayer player in UiPlayersToMoveToSession)
        {
            player.gameObject.transform.SetParent(playersInSessionParent);
            player.Status = UIPlayerStatus.InSession;
            player.Check();
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
        }
        
        currentPlayersInSession -= UiPlayersToMoveToGlobal.Count;
        numberOfPlayersTmp.text = $"{currentPlayersInSession}/{_match.MaxPlayers}";

        UiPlayersToMoveToGlobal = new List<UIEditMatchPlayer>();
    }


    private SyncListGameObject generateMockPlayers()
    {
        SyncListGameObject toRet = new SyncListGameObject();
        return toRet;
    }
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
