using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour
{
    public UnityEvent DoAction;
    private void OnTriggerEnter(Collider other)
    {
        DoAction.Invoke();
    }
}
