using UnityEngine;

namespace App.Scripts.Weapons
{
    public class TrajectoryRender : MonoBehaviour
    {
        [SerializeField] LineRenderer _lineRenderer;
        [SerializeField] int _maxFlightTime;
        [SerializeField] float _timecoef;

        public void DrawTrajectory(Vector3 fromPoint, Vector3 velocity)
        {
            Vector3[] points = new Vector3[_maxFlightTime];
            points[0] = fromPoint;
            for(int curTime = 1; curTime < _maxFlightTime; curTime++)
            {
                Vector3 cPoint;
                float time = curTime * _timecoef;
                points[curTime] = fromPoint + velocity * time + Physics.gravity * time * time / 2;
                if(CollisionCheck(points[curTime-1], points[curTime], out cPoint))
                {
                    points[curTime] = cPoint;
                    System.Array.Resize<Vector3>(ref points, curTime);
                    break;
                }
            }
            _lineRenderer.positionCount = points.Length;
            _lineRenderer.SetPositions(points);
        }

        public bool CollisionCheck(Vector3 firstPoint, Vector3 lastPoint, out Vector3 cPoint)
        {
            Vector3 direction = lastPoint - firstPoint;
            Physics.queriesHitTriggers = false;
            Physics.Raycast(firstPoint, direction, out RaycastHit hitInfo, direction.magnitude);
            if (hitInfo.collider == null)
            {
                cPoint = Vector3.zero;
                return false;
            }
            else
            {
                cPoint = hitInfo.point;
                return true;
            }
        }

        public void HideLine()
        {
            _lineRenderer.enabled = false;
        }

        public void ShowLine()
        {
            _lineRenderer.enabled = true;
        }
    }
}
