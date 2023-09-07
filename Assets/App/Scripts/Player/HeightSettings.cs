using System.Collections;
using System.Collections.Generic;
using Game.Sync;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class HeightSettings : MonoBehaviour, ILocalPlayerControl
{
    [SerializeField] Animator _animator;
    [SerializeField] Transform _character;
    [SerializeField] float _baseHeight;
    
     public float Height;

    [SerializeField] Transform _head;
    [SerializeField] Transform _floor;

    [SerializeField] GXRInputManager _controller;


    [SerializeField] private TMP_Text _debugTMP;

    public bool IsClientAndOwned = false;
    private void Start()
    {
        _controller.OnAButtonPressed.AddListener(SetHeight);
    }

    private void Update()
    {

        if (IsClientAndOwned)
        {
            Height = _head.position.y - _floor.position.y;
            // _debugTMP.text = $"{_height}\n{_height/_baseHeight}";
            _animator.SetFloat("Height", Height/_baseHeight);
        }
        SetHeight();
    }

    private void SetHeight()
    {
        _character.localScale = (Height / _baseHeight) * new Vector3(1, 1, 1);
    }

    public void SetIsClientAndOwned(bool value)
    {
        this.IsClientAndOwned = value;
    }
}
