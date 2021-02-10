using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    float health = 20f;
    float shieldTimer = 5f;
    bool gunActive = false;

    private void OnCollisionEnter2D(Collision2D collision)
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
                        H.HealthPoints += health;

                        //gameObject.SetActive(false);
                        break;
                    }
                case "Cherry_Shield":
                    {
                        //put code in coroutine

                        Debug.Log("Shield Activated");
                        collision.gameObject.layer = 3;
                        float tempTimer = shieldTimer;

                        while (shieldTimer > 0.0f)
                        {
                            tempTimer -= Time.deltaTime;
                        }

                        Debug.Log("Shield Deactivated");
                        collision.gameObject.layer = 0;

                        //gameObject.SetActive(false);
                        break;
                    }
                case "Banana_Gun":
                    {
                        gunActive = !gunActive;

                        Debug.Log("Banana Gun Picked up: " + gunActive);

                        //gameObject.SetActive(false);
                        break;
                    }
            }
        }
    }
}
