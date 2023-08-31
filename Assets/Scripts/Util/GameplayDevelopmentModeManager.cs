using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GameplayDevelopmentModeManager : MonoBehaviour
{
    [CanBeNull] public static GameplayDevelopmentModeManager Instance;

    public bool isGameplayDevelopmentMode = true;

    public bool autoCreateSessionAtStartServer = true;

    public bool addPlayerToMatchAfterConnecting = true;
    // Start is called before the first frame update

    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        DontDestroyOnLoad(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
