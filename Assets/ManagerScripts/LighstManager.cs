using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LighstManager : MonoBehaviour
{
    private int level = 0;
    public GameObject MainMenuPanel;
    [SerializeField]
    private Light lightLevel1;
    [SerializeField]
    private Light lightLevel2;
    void Update()
    {
        setLevel();
    }
    private void setLevel()
    {

        level = GameManager.instance.getGameLevel();
        activeLevelLigths();
    }

    private void activeLevelLigths()
    {
        if(!MainMenuPanel.activeInHierarchy)
        {
            switch (level)
            {
                case 1:
                    desactivateLight(lightLevel2.transform.gameObject);
                    activeLight(lightLevel1.transform.gameObject);
                    break;
                case 2:
                    desactivateLight(lightLevel1.transform.gameObject);
                    activeLight(lightLevel2.transform.gameObject);
                    break;
            }

        }
        else
        {
            desactivateLight(lightLevel2.transform.gameObject);
            desactivateLight(lightLevel1.transform.gameObject);
        }
        
    }


    private void activeLight(GameObject _light)
    {
        _light.SetActive(true);
    }

    private void desactivateLight(GameObject _light)
    {
        _light.SetActive(false);
    }
}
