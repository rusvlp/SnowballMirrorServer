using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIEditMatchPlayer : MonoBehaviour
{
    [SerializeField] private Image checkIcon;
    [SerializeField] private Image backgroundImage;
    
    public TMP_Text PlayerID;
    public UIPlayerStatus Status = UIPlayerStatus.Global;
    private Color originalColor;
    
    private EmptyPlayer _player = new EmptyPlayer
    {
        PlayerGlobalIndex = 1,
        PlayerMatchIndex = 1,
        Fingerprint = "Mock", 
        Name = "Mock"
    };

    public EmptyPlayer Player
    {
        get => _player;
        set
        {
            _player = value;
            PlayerID.text = value.PlayerGlobalIndex.ToString();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        originalColor = backgroundImage.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Check()
    {
        if (checkIcon.enabled)
        {
            if (Status == UIPlayerStatus.Global)
            {
                UISessionEditWindow.Instance.UiPlayersToMoveToSession.Remove(this);
            }
            else
            {
                UISessionEditWindow.Instance.UiPlayersToMoveToGlobal.Remove(this);
            }
            
            checkIcon.enabled = false;
        }
        else
        {
            if (Status == UIPlayerStatus.Global)
            {
                UISessionEditWindow.Instance.UiPlayersToMoveToSession.Add(this);
            }
            else
            {
                UISessionEditWindow.Instance.UiPlayersToMoveToGlobal.Add(this);
            }
            
            checkIcon.enabled = true;
        }
    }


    public void ChangeBackgroundColor(Color newColor)
    {
        backgroundImage.color = newColor;
    }

    public void ResetBackgroundColor()
    {
        backgroundImage.color = originalColor;
    }
}

public enum UIPlayerStatus
{
    Global, 
    InSession
}
