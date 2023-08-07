using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    [SerializeField] Transform _leftDoor;
    [SerializeField] Transform _rightDoor;
    [SerializeField] float _doorTime;
    [SerializeField] float _angle;

    bool isOpened;

    private void Start()
    {
        isOpened = false;
        RotateDoors(new Vector3(0, 0, _angle));
    }

    public void OpenDoors()
    {
        if (!isOpened)
        {
            RotateDoors(new Vector3(0, 0, _angle));
            isOpened = true;
        }
    }

    public void CloseDoors()
    {
        if (isOpened)
        {
            RotateDoors(new Vector3(0, 0, 0));
            isOpened = false;
        }
    }

    void RotateDoors(Vector3 angleVector)
    {
        _leftDoor.DOLocalRotate(angleVector, _doorTime);
        _rightDoor.DOLocalRotate(-angleVector, _doorTime);
    }

}

//IEnumerator RotateTo(Vector3 targetAngles)
//{
//    //_leftDoor.localEulerAngles = Vector3.Lerp(_leftDoor.localEulerAngles, targetAngles, _doorSpeed);
//    //_rightDoor.localEulerAngles = Vector3.Lerp(_rightDoor.localEulerAngles, -targetAngles, _doorSpeed);

//    _leftDoor.localRotation = Quaternion.Lerp(_leftDoor.localRotation, Quaternion.Euler(targetAngles), _doorTime * Time.deltaTime);
//    _rightDoor.localRotation = Quaternion.Lerp(_rightDoor.localRotation, Quaternion.Euler(-targetAngles), _doorTime * Time.deltaTime);
//    //if (_leftDoor.localEulerAngles.z - targetAngles.z < 5)
//    //{
//    //    Debug.Log("Корутина завершена");
//    //    StopCoroutine(RotateTo(targetAngles));
//    //}
//    yield return new WaitForEndOfFrame();
//}