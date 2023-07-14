using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISessionController : MonoBehaviour
{
    [SerializeField] private GameObject createSessionWindow;

    [SerializeField] private TMP_InputField nameInputField;

    [SerializeField] private TMP_InputField playersInputField;

    [SerializeField] private GameObject uiSessionPrefab;

    [SerializeField] private Transform parent;

    [SerializeField] private GameObject editSessionWindow;
    
    public static UISessionController Instance;

    //private Match currentEditingMatch;
    
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCloseCreateSessionWindow()
    {
        if (createSessionWindow.activeSelf)
        {
            createSessionWindow.SetActive(false);
        }
        else
        {
            createSessionWindow.SetActive(true);
        }
    }

    public void CreateSession()
    {
        string name = nameInputField.text;
        int maxPlayers = Convert.ToInt32(playersInputField.text);
        string matchId = MatchMaker.GetRandomMatchID();
        print(name);
        Match match = MatchMaker.Instance.CreateMatch(name, matchId, maxPlayers);
        
        if (match != null)
        {
            UISession uiSession = Instantiate(uiSessionPrefab, parent).GetComponent<UISession>();
            uiSession.MatchSession = match;
            OpenCloseCreateSessionWindow();
        }
        else
        {
            
        }
    }

    public void OpenEditMatchWindow(Match match)
    {
        
        UISessionEditWindow editWindow = editSessionWindow.GetComponent<UISessionEditWindow>();
        editWindow.Session = match;
        editWindow.Open();
        editSessionWindow.SetActive(true);
    }

    public void CloseEditMatchWindow()
    {
        editSessionWindow.SetActive(false);
    }
}
