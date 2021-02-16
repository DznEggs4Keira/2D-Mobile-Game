using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    //Handle Collision and movement and set active to false
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //"destroy" the enemy
        collision.gameObject.SetActive(false);
        
        //Deactivate bullet
        gameObject.SetActive(false);
    }
}
