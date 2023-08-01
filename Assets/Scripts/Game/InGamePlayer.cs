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

    [SerializeField] private Transform _uiCanvas;
    [SerializeField] private GameObject _uiCanvasPrefab;
    
    [SerializeField] private float _nameYDistance;
    
    public Transform Camera;
    
    
    
    public static Action CmdMovePlayerCalled;
    
    // Start is called before the first frame update
    void Start()
    {
        InstantiatePlayerName();
        
        if (isClient)
        {
            this.Camera = CameraController.LocalCameraController.Camera;
            
            if (isOwned)
            {
                InputManager.Instance.InGamePlayer = this;
            }
            
        }

        if (isServer)
        {
            _speed = 5;
        } 
    }

    // Update is called once per frame
    void Update()
    {
        _uiCanvas.LookAt(Camera);

        _uiCanvas.position = transform.position;
    }

    public void InstantiatePlayerName()
    {
        this._rigidbody = GetComponent<Rigidbody>();

        Quaternion playerNameRotation = new Quaternion(0, 0, 0, 0);
        Vector3 playerNamePosition = new Vector3(transform.position.x, transform.position.y + _nameYDistance, transform.position.z);

        _uiCanvas = Instantiate(_uiCanvasPrefab, playerNamePosition, playerNameRotation).transform;
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
