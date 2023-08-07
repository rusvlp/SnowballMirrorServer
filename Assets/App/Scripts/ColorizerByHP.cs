/*using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ColorizerByHP : MonoBehaviour
{
    [SerializeField] DamageAble _dmgble;
    [SerializeField] MeshRenderer _material;

    private void Awake()
    {
        _material.material.color = Color.HSVToRGB(0.277f, 1, 1);
        //_material.material.color = new Color(0, 1, 0, 1);
        _dmgble.OnDamageTaken += ColorChange;
    }
    public void ColorChange()
    {
        float diff = _dmgble.HitPoints / _dmgble.MaxHitPoints;
        _material.material.color = Color.HSVToRGB(diff*0.277f, 1, 1);
        Debug.Log(_material.material.color);
        //if (diff > 0.5) _material.material.color = new Color(1-diff, 1, 0, 1);
        //else _material.material.color = new Color(1, diff, 0, 1);
    }
}
*/
