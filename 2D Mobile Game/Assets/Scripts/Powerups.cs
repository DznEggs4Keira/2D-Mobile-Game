using System.Collections;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    float health = 20f;
    float shieldTimer = 5f;
    float gunTimer = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Handle Effects on Player
        if (collision.gameObject.CompareTag("Player"))
        {
            //Pickup
            Health H = collision.gameObject.GetComponent<Health>();

            if (H == null) return;

            string tag = gameObject.tag;
            switch(tag)
            {
                case "Apple_Health":
                    {
                        HealthPickup(H);
                        break;
                    }
                case "Cherry_Shield":
                    {
                        StartCoroutine(ShieldPickup(collision));
                        break;
                    }
                case "Banana_Gun":
                    {
                        StartCoroutine(GunPickup(collision));
                        break;
                    }
            }
        }
    }

    void HealthPickup(Health H)
    {
        Debug.Log("Health picked up");

        H.HealthPoints += health;

        gameObject.SetActive(false);
    }

    IEnumerator ShieldPickup(Collider2D collision)
    {
        Debug.Log("Shield Activated");

        //don't show cherry sprite when shield is picked up
        GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);

        //move to powerup layer
        collision.gameObject.layer = 7;

        yield return new WaitForSeconds(shieldTimer);

        Debug.Log("Shield Deactivated");
        //bring back to default layer after timer out
        collision.gameObject.layer = 0;

        //re enable and set gameobject to inactive for respawn
        GetComponent<SpriteRenderer>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    IEnumerator GunPickup(Collider2D collision)
    {
        Debug.Log("Banana Gun Picked Up");

        //don't show banana sprite when gun is picked up
        GetComponent<SpriteRenderer>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);

        //Enable use of gun
        collision.GetComponent<Player_Controller>().SetGunActive(true);

        yield return new WaitForSeconds(gunTimer);

        //Disable use of gun
        collision.GetComponent<Player_Controller>().SetGunActive(false);

        Debug.Log("Banana Gun Timed Out");

        //re enable and set gameobject to inactive for respawn
        GetComponent<SpriteRenderer>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

}
