using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class UISession : MonoBehaviour
{
    [SerializeField] private TMP_Text matchIdTmp;
    [SerializeField] private TMP_Text nameTmp;
    [SerializeField] private TMP_Text statusTmp;
    [SerializeField] private TMP_Text playersTmp; 
    //private Button editMatchButton;

    private Match _match = new Match
    {
        MatchID = "MOCK",
        MaxPlayers = 228,
        Status = MatchStatus.Ready,
        Name = "Mock"
    };


    private void OnEnable()
    {
        Match.OnStatusChanged += matchStatusChangingHandler;
    }

    private void OnDisable()
    {
        Match.OnStatusChanged -= matchStatusChangingHandler;
    }

    private void matchStatusChangingHandler(string matchId, MatchStatus status)
    {
        if (_match.MatchID == matchId)
        {
            statusTmp.text = status.ToString();
        }
    }
    
    
    public MatchStatus SetUiStatus
    {
        get => _match.Status;
        set
        {
            statusTmp.text = value.ToString();
        }
    }
    
    public Match MatchSession
    {
        get => _match;
        set
        {
            matchIdTmp.text = value.MatchID;
            nameTmp.text = value.Name;
            statusTmp.text = value.Status.ToString();
            playersTmp.text = $"0/{value.MaxPlayers}";
            _match = value;
        } 
    }

    /*
    public void SetFields(string matchId, string name, string status, int players)
    {
    }
    */

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            UISessionController.Instance.OpenEditMatchWindow(_match);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
