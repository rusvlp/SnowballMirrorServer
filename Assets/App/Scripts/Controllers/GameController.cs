using System;
using UnityEngine;

namespace App.Scripts.Controllers
{
    public class GameController : MonoBehaviour
    {
        public static GameController Instance { get; private set; }
        public Configuration Configuration;
        public SceneConfiguration SceneConfiguration;
        void Awake()
        {
            if (Instance == null) 
                Instance = this;
            else 
                Destroy(this);
        }
    }
}