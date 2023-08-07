using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightSettings : MonoBehaviour
{
    [SerializeField] Animator _animator;
    [SerializeField] Transform _character;
    [SerializeField] float _baseHeight;
    public float _height;

    [SerializeField] Transform _head;
    [SerializeField] Transform _floor;

    [SerializeField] GXRInputManager _controller;

    private void Start()
    {
        _controller.OnAButtonPressed.AddListener(SetHeight);
    }

    private void Update()
    {
        _height = _head.position.y - _floor.position.y;
        _animator.SetFloat("Height", _height/_baseHeight);
    }

    private void SetHeight()
    {
        _character.localScale = (_height / _baseHeight) * new Vector3(1, 1, 1);
    }
}
