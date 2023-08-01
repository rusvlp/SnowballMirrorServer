using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController LocalCameraController;

    public Transform Camera;
    
    void Start()
    {
        LocalCameraController = this;
        this.Camera = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
