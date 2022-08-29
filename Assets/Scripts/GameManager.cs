using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    int time = 30;
    private void Awake()
    {
        if(instance == null)
            instance = this;
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
}
