using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject GamePanel;
    public GameObject PausePanel;
    public GameObject SettingsPanel;
    public GameObject MainMenuPanel;
    public GameObject GameOverPanel;
    private void ClearPanels()
    {
        PausePanel.SetActive(false);
        GamePanel.SetActive(false);
        SettingsPanel.SetActive(false);
        MainMenuPanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }

    public void ShowPause()
    {
        ClearPanels();
        PausePanel.SetActive(true);
    }

    public void ShowGamePanel()
    {
        ClearPanels();
        GamePanel.SetActive(true);
    }

    public void ShowSettingsPanel()
    {
        ClearPanels();
        SettingsPanel.SetActive(true);
    }

    public void ShowMainMenu()
    {
        ClearPanels();
        MainMenuPanel.SetActive(true);
    }

    public void ShowGameOverPanel()
    {
        ClearPanels();
        GameOverPanel.SetActive(true);
    }

}
