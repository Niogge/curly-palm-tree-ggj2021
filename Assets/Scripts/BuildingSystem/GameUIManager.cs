using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public GameObject BuildingPanel; //<- set in inspector
    public GameObject PausePanel; //<- set in inspector

    private void Awake()
    {
        BuildingPanel.SetActive(true);
        PausePanel.SetActive(true);
        GameEventSystem.TogglePauseUIEvent += TogglePausePanel;
        GameEventSystem.ToggleBuildingUIEvent += ToggleBuildingPanel;
    }

    private void ToggleBuildingPanel(bool activeStatus, Building building)
    {
        BuildingPanel.GetComponent<BuildingPanelBehaviour>().CurrentBuilding = building;
        BuildingPanel.SetActive(activeStatus);
    }

    private void TogglePausePanel(bool activeStatus)
    {
        PausePanel.SetActive(activeStatus);
    }
}