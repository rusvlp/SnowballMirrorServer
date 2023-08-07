using UnityEngine;

namespace App.Scripts
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configuration/Weapon", order = 1)]
    public class WeaponConfiguration : ScriptableObject
    {
        public float Speed = 10f;
        public float CoolDown = 2f;
        public int Capacity = 5;
        public GameObject GrenPrefab;
    }
}