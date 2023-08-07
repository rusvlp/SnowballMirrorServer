using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAnimation : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] Transform _character;
    [SerializeField] float _modifier;
    [SerializeField] float _threshold;

    private Vector3 _prevPos;
    private float _prevDist;

    private void Awake()
    {
        _prevPos = _character.localPosition;
    }

    private void Update()
    {
        float dist = (_character.localPosition - _prevPos).magnitude * _modifier;
        float interpolate = Mathf.Clamp(Mathf.Clamp(dist, _prevDist - _threshold, _prevDist + _threshold), 0, 1);
        _animator.SetFloat("Move", interpolate);
        _prevDist = interpolate;
        _prevPos = _character.localPosition;
    }
}
