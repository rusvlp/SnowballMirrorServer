using App.Scripts.Controllers;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace App.Scripts.Weapons
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(BoxCollider))]
    [RequireComponent(typeof(XRGrabInteractable))]
    public class WeaponSystem : MonoBehaviour
    {
        [HideInInspector] int _currentShot;

        [SerializeField] HandColliderCheck _handCheck;
        [SerializeField] GXRInputManager _inputManager;
        [SerializeField] TrajectoryRender _trajRender;
        [SerializeField] Transform _blowPoint;

        WeaponConfiguration _weaponConfiguration;
        SceneConfiguration _sceneConfiguration;
        Ball _gren;

        void Start()
        {
            _currentShot = 0;
            _inputManager.OnRightTriggerPressed.AddListener(OnShot);
            _sceneConfiguration = GameController.Instance.SceneConfiguration;
            _weaponConfiguration = GameController.Instance.Configuration.WeaponConfiguration;
        }

        void GrenInit()
        {
            Debug.Log($"_weaponConfiguration.GrenPrefab {_weaponConfiguration.GrenPrefab}");
            Debug.Log($"_sceneConfiguration.BulletsParent {_sceneConfiguration.BulletsParent}");
            //Debug.Log($"_weaponConfiguration.GrenPrefab {_weaponConfiguration.GrenPrefab}");
            _gren = Instantiate(_weaponConfiguration.GrenPrefab, _blowPoint.position, Quaternion.identity, _sceneConfiguration.BulletsParent).GetComponent<Ball>();
            _gren.GetComponent<ProjectileScipt>().Launch(_blowPoint.forward * _weaponConfiguration.Speed);
        }

        void OnShot()
        {
            if (_handCheck.IsInCollider)
            {
                GrenInit();
                if (_currentShot > _weaponConfiguration.Capacity) Destroy(this.gameObject);
                _currentShot++;
            }
        }

        void OnDestroy()
        {
            _inputManager.OnRightTriggerPressed.RemoveListener(OnShot);
        }
        void Update()
        {
            if (_handCheck.IsInCollider && _inputManager.RightHand.GripPressed)
            {
                _trajRender.ShowLine();
                _trajRender.DrawTrajectory(_blowPoint.transform.position, _blowPoint.transform.forward * _weaponConfiguration.Speed);
            }
            else _trajRender.HideLine();
        }
    }
}
