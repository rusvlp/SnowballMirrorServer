using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Mirror;
using Mirror.Discovery;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


[Serializable]
public class CustomServerFoundUnityEvent : UnityEvent<DiscoveryResponse> { };

public class CustomNetworkDiscovery : NetworkDiscoveryBase<DiscoveryRequest, DiscoveryResponse>
{

    public string ServerID;

    
    public CustomServerFoundUnityEvent OnServerFound;

    public Transport transport;
    
    public static CustomNetworkDiscovery Instance;
    
    [SerializeField] private float _discoveryTimeout = 10f;
    

    public static Action<bool> OnSearchingStatusUpdate;
    
    public bool isSearching = false;

    private void Awake()
    {
        Instance = this;
    }


    private void OnEnable()
    {
        CustomNetworkManager.OnServerStarted += ServerStartedHandle;
    }

    private void OnDisable()
    {
        CustomNetworkManager.OnServerStarted -= ServerStartedHandle;
    }


    private void ServerStartedHandle()
    {
        base.AdvertiseServer();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
        //print("Start method from CustomNetworkDiscovery is called");

        ServerID = "S-"+MatchMaker.GetRandomID();
        
        if (transport == null)
        {
            transport = Transport.active;
        }
        
        
        base.Start();
        
       
        
        if (GameplayDevelopmentModeManager.Instance != null &&
            GameplayDevelopmentModeManager.Instance.isGameplayDevelopmentMode)
        {

            StartSearch();
        }
    }

    
    #region Server
    protected override DiscoveryResponse ProcessRequest(DiscoveryRequest request, IPEndPoint endpoint)
    {
        try
        {
            return new DiscoveryResponse
            {
                serverId = ServerID,
                uri = transport.ServerUri(), 
                operatingSystem = SystemInfo.operatingSystem
            };
        }
        catch (NotImplementedException)
        {
            print($"Transport {transport} does not support network discovery");
            throw;
        }
    }
    #endregion
    
    
    #region Client
    protected override DiscoveryRequest GetRequest() => new DiscoveryRequest();
    
    
    protected override void ProcessResponse(DiscoveryResponse response, IPEndPoint endpoint)
    {
        response.EndPoint = endpoint;

        // although we got a supposedly valid url, we may not be able to resolve
        // the provided host
        // However we know the real ip address of the server because we just
        // received a packet from it,  so use that as host.
        UriBuilder realUri = new UriBuilder(response.uri)
        {
            Host = response.EndPoint.Address.ToString()
        };
        response.uri = realUri.Uri;

        print("Response processed");
        
        
        
        OnServerFound.Invoke(response);
        
        if (GameplayDevelopmentModeManager.Instance != null &&
            GameplayDevelopmentModeManager.Instance.isGameplayDevelopmentMode)
        {
            Connect(response);
        }
        
        
        
    }

    private void Connect(DiscoveryResponse response)
    {
        CustomNetworkManager.Instance.networkAddress = response.uri.Host;
        CustomNetworkManager.Instance.StartClient();
    }
    
    
    #endregion

    public void StartSearch()
    {
        print("Searching started");

        StartCoroutine(DiscoveryRoutine());

    }

    private IEnumerator DiscoveryRoutine()
    {
        
        base.StartDiscovery();
        OnSearchingStatusUpdate?.Invoke(true);

        yield return new WaitForSeconds(_discoveryTimeout);
        
        base.StopDiscovery();
        OnSearchingStatusUpdate?.Invoke(false);
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
