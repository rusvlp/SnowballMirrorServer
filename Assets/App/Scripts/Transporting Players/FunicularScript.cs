using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FunicularScript : MonoBehaviour
{
    [SerializeField] Transform _body;
    [SerializeField] BezierTransport _relatedWay;

    [SerializeField] float _maxHeight;
    [SerializeField] float _liftSpeed;

    public bool _halfWay = false;
    public bool _isLifted = false;

    public void Activate(bool direction)
    {
        
        if(!_isLifted && !_halfWay)
        {
            Debug.Log("Button!");
            StartCoroutine(StartSystem(direction));
        }
            
    }

    private IEnumerator Lift()
    {
        if(_body.localPosition.y < _maxHeight)
        {
            Debug.Log("Ligting " + _body.localPosition.y + " " + _maxHeight);
            _body.position = _body.position + new Vector3(0, _liftSpeed * Time.deltaTime, 0);
            yield return new WaitForEndOfFrame();
        }
        else
        {
            Debug.Log("Lifted");
            StopCoroutine(Lift());
        }
    }

    private IEnumerator Lower()
    {
        if (_body.localPosition.z > 0)
        {
            _body.position -= new Vector3(0, _liftSpeed * Time.fixedDeltaTime, 0);
            yield return new WaitForEndOfFrame();
        }
        else
        {
            StopCoroutine(Lower());
        }
    }
    
    private IEnumerator StartSystem(bool direction)
    {
        if (!_halfWay)
        {
            if (!_isLifted)
            {
                StartCoroutine(Lift());
                Debug.Log("Button!");
                _isLifted = true;
                yield return Lift();
            }
            else
            {
                _relatedWay.Activate(direction);
                _halfWay = true;
                yield return _relatedWay.RouteTheCurve();
            }
        }
        else
        {
            StartCoroutine(Lower());
            _isLifted = false;
            _halfWay = false;
            StopCoroutine(StartSystem(direction));
        }
        /*
        if (_body.position.z < _maxHeight)
        {
            StartCoroutine(Lift());
        }
        else if (_body.position.z > _maxHeight)
        {
            if (!_halfWay)
            {
                _relatedWay.Activate(direction);
                _halfWay = true;
                yield return _relatedWay.RouteTheCurve();
            }
            else
            {
            }
        }
        */
    }
}
