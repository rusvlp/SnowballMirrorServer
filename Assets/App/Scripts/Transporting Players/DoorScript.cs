using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] Transform _door;
    [SerializeField] float _doorTime;
    [SerializeField] float _angle;

    bool isOpened;

    private void Start()
    {
        isOpened = false;
        OpenDoor();
    }

    public void OpenDoor()
    {
        if (!isOpened)
        {
            RotateDoor(new Vector3(0, 0, _angle));
            isOpened = true;
        }
    }

    public void CloseDoor()
    {
        if (isOpened)
        {
            RotateDoor(new Vector3(0, 0, 0));
            isOpened = false;
        }
    }

    void RotateDoor(Vector3 angleVector)
    {
        _door.DOLocalRotate(angleVector, _doorTime);
    }
}
