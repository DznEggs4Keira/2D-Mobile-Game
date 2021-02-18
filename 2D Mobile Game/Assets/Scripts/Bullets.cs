using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public GameObject go_Sparks;

    //Handle Collision and movement and set active to false
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //fire particles for gun shot effect
        GameObject sparks = Instantiate(go_Sparks, new Vector3(collision.contacts[0].point.x, collision.contacts[0].point.y, 0), Quaternion.identity);

        sparks.GetComponent<ParticleSystem>().Play();

        //"destroy" the enemy
        collision.gameObject.SetActive(false);
        
        //Deactivate bullet
        gameObject.SetActive(false);
    }
}
