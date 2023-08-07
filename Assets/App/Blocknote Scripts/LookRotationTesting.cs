using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LookRotationTesting : MonoBehaviour
{
    public Transform obj1;
    public Transform obj2;

    private void Update()
    {
        Vector3 direction = Vector3.ProjectOnPlane(obj2.position - obj1.position, obj1.right).normalized;
        Debug.DrawLine(obj1.position, obj1.position + obj1.right, Color.blue, 10);
        Debug.DrawLine(obj1.position, obj1.position + direction, Color.red, 10);
        obj1.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }
}
