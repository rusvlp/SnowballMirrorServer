using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UISessionEditWindow : MonoBehaviour
{
    [SerializeField] private TMP_Text sessionNameTmp;
    [SerializeField] private TMP_Text sessionMatchIdTmp;
    [SerializeField] private Transform registeredPlayersParent;
    [SerializeField] private GameObject editMatchPlayerPrefab;
    private Match _match;
    
    public Match Session
    {
        get => _match;
        set
        {
            sessionNameTmp.text = value.Name;
            sessionMatchIdTmp.text = value.MatchID;
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
