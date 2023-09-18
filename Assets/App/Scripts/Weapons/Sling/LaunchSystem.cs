//using App.Scripts.Controllers;
//using UnityEngine;

//namespace App.Scripts.Weapons
//{
//    public class LaunchSystem : MonoBehaviour
//    {
//        [HideInInspector] public bool LaunchPhase;

//        [SerializeField] HandColliderCheck _handCheck;
//        [SerializeField] TrajectoryRender _trajRender;
//        [SerializeField] GameObject _ballPrefab;
//        [SerializeField] Transform _anchor;
//        [SerializeField] GXRInputManager _inputs;
//        [SerializeField] RubberRenderer _leftRubber;
//        [SerializeField] RubberRenderer _rightRubber;

//        public SlingshotConfiguration _slingshotConfiguration;
//        public SceneConfiguration _sceneConfiguration;
//        Ball _snowBall;

//        Vector3 _direction;
//        void Awake()
//        {
//            _slingshotConfiguration = GameController.Instance.Configuration.SlingshotConfiguration;
//            _sceneConfiguration = GameController.Instance.SceneConfiguration;
//            BallInit();
//        }

//        void BallInit()
//        {
//            _snowBall = Instantiate(_ballPrefab, _anchor.position, Quaternion.identity, _sceneConfiguration.BulletsParent).GetComponent<Ball>();
//            _snowBall.MeshRenderer.material = _slingshotConfiguration.GhostMaterial;
//        }

//        void Update()
//        {
//            if (!LaunchPhase)
//            {
//                _snowBall.Transform.position = _anchor.position;
//                _trajRender.HideLine();
//            }
//            else _trajRender.ShowLine();

//            if (_inputs.RightTriggerPressed)
//            {
//                if(_handCheck.IsInCollider)
//                {
//                    _snowBall.MeshRenderer.material = _slingshotConfiguration.SnowMaterial;
//                    _leftRubber.SetRubber(_snowBall.Transform);
//                    _rightRubber.SetRubber(_snowBall.Transform);
//                    LaunchPhase = true;
//                }

//                if(LaunchPhase)
//                {
//                    _direction = _handCheck.Hand.position - _anchor.position;
//                    if(_direction.magnitude < _slingshotConfiguration.MaxStretch) _snowBall.Transform.position = _handCheck.Hand.position;
//                    else _snowBall.Transform.position = _anchor.position + _direction.normalized * _slingshotConfiguration.MaxStretch;
//                    //_trajRender.ShowLine();
//                    _trajRender.DrawTrajectory(_snowBall.Transform.position, -_direction*_slingshotConfiguration.Power);
//                }
//            }
//            else 
//            {
//                if (_handCheck.IsInCollider)
//                {
//                    if(LaunchPhase)
//                    {
//                        //_trajRender.HideLine();
//                        _snowBall.Transform.position = _anchor.position;
//                        _snowBall.MeshRenderer.material = _slingshotConfiguration.GhostMaterial;
//                        LaunchPhase = false;
//                    }
//                }
//                else
//                {
//                    if (LaunchPhase)
//                    {
//                        Debug.Log("Phase!");
//                        //_trajRender.HideLine();
//                        _snowBall.ProjectileScipt.Launch(-_direction * _slingshotConfiguration.Power);
//                        LaunchPhase = false;
//                        BallInit();
//                    }
//                }
//            }
//        }
//    }
//}

using App.Scripts.Controllers;
using Mirror;
using UnityEngine;

namespace App.Scripts.Weapons
{
    public class LaunchSystem : MonoBehaviour
    {
        [HideInInspector] public bool LaunchPhase;

        [SerializeField] HandColliderCheck _handCheck;
        [SerializeField] TrajectoryRender _trajRender;
        [SerializeField] GameObject _ballPrefab;
        [SerializeField] Transform _anchor;
        public GXRInputManager Inputs;
        [SerializeField] RubberRenderer _leftRubber;
        [SerializeField] RubberRenderer _rightRubber;

        public SlingshotConfiguration _slingshotConfiguration;
        public SceneConfiguration _sceneConfiguration;
        Ball _snowBall;

        Vector3 _direction;
        void Awake()
        {
            _slingshotConfiguration = GameController.Instance.Configuration.SlingshotConfiguration;
            _sceneConfiguration = GameController.Instance.SceneConfiguration;
            BallInit();
        }

        void BallInit()
        {
            print("Ball initialized");
            _snowBall = Instantiate(_ballPrefab, _anchor.position, Quaternion.identity, _sceneConfiguration.BulletsParent).GetComponent<Ball>();
            _snowBall.MeshRenderer.material = _slingshotConfiguration.GhostMaterial;
        }

        void Update()
        {
            if (!LaunchPhase)
            {
                _snowBall.Transform.position = _anchor.position;
                _trajRender.HideLine();
            }
            else _trajRender.ShowLine();

            if (Inputs.RightHand.GripPressed)
            {
                if (_handCheck.IsInCollider)
                {
                    _snowBall.MeshRenderer.material = _slingshotConfiguration.SnowMaterial;
                    _leftRubber.SetRubber(_snowBall.Transform);
                    _rightRubber.SetRubber(_snowBall.Transform);
                    LaunchPhase = true;
                }

                if (LaunchPhase)
                {
                    _direction = _handCheck.Hand.position - _anchor.position;
                    if (_direction.magnitude < _slingshotConfiguration.MaxStretch) _snowBall.Transform.position = _handCheck.Hand.position;
                    else _snowBall.Transform.position = _anchor.position + _direction.normalized * _slingshotConfiguration.MaxStretch;
                    //_trajRender.ShowLine();
                    _trajRender.DrawTrajectory(_snowBall.Transform.position, -_direction * _slingshotConfiguration.Power);
                }
            }
            else
            {
                if (_handCheck.IsInCollider)
                {
                    if (LaunchPhase)
                    {
                        //_trajRender.HideLine();
                        _snowBall.Transform.position = _anchor.position;
                        _snowBall.MeshRenderer.material = _slingshotConfiguration.GhostMaterial;
                        LaunchPhase = false;
                    }
                }
                else
                {
                    if (LaunchPhase)
                    {
                        /*
                         * Где-то в этом месте должен быть код для спавна снежка по сети
                         */
                        
                        
                        Debug.Log("Phase!");
                        //_trajRender.HideLine();
                        _snowBall.ProjectileScipt.Launch(-_direction * _slingshotConfiguration.Power);
                        LaunchPhase = false;
                        BallInit();
                    }
                }
            }
        }
    }
}