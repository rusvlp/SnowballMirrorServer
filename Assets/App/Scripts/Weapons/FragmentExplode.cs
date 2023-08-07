using UnityEngine;

namespace App.Scripts.Weapons
{
    public class FragmentExplode : MonoBehaviour
    {
        //[SerializeField] Transform _bullets;
        [SerializeField] GameObject _fragmentPrefab;
        [SerializeField] GameObject _curFragment;
        [SerializeField] int _partsCount;
        [SerializeField] float _height;
        [SerializeField] float _radExplode;
        //Configs are not required now

        void Explode()
        {
            for(int part = 0; part < _partsCount; part++)
            {
                float deg = (float) part / _partsCount * 2 * Mathf.PI;
                _curFragment = Instantiate(_fragmentPrefab, this.transform.position, Quaternion.identity/*, _bullets*/);
                _curFragment.GetComponent<ProjectileScipt>().Launch(new Vector3(_radExplode * Mathf.Sin(deg), _height, _radExplode * Mathf.Cos(deg)));
            }
        }

        //Mathf.Sin((part/_partsCount)*360) * 100, _height, Mathf.Cos((part / _partsCount) * 360)*100

        void OnDestroy()
        {
            Explode();
        }
    }
}
