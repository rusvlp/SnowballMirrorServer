using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpinePosition : MonoBehaviour
{
    [SerializeField] Transform _headPos;
    [SerializeField] Transform _floorPos;
    [SerializeField] float _maxHeight;
    [SerializeField] Animator _animator;

    private void Update()
    {
        _animator.SetFloat("Height", (_headPos.position.y - _floorPos.position.y)/_maxHeight);
    }
}
