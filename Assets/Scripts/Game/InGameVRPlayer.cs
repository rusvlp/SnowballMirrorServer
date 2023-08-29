using System;
using System.Collections;
using System.Collections.Generic;
using App.Scripts.Weapons;
using Game.Sync;
using JetBrains.Annotations;
using Mirror;
using Unity.VisualScripting;
using UnityEngine;
using Object = System.Object;

public class InGameVRPlayer : NetworkBehaviour
{

    [SerializeField] private LaunchSystem _launchSystem;
    [SerializeField] private Camera _camera;

    [Header("Hands")] 
    [SerializeField] private HandScript _rightHandScript;
    [SerializeField] private HandScript _leftHandScript;

    [Header("Body")] 
    [SerializeField] private HeadFollowScript _headFollow;
    [SerializeField] private HeightSettings _heightSettings;

    private List<ILocalPlayerControl> _localPlayerControls = new List<ILocalPlayerControl>();
    public static Func<GXRInputManager> SettingGXRInputManager;

    // Start is called before the first frame update
    void Start()
    {
        
        SetupPlayerControls();
        
        print(isClient && isOwned);
        
        if (isClient && isOwned)
        {
            SetUpDependencies();
        }
        else
        {
            DisableLocalPlayerDependencies();
        }
    }

    private void SetupPlayerControls()
    {
        _localPlayerControls.Add(_rightHandScript);
        _localPlayerControls.Add(_leftHandScript);
        _localPlayerControls.Add(_headFollow);
        _localPlayerControls.Add(_heightSettings);
    }
    
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetUpDependencies()
    {
        print("SetUpDependencies is called");
        foreach (ILocalPlayerControl ilpc in _localPlayerControls)
        {
            ilpc.SetIsClientAndOwned(true);
        }

    }

    private void DisableLocalPlayerDependencies()
    {
        _camera.enabled = false;
        foreach (ILocalPlayerControl ilpc in _localPlayerControls)
        {
            ilpc.SetIsClientAndOwned(false);
        }
    }
}
