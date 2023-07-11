using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OfflineConnectManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField ipAddress;

    [SerializeField] private CustomNetworkManager netManager;
    // Start is called before the first frame update
    void Start()
    {
       
        
        //print(this.netManager);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartServer()
    {
        this.netManager.StartServer();
    }

    public void StartClient()
    {
        this.netManager.networkAddress = ipAddress.text;
        this.netManager.StartClient();
    }
}
