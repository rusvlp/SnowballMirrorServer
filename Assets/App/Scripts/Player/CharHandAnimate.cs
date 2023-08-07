using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharHandAnimate : MonoBehaviour
{
    [SerializeField] Animator _handAnimator;
    [SerializeField] HandScript _inputHand;

    private void Awake()
    {
        _inputHand.OnGripUpdated.AddListener(GripUpdate);
        _inputHand.OnGripPressed.AddListener(GripPress);
        _inputHand.OnGripReleased.AddListener(GripRelease);
    }
    void GripUpdate(float gripProgress)
    {
        _handAnimator.SetFloat("AnimProgress", gripProgress);
    }

    void GripPress()
    {
        _handAnimator.SetFloat("AnimProgress", 1);
    }

    void GripRelease()
    {
        _handAnimator.SetFloat("AnimProgress", 0);
    }
}