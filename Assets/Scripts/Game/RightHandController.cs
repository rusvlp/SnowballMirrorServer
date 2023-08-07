using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class RightHandController : MonoBehaviour
{

    public InputActionProperty pinchAnimationAction;

    public InputActionProperty gripAnimationAction;
    
    [SerializeField] private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       float triggerValue = pinchAnimationAction.action.ReadValue<float>();
       _animator.SetFloat("Pinch", triggerValue);

       float gripValue = gripAnimationAction.action.ReadValue<float>();
       _animator.SetFloat("Flex", gripValue);
       
       print(triggerValue);
    }
}
