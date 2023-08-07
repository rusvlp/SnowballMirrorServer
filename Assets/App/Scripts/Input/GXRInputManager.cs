using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GXRInputManager : MonoBehaviour
{
    public GenericXRController inputActions;

    public HandScript LeftHand;
    public HandScript RightHand;

    [SerializeField] public Vector2 GripThreshold = new Vector2(0.1f, 0.9f);
    [SerializeField] public Vector2 TriggerThreshold = new Vector2(0.1f, 0.9f);

    public UnityEvent OnLeftGripPressed = new UnityEvent();
    public UnityEvent<float> OnLeftGripUpdated = new UnityEvent<float>();
    public UnityEvent OnLeftGripReleased = new UnityEvent();

    public UnityEvent OnLeftTriggerPressed = new UnityEvent();
    public UnityEvent<float> OnLeftTriggerUpdated = new UnityEvent<float>();
    public UnityEvent OnLeftTriggerReleased = new UnityEvent();



    public UnityEvent OnRightGripPressed = new UnityEvent();
    public UnityEvent<float> OnRightGripUpdated = new UnityEvent<float>();
    public UnityEvent OnRightGripReleased = new UnityEvent();

    public UnityEvent OnRightTriggerPressed = new UnityEvent();
    public UnityEvent<float> OnRightTriggerUpdated = new UnityEvent<float>();
    public UnityEvent OnRightTriggerReleased = new UnityEvent();

    public UnityEvent OnAButtonPressed = new UnityEvent();

    [SerializeField] public bool AButtonPressed;
    private void Awake()
    {
        inputActions = new GenericXRController();
        inputActions.Enable();

        inputActions.LeftHand.StickValue.performed += LeftHand.StickMove;
        inputActions.RightHand.StickValue.performed += RightHand.StickMove;

        inputActions.RightHand.GripValue.performed += RightHand.GripPress;
        inputActions.RightHand.TriggerValue.performed += RightHand.TriggerPress;
        inputActions.LeftHand.GripValue.performed += LeftHand.GripPress;
        inputActions.LeftHand.TriggerValue.performed += LeftHand.TriggerPress;

        inputActions.RightHand.AButton.performed += AButtonPress;
    }

    private void AButtonPress(InputAction.CallbackContext obj)
    {
        AButtonPressed = obj.ReadValueAsButton();
        OnAButtonPressed.Invoke();
    }

    //private void LeftStickMove(InputAction.CallbackContext obj)
    //{
    //    LeftStickValue = obj.ReadValue<Vector2>();
    //}

    //private void RightStickMove(InputAction.CallbackContext obj)
    //{
    //    RightStickValue = obj.ReadValue<Vector2>();
    //}

    //private void LeftTriggerPress(InputAction.CallbackContext obj)
    //{
    //    LeftTriggerValue = obj.ReadValue<float>();
    //    if(LeftTriggerValue > TriggerThreshold.x && LeftTriggerValue < TriggerThreshold.y)
    //    {
    //        OnLeftTriggerUpdated.Invoke(LeftTriggerValue);
    //    }
    //    if(!LeftTriggerPressed && LeftTriggerValue > TriggerThreshold.y)
    //    {
    //        OnLeftTriggerPressed.Invoke();
    //        LeftTriggerPressed = true;
    //    }
    //    if(LeftTriggerPressed && LeftTriggerValue < TriggerThreshold.x)
    //    {
    //        OnLeftTriggerReleased.Invoke();
    //        LeftTriggerPressed = false;
    //    }

    //}

    //private void LeftGripPress(InputAction.CallbackContext obj)
    //{
    //    LeftGripValue = obj.ReadValue<float>();
    //    if (LeftGripValue > GripThreshold.x && LeftGripValue < GripThreshold.y)
    //    {
    //        OnLeftGripUpdated.Invoke(LeftGripValue);
    //    }
    //    if (!LeftGripPressed && LeftGripValue > GripThreshold.y)
    //    {
    //        OnLeftGripPressed.Invoke();
    //        LeftGripPressed = true;
    //    }
    //    if (LeftGripPressed && LeftGripValue < GripThreshold.x)
    //    {
    //        OnLeftGripReleased.Invoke();
    //        LeftGripPressed = false;
    //    }
    //}

    //private void RightTriggerPress(InputAction.CallbackContext obj)
    //{
    //    RightTriggerValue = obj.ReadValue<float>();
    //    if (RightTriggerValue > TriggerThreshold.x && RightTriggerValue < TriggerThreshold.y)
    //    {
    //        OnRightTriggerUpdated.Invoke(RightTriggerValue);
    //    }
    //    if (!RightTriggerPressed && RightTriggerValue > TriggerThreshold.y)
    //    {
    //        OnRightTriggerPressed.Invoke();
    //        RightTriggerPressed = true;
    //    }
    //    if (RightTriggerPressed && RightTriggerValue < TriggerThreshold.x)
    //    {
    //        OnRightTriggerReleased.Invoke();
    //        RightTriggerPressed = false;
    //    }
    //}

    //private void RightGripPress(InputAction.CallbackContext obj)
    //{
    //    RightGripValue = obj.ReadValue<float>();
    //    if (RightGripValue > GripThreshold.x && RightGripValue < GripThreshold.y)
    //    {
    //        OnRightGripUpdated.Invoke(RightGripValue);
    //    }
    //    if (!RightGripPressed && RightGripValue > GripThreshold.y)
    //    {
    //        OnRightGripPressed.Invoke();
    //        RightGripPressed = true;
    //    }
    //    if (RightGripPressed && RightGripValue < GripThreshold.x)
    //    {
    //        OnRightGripReleased.Invoke();
    //        RightGripPressed = false;
    //    }
    //}



    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void OnDestroy()
    {
        inputActions.Dispose();
    }
}
