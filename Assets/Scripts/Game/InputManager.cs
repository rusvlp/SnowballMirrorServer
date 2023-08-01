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
        Vector3 actualVector3 = new Vector3();

        actualVector3.x = Input.GetAxis("Horizontal");
        actualVector3.z = Input.GetAxis("Vertical");

        if ((actualVector3.x != _movementVector.x) || (actualVector3.z != _movementVector.z))
        {
            print(actualVector3);
            _movementVector = actualVector3;
        }
        
        InGamePlayer.CmdMovePlayer(this._movementVector);
    }
}
