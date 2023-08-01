using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class InputManager : NetworkBehaviour
{
    [SerializeField] public InGamePlayer InGamePlayer;

    [SerializeField] private Vector3 _movementVector;
    // Start is called before the first frame update

    public static InputManager Instance;

    void Awake()
    {
        Instance = this;
    }
    
    
    void Start()
    {
      
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (InGamePlayer != null)
        {
            MoveInput();
        }
       
    }

    private void MoveInput()
    {
        _movementVector.x = Input.GetAxis("Horizontal");
        _movementVector.z = Input.GetAxis("Vertical");
        InGamePlayer.CmdMovePlayer(this._movementVector);
    }
}
