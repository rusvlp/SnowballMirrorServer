using UnityEngine;

namespace App.Scripts.Weapons
{
    public class HandColliderCheck : MonoBehaviour
    {
        public Transform Hand;
        public bool IsInCollider = false;

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.Hand))
            {
                var handScript = other.GetComponent<HandScript>();
                if (handScript.isRight())
                {
                    IsInCollider = true;
                    Hand = other.gameObject.transform;
                }
            }
        }

        void OnTriggerExit(Collider other)
        {
            if(other.gameObject.CompareTag(Tags.Hand))
            {
                IsInCollider = false;
            }
        }
    }
}
