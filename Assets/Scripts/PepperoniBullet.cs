using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PepperoniBullet : MonoBehaviour
{
    public float life = 3;
    // Start is called before the first frame update
    void Awake()
    {
        Destroy(gameObject, life);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "Enemy")
        {
            Destroy(gameObject);

        }
    }
}
