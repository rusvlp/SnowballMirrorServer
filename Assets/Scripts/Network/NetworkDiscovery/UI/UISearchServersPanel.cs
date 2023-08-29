using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UISearchServersPanel : MonoBehaviour
{

    [SerializeField] private GameObject _searchServerPanelImage;
    [SerializeField] private GameObject _openPanelButton;
    
    [SerializeField] private GameObject _mainUi;
    [SerializeField] private CustomNetworkDiscovery _networkDiscovery;
    [SerializeField] private GameObject _uiServerPrefab;

    [SerializeField] private Transform _uiServersParent;
    private Dictionary<DiscoveryResponse, GameObject> _foundServers = new Dictionary<DiscoveryResponse, GameObject>();
    private List<DiscoveryResponse> _receivedResponses = new List<DiscoveryResponse>();
    
    [SerializeField] private GameObject _updateServerListLabel;
    [SerializeField] private Button _updateButton;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
    private void OnEnable()
    {
        print("UISearchServersPanel OnEnable called");
       _networkDiscovery.OnServerFound.AddListener(ServerFoundHandle);
       CustomNetworkDiscovery.OnSearchingStatusUpdate += SearchingUpdateStatusHandler;
    }

    private void OnDisable()
    {
        print("UISearchServersPanel OnDisable called");
        _networkDiscovery.OnServerFound.RemoveListener(ServerFoundHandle);
        CustomNetworkDiscovery.OnSearchingStatusUpdate -= SearchingUpdateStatusHandler;
    }


    private void SearchingUpdateStatusHandler(bool status)
    {
        _updateButton.interactable = !status;
        _updateServerListLabel.SetActive(status);

        if (!status)
        {
            foreach (DiscoveryResponse dr in _foundServers.Keys)
            {
                if (!_receivedResponses.Contains(dr))
                {
                    GameObject uiServer = _foundServers[dr];
                    _foundServers.Remove(dr);
                    Destroy(uiServer);
                }
            }
        }
        else
        {
            _receivedResponses = new List<DiscoveryResponse>();
        }
    }

    public void ServerFoundHandle(DiscoveryResponse response)
    {

        if (!_foundServers.Keys.Contains(response))
        {
            print($"Found server: {response.serverId}");
            GameObject uiServer = Instantiate(_uiServerPrefab, _uiServersParent);
        
            uiServer.GetComponent<UIServer>().SetFields(response.serverId, response.uri.Host, response.operatingSystem);
            _foundServers[response] = uiServer;
        }

        _receivedResponses.Add(response);
    }

  
    
    public void OpenCloseSearchServerPanel()
    {
        if (_searchServerPanelImage.activeSelf && !_openPanelButton.activeSelf)
        {
            _searchServerPanelImage.SetActive(false);
            _openPanelButton.SetActive(true);
            _mainUi.SetActive(true);
        } else if (!_searchServerPanelImage.activeSelf && _openPanelButton.activeSelf)
        {
            _searchServerPanelImage.SetActive(true);
            _openPanelButton.SetActive(false);
            _mainUi.SetActive(false);
        }
    }

    public void StartSearch()
    {
        CustomNetworkDiscovery.Instance.StartSearch();
        
    }
    
}
