using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    /*[SerializeField] Transform _startPoint;
    [SerializeField] Transform _endPoint;
    [SerializeField] GameObject _platform;
    [SerializeField] Transform _platformT;
    [SerializeField] public float _speed;
    [SerializeField] float _waitTime;
    [SerializeField] public float _curTime;
    [SerializeField] public Vector3 _direction;

    private void Awake()
    {
        _curTime = _waitTime;
        _direction = (_endPoint.position - _startPoint.position).normalized;
        _platformT = _platform.GetComponent<Transform>();
        if (_platformT.position != _startPoint.position) _platformT.position = _startPoint.position;
    }

    private void Update()
    {
        if(_curTime < 0)
        {
            if (_platformT.position.y > _endPoint.position.y)
            {
                _platformT.position = _endPoint.position;
                _direction = -_direction;
                _curTime = _waitTime;
            }
            if (_platformT.position.y < _startPoint.position.y)
            {
                _platformT.position = _startPoint.position;
                _direction = -_direction;
                _curTime = _waitTime;
            }
            _platformT.position += _direction * _speed * Time.deltaTime;
        }
        _curTime -= Time.deltaTime;
    }*/



    [SerializeField] List<Vector3> _points;
    [SerializeField] GameObject _platform;
    [SerializeField] Transform _platformT;
    [SerializeField] public float _speed;
    [SerializeField] float _waitTime;
    [SerializeField] public float _curTime;
    [SerializeField] public Vector3 _direction;

    private void Update()
    {
        
    }


}
