﻿using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject bulletSpawnPoint;
    public SpriteRenderer sr;
    public Touch_Controls touch;

    public GameObject dashParticle;
    public GameObject shiftParticle;

    readonly float bulletForce = 250f;

    public float dashSpeed;

    public Options_Menu options_Menu;

    private void Start()
    {
        //by default looking forward
        animator.SetFloat("Run", 0);
    }

    //When dealing with physics, it's better to use Fixed Updates
    //This will handle the movement of the player
    private void FixedUpdate()
    {
        if (touch.dashTime <= 0)
        {
            //Set player velocity to zero and reset dash time
            Touch_Controls.swipeDirection = Touch_Controls.Swipe.None;
            rb.velocity = Vector2.zero;
        }
        else
        {
            touch.dashTime -= Time.fixedDeltaTime;

            switch (Touch_Controls.swipeDirection)
            {
                case Touch_Controls.Swipe.Up:
                    {
                        rb.velocity = Vector2.up * dashSpeed * Time.fixedDeltaTime;
                        break;
                    }
                case Touch_Controls.Swipe.Down:
                    {
                        rb.velocity = Vector2.down * dashSpeed * Time.fixedDeltaTime;
                        break;
                    }
                case Touch_Controls.Swipe.Left:
                    {
                        rb.velocity = Vector2.left * dashSpeed * 4 * Time.fixedDeltaTime;
                        break;
                    }
                case Touch_Controls.Swipe.Right:
                    {
                        rb.velocity = Vector2.right * dashSpeed * 4 * Time.fixedDeltaTime;
                        break;
                    }
                default:
                    {
                        rb.velocity = Vector2.zero;
                        touch.dashTime = touch.startDashTime;
                        break;
                    }
            }
        }
    }

    //Called every frame
    //Animation
    //Particle Effects
    //Gun Control
    //Vibration
    private void Update()
    {
        animator.SetFloat("Vertical", touch.CurrentSwipe.normalized.y);
        animator.SetFloat("Horizontal", touch.CurrentSwipe.normalized.x);

        if (Touch_Controls.swipeDirection == Touch_Controls.Swipe.None 
            || Touch_Controls.swipeDirection == Touch_Controls.Swipe.Tap)
        {
            animator.SetBool("Moving", false);
        }
        else
        {
            animator.SetBool("Moving", true);
        }

        switch (Touch_Controls.swipeDirection)
        {
            //Dashing 
            //animation and effects
            case Touch_Controls.Swipe.Up:

                Instantiate(dashParticle, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().Play();

                //run up
                animator.SetFloat("Run", 0);

                //vibrate
                options_Menu.Vibrate();

                break;

            case Touch_Controls.Swipe.Down:

                Instantiate(dashParticle, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().Play();

                //run down
                animator.SetFloat("Run", 1);

                //vibrate
                options_Menu.Vibrate();
                break;

            //Shifting 
                
            //animation and effects
            case Touch_Controls.Swipe.Left:

                Instantiate(shiftParticle, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().Play();

                sr.flipY = true;

                //vibrate
                options_Menu.Vibrate();
                break;

            case Touch_Controls.Swipe.Right:

                Instantiate(shiftParticle, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().Play();

                sr.flipY = false;

                //vibrate
                options_Menu.Vibrate();
                break;

            //Shoot Gun
            case Touch_Controls.Swipe.Tap:
                if(bulletSpawnPoint.activeInHierarchy)
                    ShootGun();
                break;
        }
    }

    public void SetGunActive(bool value)
    {
        if (value)
        {
            bulletSpawnPoint.SetActive(true);
        }
        else
        {
            bulletSpawnPoint.SetActive(false);
        }
    }

    void ShootGun()
    {
        //play gun sound
        FindObjectOfType<Audio_Manager>().Play("Player_Shoot");

        //spawn bullet at bullet spawn point
        var bullet = Object_Pooler.Instance.SpawnBullets(Object_Pooler.Instance.Bullets[0].tag,
            bulletSpawnPoint.transform.position, Quaternion.identity);

        //give velocity to bullet
        bullet.GetComponent<Rigidbody2D>().velocity = touch.Tap * bulletForce * Time.fixedDeltaTime;
    }    

}
