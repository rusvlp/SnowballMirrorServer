using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEditMatchPlayer : MonoBehaviour
{
    [SerializeField] private Image checkIcon;

    public TMP_Text PlayerID;

    private EmptyPlayer _player;

    public EmptyPlayer Player
    {
        get => _player;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Check()
    {
        if (checkIcon.enabled)
        {
            checkIcon.enabled = false;
        }
        else
        {
            checkIcon.enabled = true;
        }
    }
    
}
