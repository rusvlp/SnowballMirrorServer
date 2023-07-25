using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGamePlayer : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (isClient)
        {
            SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetSceneByBuildIndex(2));

            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
