using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private int level = 0;
    public GameObject MainMenuPanel;
    [SerializeField]
    private GameObject cameraLevel1;
    [SerializeField]
    private GameObject cameraLevel2;

    void Update()
    {
        setLevel();
    }
    private void setLevel()
    {

        level = GameManager.instance.getGameLevel();
       
        activeCamera();
    }

    private void activeCamera()
    {
        if (!MainMenuPanel.activeInHierarchy)
        {
            switch (level)
            {
                case 1:
                    desactivateCamera(cameraLevel2);
                    activeCamera(cameraLevel1);
                    break;
                case 2:
                    desactivateCamera(cameraLevel1);
                    activeCamera(cameraLevel2);
                    break;
            }

        }
        else
        {
            desactivateCamera(cameraLevel2);
            desactivateCamera(cameraLevel1);
        }

    }
    private void activeCamera(GameObject _camera)
    {
        _camera.SetActive(true);
    }

    private void desactivateCamera(GameObject _camera)
    {
        _camera.SetActive(false);
    }
}
