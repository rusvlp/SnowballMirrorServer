using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DummyScript : MonoBehaviour
{
    [SerializeField] Transform hp;

    public void ChangeColor(float health)
    {
        hp.localScale = new Vector3(health, 0.13f, 0.13f);
        if(health == 0)
        {
            Destroy(gameObject);
        }
    }
}
