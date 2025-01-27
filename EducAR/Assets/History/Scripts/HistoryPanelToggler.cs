using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryPanelToggler : MonoBehaviour
{
    bool isOn = false;
    [SerializeField] GameObject InfoPanel;

    public void togglePanel()
    {
        if (isOn)
        {
            InfoPanel.SetActive(false);
            isOn = false;
        }
        else
        {
            InfoPanel.SetActive(true);
            isOn = true;
        }
    }
}
