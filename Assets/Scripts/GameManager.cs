using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public UIManager getPausePanel;
    public GameObject MainMenuPanel;

    int time = 30;
    private void Awake()
    {
        if(instance == null)
            instance = this;
    //    PauseGame();

    }

    private void Start()
    {
        StartCoroutine(CountDownRutine());
    }
    IEnumerator CountDownRutine()
    {
        yield return new WaitForSeconds(1);

        time--;
    }
    private void Update()
    {
        if(!MainMenuPanel.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
                getPausePanel.ShowPause();
            }
        }
       
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
