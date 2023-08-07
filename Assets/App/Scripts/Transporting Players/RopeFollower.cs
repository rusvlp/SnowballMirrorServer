using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RopeFollower : MonoBehaviour
{
    [SerializeField] Transform _connectedTo;
    [SerializeField] BezierTransport _transport;

    private void Start()
    {
        _transport.OnTransportStart.AddListener(StartFollow);
        _transport.OnTransportStop.AddListener(StopFollow);
    }
    private IEnumerator Follow()
    {
        while (true)
        {
            transform.position = new Vector3(_connectedTo.position.x, this.transform.position.y, _connectedTo.position.z);
            yield return null;
        }
    }
    private void StartFollow()
    {
        StartCoroutine(Follow());
    }

    private void StopFollow()
    {
        StopCoroutine(Follow());
    }
}
