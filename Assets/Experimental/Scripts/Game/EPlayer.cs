using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class EPlayer : NetworkBehaviour
{
    [SyncVar] [SerializeField] 
    private float _speed;
    
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        if (isLocalPlayer && isClient)
        {
            EInputManager.Instance.LocalPlayer = this;
        }

        if (isServer)
        {
            _speed = 3;
        }
    }
    
     

    [Command]
    public void CmdMovePlayer(Vector3 movementVector)
    {
        _rigidbody.AddForce(movementVector.normalized * _speed);
    }
    
    // Start is called before the first frame update
  

    // Update is called once per frame
    void Update()
    {
        
    }
}
