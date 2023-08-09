using System.Collections;
using System.Collections.Generic;
using TMPro;
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


    [SerializeField] private TMP_Text _debugTMP;
    private void Start()
    {
        _controller.OnAButtonPressed.AddListener(SetHeight);
    }

    private void Update()
    {
        
        
        _height = _head.position.y - _floor.position.y;
        _debugTMP.text = $"{_height}\n{_height/_baseHeight}";
        _animator.SetFloat("Height", _height/_baseHeight);
        SetHeight();
    }

    private void SetHeight()
    {
        _character.localScale = (_height / _baseHeight) * new Vector3(1, 1, 1);
    }
}
