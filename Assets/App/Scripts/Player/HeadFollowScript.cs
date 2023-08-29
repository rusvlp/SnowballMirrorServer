using System.Collections;
using System.Collections.Generic;
using Game.Sync;
using UnityEngine;
using UnityEngine.UIElements;

public class HeadFollowScript : MonoBehaviour, ILocalPlayerControl
{
    [SerializeField] Transform _headPos;
    [SerializeField] Transform _character;
    [SerializeField] Transform _characterHead;
    public bool IsClientAndOwned = false;
    private void Update()
    {
        if (IsClientAndOwned)
        {
            _character.SetPositionAndRotation(new Vector3(_headPos.position.x, _character.position.y, _headPos.position.z), 
                Quaternion.Euler(new Vector3(_character.eulerAngles.x, _headPos.eulerAngles.y, _character.eulerAngles.z)));
        }
        
    }

    public void SetIsClientAndOwned(bool value)
    {
        this.IsClientAndOwned = value;
    }
}
