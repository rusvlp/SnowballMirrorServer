using UnityEngine;

namespace App.Scripts.Weapons
{
    public class RubberRenderer : MonoBehaviour
    {
        [SerializeField] LineRenderer _rubber;
        Transform _snowBallTransform;
        Transform _transform;
        [SerializeField] LaunchSystem _launchSys;

        void Start()
        {
            _rubber.positionCount = 2;
            _transform = transform;
        }

        void Update()
        {
            if(_launchSys.LaunchPhase && !_rubber.enabled)
            {
                _rubber.enabled = true;
            }
            if (_launchSys.LaunchPhase)
            {
                _rubber.SetPosition(0, _transform.position);
                _rubber.SetPosition(1, _snowBallTransform.position);
            }
            else _rubber.enabled = false;
        }
        
        public void SetRubber(Transform ballTransform)
        {
            _snowBallTransform = ballTransform;
        }
    }
}

