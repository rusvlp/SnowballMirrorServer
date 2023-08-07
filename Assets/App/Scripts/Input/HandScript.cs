using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class HandScript : MonoBehaviour
{
    enum HandRole
    {
        RightHand,
        LeftHand
    }

    [SerializeField] GXRInputManager _inputManager;
    [SerializeField] HandRole _handRole;

    public Vector2 StickValue;
    public float GripValue;
    public float TriggerValue;
    public bool GripPressed;
    public bool TriggerPressed;

    public UnityEvent OnGripPressed = new UnityEvent();
    public UnityEvent<float> OnGripUpdated = new UnityEvent<float>();
    public UnityEvent OnGripReleased = new UnityEvent();

    public UnityEvent OnTriggerPressed = new UnityEvent();
    public UnityEvent<float> OnTriggerUpdated = new UnityEvent<float>();
    public UnityEvent OnTriggerReleased = new UnityEvent();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StickMove(InputAction.CallbackContext obj)
    {
        StickValue = obj.ReadValue<Vector2>();
    }

    public void TriggerPress(InputAction.CallbackContext obj)
    {
    TriggerValue = obj.ReadValue<float>();
        if (TriggerValue > _inputManager.TriggerThreshold.x && TriggerValue < _inputManager.TriggerThreshold.y)
        {
            OnTriggerUpdated.Invoke(TriggerValue);
        }
        if (!TriggerPressed && TriggerValue > _inputManager.TriggerThreshold.y)
        {
            OnTriggerPressed.Invoke();
            TriggerPressed = true;
        }
        if (TriggerPressed && TriggerValue < _inputManager.TriggerThreshold.x)
        {
            OnTriggerReleased.Invoke();
            TriggerPressed = false;
        }
    }

    public void GripPress(InputAction.CallbackContext obj)
    {
        GripValue = obj.ReadValue<float>();
        if (GripValue > _inputManager.GripThreshold.x && GripValue < _inputManager.GripThreshold.y)
        {
            OnGripUpdated.Invoke(GripValue);
        }
        if (!GripPressed && GripValue > _inputManager.GripThreshold.y)
        {
            OnGripPressed.Invoke();
            GripPressed = true;
        }
        if (GripPressed && GripValue < _inputManager.GripThreshold.x)
        {
            OnGripReleased.Invoke();
            GripPressed = false;
        }
    }

    public bool isRight()
    {
        if (_handRole == HandRole.RightHand) return true;
        return false;
    }
}
