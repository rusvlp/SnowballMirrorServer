using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EInputManager : MonoBehaviour
{

    public EPlayer LocalPlayer;
    [SerializeField]
    private Vector3 _movementVector;


    private static EInputManager _instance;

    public static EInputManager Instance
    {
        get => _instance;
        private set
        {
            _instance = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
    }


    private void Awake()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (LocalPlayer != null)
        {
            MoveInput();
        }
       
    }

    private void MoveInput()
    {
        _movementVector.x = Input.GetAxis("Horizontal");
        _movementVector.z = Input.GetAxis("Vertical");
        LocalPlayer.CmdMovePlayer(this._movementVector);
    }
}
