using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGamePlayer : NetworkBehaviour
{

    [SerializeField] private Rigidbody _rigidbody;

    [SyncVar] [SerializeField] private float _speed;
    
    // Start is called before the first frame update
    void Start()
    {
        this._rigidbody = GetComponent<Rigidbody>();

        if (isClient && EmptyPlayer.LocalPlayer.isLocalPlayer)
        {
            InputManager.Instance.InGamePlayer = this;
        }

        if (isServer)
        {
            _speed = 3;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    
    [Command]
    public void CmdMovePlayer(Vector3 movementVector)
    {
        _rigidbody.AddForce(movementVector.normalized * _speed);
    }

}
