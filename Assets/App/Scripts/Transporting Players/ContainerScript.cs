using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerScript : MonoBehaviour
{
    [SerializeField] UIManager _UIManager;
    [SerializeField] BezierTransport _bezTransport;
    [SerializeField] List<Transform> _players;
    private Vector3 _movedOn;
    private Vector3 _prevPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!_bezTransport.IsInMove)
            {
                if (!_players.Contains(other.transform))
                    _players.Add(other.transform);
            }
            _UIManager.ZoneOutDisable();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!_bezTransport.IsInMove)
                _players.Remove(other.gameObject.transform);
            else if (!_players.Contains(other.transform)) 
                _UIManager.ZoneOutEnable();
        }
    }

    private void Awake()
    {
        _players = new List<Transform>();
        _prevPosition = this.transform.position;
    }

    private void Update()
    {
        _movedOn = this.transform.position - _prevPosition;
        _prevPosition = this.transform.position;
        foreach (Transform t in _players)
        {
            t.transform.position += _movedOn;
        }
    }
}
