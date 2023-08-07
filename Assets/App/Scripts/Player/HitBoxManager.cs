using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxManager : MonoBehaviour
{
    [SerializeField] Transform _floorT;
    [SerializeField] Transform _cameraT;
    [SerializeField] CapsuleCollider _hitbox;
    [SerializeField] float _heightAddition;

    private void FixedUpdate()
    {
        float ySize = _cameraT.position.y - _floorT.position.y + _heightAddition;
        _hitbox.center = new Vector3(0, (ySize)/2, 0);
        _hitbox.height = ySize;
    }
}
