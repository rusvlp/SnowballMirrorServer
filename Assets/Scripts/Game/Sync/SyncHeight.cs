using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class SyncHeight : NetworkBehaviour
{

    [SyncVar] [SerializeField] private float _height;

    [SerializeField] private HeightSettings _heightSettings;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isClient && isOwned)
        {
            _height = _heightSettings.Height;
        }
        else
        {
            _heightSettings.Height = _height;
        }
    }
}
