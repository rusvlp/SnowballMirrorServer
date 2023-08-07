using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts
{
    [CreateAssetMenu(fileName = "ProjectileConfiguration", menuName = "Configuration/Projectile", order = 2)]
    public class ProjectileConfiguration : ScriptableObject
    {
        public GameObject DestroyReplacement;
        public float MaxFlightTime;
        public float Damage;
    }
}

