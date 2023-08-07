using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using App.Scripts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.OpenXR;
using App.Scripts.Controllers;
using App.Scripts.Weapons;

public class CatapultScript : MonoBehaviour
{
    [SerializeField] Animator _animator;

    [SerializeField] Transform _spoon;

    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] GameObject _projectileCurrent;
    [SerializeField] Transform _lavinParent;
    [SerializeField] Transform _lavinSpoonPosition;
    [SerializeField] Transform _forceDirection;


    [SerializeField] EventHandler _lavinHandler;
    public bool IsLavinReady;

    public float SpoonProgress;
    public bool IsSpoonReady;
    public bool IsLoadingReady;
    public bool IsLaunchReady;

    [SerializeField] float _loadingSpeed;
    [SerializeField] float _launchingSpeed;
    [SerializeField] float _forceModifier;

    private void Start()
    {
        //IsLavinReady = false;
        //IsLoadingReady = false;
        //IsLaunchReady = false;
        //Time.timeScale = 0.5f;
        _animator.SetFloat(AnimatorParams.LoadingSpeed, _loadingSpeed);
        _animator.SetFloat(AnimatorParams.LaunchingSpeed, _launchingSpeed);
    }

    public void ToLoadingState()
    {
        //Debug.Log("Загрузка");
        _animator.SetTrigger(AnimatorParams.LoadingTrigger);
        _animator.ResetTrigger(AnimatorParams.LaunchingTrigger);
    }

    public void ToLaunchState()
    {
        _animator.SetTrigger(AnimatorParams.LaunchingTrigger);
        _animator.ResetTrigger(AnimatorParams.LoadingTrigger);
        if (IsLavinReady != IsSpoonReady)
        {
            if(_projectileCurrent != null)
                _projectileCurrent.GetComponent<ProjectileScipt>().DestroyProjectile();
            IsLavinReady = false;
        }
    }

    public void ToDefaultState()
    {
        _animator.SetTrigger(AnimatorParams.DefaultTrigger);
        if(IsSpoonReady && IsLavinReady)
        {
            Vector3 lastPos = _projectileCurrent.transform.position;
            _projectileCurrent.transform.parent = null;
            _projectileCurrent.transform.position = lastPos;
            _projectileCurrent.GetComponent<ProjectileScipt>().Launch(_forceDirection.transform.forward * _forceModifier);
        }
        IsSpoonReady = false;
        IsLavinReady = false;
    }

    public void LavinStart()
    {
        _projectileCurrent = Instantiate(_projectilePrefab, _lavinParent);
        _lavinHandler = _projectileCurrent.GetComponent<EventHandler>();
        _lavinHandler.HandleEvent += SetLavinReady;
    }

    public void LavinCheck()
    {
    }

    public void SetLavinReady()
    {
        IsLavinReady = true;
        _projectileCurrent.GetComponent<Animator>().enabled = false;
        _lavinHandler.HandleEvent -= SetLavinReady;
    }

    public void SetSpoonReady()
    {
        IsSpoonReady = true;
        LavinToSpoon();
    }

    public void LavinToSpoon()
    {
        _projectileCurrent.transform.SetParent(_lavinSpoonPosition);
        _projectileCurrent.transform.localPosition = Vector3.zero;
    }

    public void LeverProcessing(float leverProgress)
    {
        Debug.Log(leverProgress);
        if(leverProgress > 0.5)
        {
            ToLoadingState();
        }
        else
        {
            ToLaunchState();
        }
    }
}

/*
 * public void Enter()
    {
        SetStatus(Status.Loading);
    }

    void SetStatus(Status status)
    {
        switch (status)
        {
            case Status.Pending:
                _status = Status.Pending;
                OnPending.Invoke();
                break;
            case Status.Loading:
                _status = Status.Loading;
                OnLoading.Invoke();
                break;
            case Status.Ready:
                _status = Status.Ready;
                OnReady.Invoke();
                break;
            case Status.Launching:
                _status = Status.Launching;
                OnLaunch.Invoke();
                break;
        }
    }

    public void LoadSpoon()
    {
        if (_spoon.localEulerAngles.y < _endAngle) 
            _spoon.transform.DOLocalRotate(new Vector3(0, _spoon.localEulerAngles.y + 5, 0), _rotationLatency * Time.deltaTime, RotateMode.Fast);
        Debug.Log(_spoon.localEulerAngles.y);
    }
*/

//enum Status
//{
//    Pending,
//    Loading,
//    Ready,
//    Launching
//}

//public UnityEvent OnLoading = new UnityEvent();
//public UnityEvent OnReady = new UnityEvent();
//public UnityEvent OnLaunch = new UnityEvent();