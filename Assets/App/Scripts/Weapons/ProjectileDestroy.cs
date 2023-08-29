using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(ParticleSystem))]
    public class ProjectileDestroy : MonoBehaviour
    {
        [SerializeField] AudioSource AudioSource;
        [SerializeField] ParticleSystem ParticleSystem;
        [SerializeField] private bool noAudio = true;
        float duration;
        float curTime = 0;
        private void Start()
        {
            if (!noAudio)
            {
                duration = AudioSource.clip.length;
                AudioSource.Play();
                ParticleSystem.Play();
            }
       
        }

        private void FixedUpdate()
        {
            if (curTime < duration)
                curTime += Time.fixedDeltaTime;
            else Destroy(gameObject);
        }
    }
}

