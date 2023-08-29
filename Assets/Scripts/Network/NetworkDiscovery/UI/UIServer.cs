using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIServer : MonoBehaviour
{
    [Header("OS Logos")]
    [SerializeField] private Sprite _windowsLogo;
    [SerializeField] private Sprite _androidLogo;
    [SerializeField] private Sprite _appleLogo;

    [Header("Server information")] 
    [SerializeField] private TMP_Text _serverIdTmp;
    [SerializeField] private TMP_Text _serverIpTmp;
    
    [Header("Misc")]
    [SerializeField] private Image _osLogoImage;
    // Start is called before the first frame update
    void Start()
    {
        _osLogoImage.color = Color.black;
    }


    public void SetFields(string serverId, string serverIp, string osName)
    {
        if (osName.Contains("Windows"))
        {
            _osLogoImage.sprite = _windowsLogo;
        } else if (osName.Contains("Mac"))
        {
            _osLogoImage.sprite = _appleLogo;
        } else if (osName.Contains("Android"))
        {
            _osLogoImage.sprite = _androidLogo;
        }

        _serverIdTmp.text = serverId;
        _serverIpTmp.text = serverIp;
    }


    public void Connect()
    {
        CustomNetworkManager.Instance.networkAddress = _serverIpTmp.text;
        CustomNetworkManager.Instance.StartClient();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
