using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToggleButtonScript : MonoBehaviour
{
    public bool toggle;
    public UnityEvent<bool> DoAction;

    [SerializeField] Animation _anim;

    private void Start()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        _anim.Play();
        DoAction.Invoke(toggle);
    }

    public void ToggleButton()
    {
        toggle = !toggle;
    }
}
