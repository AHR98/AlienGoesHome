using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float life = 5;
   // Start is called before the first frame update
    void Awake()
    {
        Destroy(gameObject, life);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag != "Player")
        {
            Destroy(gameObject);

        }
    }
}
