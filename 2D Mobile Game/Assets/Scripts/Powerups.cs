using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    private float boost = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //ignore collisions with powerups
        if (collision.gameObject.CompareTag("Enemies"))
        {
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), this.gameObject.GetComponent<Collider2D>(), true);
        }
        else
        {
            //Pickup
            Health H = collision.gameObject.GetComponent<Health>();

            if (H == null) return;
            
            H.HealthPoints += boost;

            this.gameObject.SetActive(false);
        }
    }
}
