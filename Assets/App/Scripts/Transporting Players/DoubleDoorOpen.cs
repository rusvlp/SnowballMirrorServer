using App.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleDoorOpen : MonoBehaviour
{
    [SerializeField] Transform _door1;
    [SerializeField] Transform _door2;
    [SerializeField] float _doorOffset;
    [SerializeField] float _doorSpeed;

    [SerializeField] BezierTransport _transport;
    
    public bool _doorBlock;

    int _playersCount;
    Vector3 _door1Start;
    Vector3 _door2Start;
    Vector3 _door1Final;
    Vector3 _door2Final;

    [SerializeField] AudioSource _openDoorSound;
    [SerializeField] AudioSource _closeDoorSound;

    private void Start()
    {
        _playersCount = 0;
        _door1Start = _door1.localPosition;
        _door2Start = _door2.localPosition;
        _door1Final = _door1.localPosition + _door1.right * _doorOffset;
        _door2Final = _door2.localPosition - _door2.right * _doorOffset;
        _transport.OnTransportStart.AddListener(BlockToggle);
        _transport.OnTransportStop.AddListener(BlockToggle);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            _playersCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            _playersCount--;
        }
    }

    private void Update()
    {
        if( _playersCount > 0 && !_doorBlock)
        {
            _door1.localPosition = Vector3.Lerp(_door1.localPosition, _door1Final, _doorSpeed);
            _door2.localPosition = Vector3.Lerp(_door2.localPosition, _door2Final, _doorSpeed);
            _openDoorSound.Play();
        }
        else
        {
            _door1.localPosition = Vector3.Lerp(_door1.localPosition, _door1Start, _doorSpeed);
            _door2.localPosition = Vector3.Lerp(_door2.localPosition, _door2Start, _doorSpeed);
            _closeDoorSound.Play();
        }
    }

    private void BlockToggle()
    {
        _doorBlock = !_doorBlock;
    }
}
