using UnityEngine;

namespace App.Scripts
{
    [CreateAssetMenu(fileName = "SlingshotConfig", menuName = "Configuration/Slingshot", order = 0)]
    public class SlingshotConfiguration : ScriptableObject
    {
        public Material GhostMaterial;
        public Material SnowMaterial;
        public float MaxStretch = 0.3f;
        public float Power = 30f;
    }
}