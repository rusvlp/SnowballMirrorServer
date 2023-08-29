using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class SyncHands : NetworkBehaviour
{
    [SerializeField] private HandScript _rightHandScript;
    [SerializeField] private HandScript _leftHandScript;


    private Transform _rightHandTransform;
    private Transform _leftHandTransform;
    
    [Header("Left Hand")]
    [SerializeField] [SyncVar] private Vector3 _leftHandPosition;
    [SerializeField] [SyncVar] private Quaternion _leftHandRotation;

    [Header("Right Hand")] 
    [SerializeField] [SyncVar] private Vector3 _rightHandPosition;
    [SerializeField] [SyncVar] private Quaternion _rightHandRotation;
    // Start is called before the first frame update
    void Start()
    {
        _rightHandTransform = _rightHandScript.transform;
        _leftHandTransform = _leftHandScript.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isClient && isOwned)
        {
            _rightHandPosition = _rightHandTransform.position;
            _rightHandRotation = _rightHandTransform.rotation;


            _leftHandPosition = _leftHandTransform.position;
            _leftHandRotation = _leftHandTransform.rotation;
        }
        else
        {
            _rightHandTransform.position = _rightHandPosition;
            _rightHandTransform.rotation = _rightHandRotation;


            _leftHandTransform.position = _leftHandPosition;
            _leftHandTransform.rotation = _leftHandRotation;
        }
    }
}
