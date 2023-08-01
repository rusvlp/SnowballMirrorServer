using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGamePlayer : NetworkBehaviour
{

    [SerializeField] private Rigidbody _rigidbody;

    [SyncVar] [SerializeField] private float _speed;

    public static Action CmdMovePlayerCalled;
    
    // Start is called before the first frame update
    void Start()
    {
        this._rigidbody = GetComponent<Rigidbody>();

        
        
        if (isClient && isOwned)
        {
            print(isOwned);
            InputManager.Instance.InGamePlayer = this;
        }

        if (isServer)
        {
            _speed = 5;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    [Command]
    public void CmdMovePlayer(Vector3 movementVector)
    {
        print($"$Command to move is received, connection {this.connectionToClient}");
        _rigidbody.AddForce(movementVector.normalized * _speed);
        CmdMovePlayerCalled?.Invoke();
    }

    public void MovePlayer(Vector3 movementVector)
    {
        _rigidbody.AddForce(movementVector.normalized * _speed);
    }

}
