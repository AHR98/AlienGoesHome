using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    public int level;
    public int healthPlayer;
    public int bullets;
    public int hpynosis;
    public float[] positionPlayer;
    public PlayerData(SlikyController player, Gun gunPlayer, GameObject slinkyGameObject)
    {
        positionPlayer = new float[3];
        positionPlayer[0] = slinkyGameObject.transform.position.x;
        positionPlayer[1] = slinkyGameObject.transform.position.y;
        positionPlayer[2] = slinkyGameObject.transform.position.z;

        if (player.level1)
            level = 1;
        if (player.isTouchingFloor)
        {
            level = 2;
            positionPlayer[1] = slinkyGameObject.transform.position.y + 1.5f;
        }
            
        healthPlayer = player.currentHealth;
        hpynosis = player.currentHypnosis;
        bullets = gunPlayer.currentBullets;


 


    }
}
