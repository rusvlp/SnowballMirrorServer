using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BezierTransport : MonoBehaviour
{
    [SerializeField] Transform _platform;
    [SerializeField] public BezierCurve[] _curves;
    [SerializeField] public bool IsInMove;
    [SerializeField] float _speed;
    [SerializeField] int _currentRoute;
    [SerializeField] bool _direction;
    [SerializeField] float _routeProgress;

    public UnityEvent OnTransportStart;
    public UnityEvent OnTransportStop;

    private void Start()
    {
        _currentRoute = 0;
        _routeProgress = 0;
    }
    public void Activate(bool direction)
    {
        if (!IsInMove && _direction != direction)
        {
            _direction = direction;
            StartCoroutine(RouteTheCurve());
        }
    }

    public static Vector3 AutoBezierPosition(float t, List<Transform> points)
    {
        if (points.Count == 4)
            return BezierPosition4(t, points);
        if (points.Count == 3)
            return BezierPosition3(t, points);
        if (points.Count == 2)
            return BezierPosition2(t, points);
        else
        {
            Debug.LogError("Несоответствие количества точек");
            throw new System.Exception("Несоответствие количества точек");
        }
    }

    public static Vector3 BezierPosition4(float t, List<Transform> points)
    {
        return Mathf.Pow(1 - t, 3) * points[0].position + 3 * Mathf.Pow(1 - t, 2) * t * points[1].position + 3 * (1 - t) * Mathf.Pow(t, 2) * points[2].position + Mathf.Pow(t, 3) * points[3].position;
    }

    public static Vector3 BezierPosition3(float t, List<Transform> points)
    {
        return Mathf.Pow(1 - t, 2) * points[0].position + 2 * t * (1 - t) * points[1].position + Mathf.Pow(t, 2) * points[2].position;
    }

    public static Vector3 BezierPosition2(float t, List<Transform> points)
    {
        return (1 - t) * points[0].position + t * points[1].position;
    }

    public IEnumerator RouteTheCurve()
    {
        OnTransportStart.Invoke();
        Debug.Log("Working!");
        IsInMove = true;
        if (_direction)
        {
            for(_currentRoute = 0; _currentRoute < _curves.Length; _currentRoute++)
            {
                List<Transform> points = _curves[_currentRoute].Points;
                while (_routeProgress < 1)
                {
                    _routeProgress += Time.deltaTime * _speed;

                    _platform.position = AutoBezierPosition(_routeProgress, points);
                    // I should change this to delegates for better perfomance
                    yield return new WaitForEndOfFrame();
                }
                _routeProgress = 0;
            }
            _routeProgress = 1;
            IsInMove = false;
            OnTransportStop.Invoke();
            StopCoroutine(RouteTheCurve());
        }
        else
        {
            for(_currentRoute = _curves.Length - 1; _currentRoute >= 0; _currentRoute--)
            {
                List<Transform> points = _curves[_currentRoute].Points;
                while (_routeProgress > 0)
                {
                    _routeProgress -= Time.deltaTime * _speed;

                    _platform.position = AutoBezierPosition(_routeProgress, points);
                    yield return new WaitForEndOfFrame();
                }
                _routeProgress = 1;
            }
            _routeProgress = 0;
            IsInMove = false;
            OnTransportStop.Invoke();
            StopCoroutine(RouteTheCurve());
        }
    }
}
