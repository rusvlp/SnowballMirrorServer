using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDelegateTest : MonoBehaviour
{
    public delegate void TestDelegate(bool toggle);
    public event TestDelegate testDelegate;

    private void Start()
    {
        testDelegate += toggler;
    }

    public void toggler(bool t)
    {

    }
}
