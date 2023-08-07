using App.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class LeverScript : MonoBehaviour
{
    enum RotationAxis
    {
        x, y
    }

    enum LeverType
    {
        full, balance
    }

    [SerializeField] GXRInputManager _inputs;
    [SerializeField] RotationAxis _axisSelection;
    [SerializeField] LeverType _typeSelection;
    [SerializeField] float _maxAngle;
    [SerializeField] float _thresholdValue;

    public float Progress { get; private set; }


    public UnityEvent OnLeverPulled = new UnityEvent();
    public UnityEvent<float> OnLeverUpdate = new UnityEvent<float>();
    public UnityEvent OnLeverReleased = new UnityEvent();

    bool _isPulled = false;
    bool _isReleased = false;


    delegate float ProgressDelegate(float angle);
    ProgressDelegate CalcResult;

    Vector3 _rotAxis;
    Vector3 _rotAxisGlobal;
    int _rotAxisIndex;


    (Vector3, Vector3, int) SelectAxis(RotationAxis selected)
    {
        switch (selected)
        {
            case RotationAxis.x:
                return (this.transform.right, Vector3.right, 0);
            case RotationAxis.y:
                return (this.transform.up, Vector3.up, 1);
                /*case RotationAxis.z:
                    return (2, targetObj.forward);*/
        }
        return (this.transform.right, Vector3.right, 0);
    }

    ProgressDelegate SelectLeverType(LeverType selected)
    {
        switch (selected)
        {
            case LeverType.full:
                return CalculateFull;
            case LeverType.balance:
                return CalculateBalance;
        }
        return CalculateFull;
    }

    private void Start()
    {
        CalcResult = SelectLeverType(_typeSelection);
        (_rotAxis, _rotAxisGlobal, _rotAxisIndex) = SelectAxis(_axisSelection);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(Tags.Hand) && (_inputs.LeftHand.GripPressed || _inputs.RightHand.GripPressed))
        {
            LeverRotate(other.transform);
        }
    }

    void LeverRotate(Transform handPos)
    {
        Vector3 targetPos = handPos.transform.position;
        Vector3 leverPos = transform.position;

        Vector3 direction = Vector3.ProjectOnPlane(targetPos - leverPos, _rotAxis);

        transform.LookAt(leverPos + direction, _rotAxis);
        float angle = transform.localEulerAngles[_rotAxisIndex];
        if (angle > 180) angle -= 360;
        angle = Mathf.Clamp(angle, -_maxAngle, _maxAngle);
        transform.localEulerAngles = angle * _rotAxisGlobal;

        Progress = CalcResult.Invoke(angle);
        EventCaller();
    }

    void EventCaller()
    {
        OnLeverUpdate.Invoke(Progress);

        float absProgress = Mathf.Abs(Progress);

        if (absProgress <= _thresholdValue && !_isReleased)
        {
            OnLeverReleased.Invoke();
            _isReleased = true;

            Debug.Log("Released!");
        }
        if (absProgress > _thresholdValue && _isReleased)
        {
            _isReleased = false;
        }
        if (absProgress >= (1 - _thresholdValue) && !_isPulled)
        {
            OnLeverPulled.Invoke();
            _isPulled = true;

            Debug.Log("Pulled!");
        }
        if (absProgress < (1 - _thresholdValue) && _isPulled)
        {
            _isPulled = false;
        }
    }

    float CalculateFull(float angle)
    {
        return (angle + _maxAngle) / (_maxAngle * 2);
    }

    float CalculateBalance(float angle)
    {
        return angle / _maxAngle;
    }

    public void UpdateLogger(float leverProgress)
    {
        //Debug.Log(leverProgress);
    }
}
