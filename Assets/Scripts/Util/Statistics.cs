using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    [SerializeField] private int CmdMovePlayerCalled = 0;
    [SerializeField] private int ClientMovePlayer = 0;

    private Dictionary<string, TMP_Text> invokers = new Dictionary<string, TMP_Text>();
    
    private void OnEnable()
    {
        InputManager.MoveInputCalled += ClientMovePlayerHandler;
    }

    private void OnDisable()
    {
        InGamePlayer.CmdMovePlayerCalled += CmdMovePlayerCalledHandler;
    }

    public void CmdMovePlayerCalledHandler() => CmdMovePlayerCalled++;

    public void ClientMovePlayerHandler() => ClientMovePlayer++;
    // Start is called before the first frame update
    void Start()
    {
            
    }

    private void DrawFunctionCallsTMPs(List<string> functionNames)
    {
        foreach (string name in functionNames)
        {
            
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
