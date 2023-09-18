using App.Scripts.Controllers;
using Mirror;
using UnityEngine;

namespace App.Scripts.Weapons
{
    public class ProjectileScipt : NetworkBehaviour
    {
        [SerializeField] bool IsLaunched = false;
        [SerializeField] public Vector3 Velocity;
        [SerializeField] Vector3 _fromPosition;
        [SerializeField] float _timeModifier;

        ProjectileConfiguration _projectileConfiguration;
        Vector3 _prevPosition;
        float _curFlightTime = 0;

        // Update is called once per frame

        void Awake()
        {
            _projectileConfiguration = GameController.Instance.Configuration.ProjectileConfiguration;
            if (isServer)
            {
                netIdentity.AssignClientAuthority(connectionToClient);
            }
            
        }
        void Update()
        {
            if (IsLaunched)
            {
                _curFlightTime += Time.deltaTime * _timeModifier;
                if (_curFlightTime >= _projectileConfiguration.MaxFlightTime) Destroy(gameObject);
                this.transform.position = _fromPosition + Velocity * _curFlightTime + Physics.gravity * _curFlightTime * _curFlightTime / 2;
                if (CollisionCheck(_prevPosition, this.transform.position, out RaycastHit hInfo))
                {
                    if (IsLaunched)
                    {
                        if(hInfo.collider.CompareTag(Tags.Player))
                            hInfo.collider.gameObject.GetComponent<PlayerStat>().GetDamage(_projectileConfiguration.Damage);
                        DestroyProjectile();
                    }
                }
            }
        }

        public void DestroyProjectile()
        {
            Instantiate(_projectileConfiguration.DestroyReplacement, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        void FixedUpdate()
        {
            if (IsLaunched)
            {
                if (this.transform.position != _prevPosition)
                {
                    this.transform.rotation = Quaternion.LookRotation(this.transform.position - _prevPosition);
                    _prevPosition = this.transform.position;
                }
            }
        }

        public void Launch(Vector3 velocity)
        {
           
            IsLaunched = true;
            _fromPosition = this.transform.position;
            _prevPosition = _fromPosition;
            this.Velocity = velocity;
            
            print("projectile is launched");
            CmdNetworkSpawn();
            CmdLaunch(velocity);
        }

        [Command]
        public void CmdNetworkSpawn(NetworkConnectionToClient conn = null)
        {
            print($"Spawned a snowball, issuer connection: {conn}");
            NetworkServer.Spawn(this.gameObject, conn);
        }
        
        [Command]
        public void CmdLaunch(Vector3 velocity)
        {
            IsLaunched = true;
            _fromPosition = this.transform.position;
            _prevPosition = _fromPosition;
            this.Velocity = velocity;
            
            print("projectile is launched");
        }
        
        /*private void OnTriggerEnter(Collider other)
    {
        if (IsLaunched && other.gameObject.tag != "Weapon")
        {
            try
            {
                other.GetComponent<DamageAble>().getDamage(_damage);
            }
            catch (System.Exception)
            {
            }
            Destroy(gameObject);
        }
    }*/

        public bool CollisionCheck(Vector3 firstPoint, Vector3 lastPoint, out RaycastHit hInfo)
        {
            Vector3 direction = lastPoint - firstPoint;
            int layer = 1 << LayerMask.NameToLayer("Default");
            Physics.queriesHitTriggers = false;
            Physics.Raycast(firstPoint, direction, out RaycastHit hitInfo, direction.magnitude);
            hInfo = hitInfo;
            if (hitInfo.collider == null) return false;
            else return true;
        }

        /*
    private IEnumerator PlayOnDestroy()
    {
        _sound.Play();
        yield return new WaitForSeconds(_sound.clip.length);
        Destroy(gameObject);
    }*/
    }
}
