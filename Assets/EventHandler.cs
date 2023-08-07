using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public delegate void DoSomething();
    public event DoSomething HandleEvent;

    public void DoHandle()
    {
        HandleEvent();
    }
}
