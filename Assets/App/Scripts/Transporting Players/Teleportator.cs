using App.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportator : MonoBehaviour
{
    [SerializeField] Transform _finalPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Player))
        {
            other.transform.position = _finalPoint.position;
        }
    }
}
