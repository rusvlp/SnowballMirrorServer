using System;
using UnityEngine;

namespace App.Scripts.Weapons
{
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(ProjectileScipt))]
    public class Ball : MonoBehaviour
    {
        public MeshRenderer MeshRenderer { get; private set; }
        public ProjectileScipt ProjectileScipt { get; private set; }
        public Transform Transform { get; private set; }

        void Awake()
        {
            MeshRenderer = GetComponent<MeshRenderer>();
            ProjectileScipt = GetComponent<ProjectileScipt>();
            Transform = GetComponent<Transform>();
        }
    }
}