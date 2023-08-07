using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] Canvas _canvas;
    [SerializeField] GameObject _zoneOutPanel;
    [SerializeField] GameObject _deadPanel;

    public void ZoneOutEnable()
    {
        _zoneOutPanel.SetActive(true);
    }

    public void ZoneOutDisable()
    {
        _zoneOutPanel.SetActive(false);
    }

    public void DeadPanelEnable()
    {
        _deadPanel.SetActive(true);
    }

    public void DeadPanelDisable()
    {
        _deadPanel.SetActive(false);
    }
}
