using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class GameplayDevelopmentModeManager : MonoBehaviour
{
    [CanBeNull] public static GameplayDevelopmentModeManager Instance;

    public bool isGameplayDevelopmentMode = true;

    public bool autoCreateSessionAtStartServer = true;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
