using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject bulletSpawnPoint;
    public SpriteRenderer sr;
    public Touch_Controls touch;

    public GameObject dashParticle;
    public GameObject shiftParticle;

    readonly float bulletForce = 5f; //5000f; - used with lerp
    float dashTime;

    public float dashSpeed;
    public float startDashTime;

    private void Start()
    {
        dashTime = startDashTime;

        //by default looking forward
        animator.SetFloat("Run", 0);
    }

    //handle animations in update for framerate
    private void Update()
    {
        animator.SetFloat("Vertical", touch.CurrentSwipe.y);
        animator.SetFloat("Horizontal", touch.CurrentSwipe.x);

        {
            if (Touch_Controls.swipeDirection == Touch_Controls.Swipe.None 
                || Touch_Controls.swipeDirection == Touch_Controls.Swipe.Tap)
            {
                animator.SetBool("Moving", false);
            }
            else
            {
                animator.SetBool("Moving", true);
            }
        }

        {
            switch (Touch_Controls.swipeDirection)
            {
                //Dashing 
                
                //animation and effects
                case Touch_Controls.Swipe.Up:

                    Instantiate(dashParticle, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);

                    //run up
                    animator.SetFloat("Run", 0);
                    break;

                case Touch_Controls.Swipe.Down:

                    Instantiate(dashParticle, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);

                    //run down
                    animator.SetFloat("Run", 1);
                    break;

                //Shifting 
                
                //animation and effects
                case Touch_Controls.Swipe.Left:

                    Instantiate(shiftParticle, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);

                    sr.flipY = true;
                    break;

                case Touch_Controls.Swipe.Right:

                    Instantiate(shiftParticle, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);

                    sr.flipY = false;
                    break;

                //Shoot Gun
                case Touch_Controls.Swipe.Tap:
                    if(bulletSpawnPoint.activeInHierarchy)
                    {
                        ShootGun();
                        Debug.Log("Gun Shot");
                    }
                    else
                    {
                        Debug.Log("Tap Registered");
                    }
                    break;
            }
        }
    }

    //When dealing with physics, it's better to use Fixed Updates
    private void FixedUpdate()
    {
        Vector2 velocity;

        dashTime -= Time.deltaTime;

        if (dashTime <= 0)
        {
            dashTime = startDashTime;
            Debug.Log("dash reset");

            rb.velocity = Vector2.zero;
        }

        switch (Touch_Controls.swipeDirection)
        {
            //Dashing movement
            case Touch_Controls.Swipe.Up:
                {
                    velocity = Vector2.up * dashSpeed;
                    Debug.Log("Up swipe");
                    break;
                }

            case Touch_Controls.Swipe.Down:
                {
                    velocity = Vector2.down * dashSpeed;
                    Debug.Log("Down swipe");
                    break;
                }
                
            //Shifting movement
            case Touch_Controls.Swipe.Left:
                {
                    velocity = Vector2.left * dashSpeed * 4;
                    Debug.Log("Left swipe");
                    break;
                }

            case Touch_Controls.Swipe.Right:
                {
                    velocity = Vector2.right * dashSpeed * 4;
                    Debug.Log("right swipe");
                    break;
                }

            case Touch_Controls.Swipe.None:
            default:
                {
                    velocity = Vector2.zero;
                    break;
                }
        }

        rb.velocity = velocity;
        //rb.AddForce(velocity);
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
        //spawn bullet at bullet spawn point
        var bullet = Object_Pooler.Instance.SpawnBullets(Object_Pooler.Instance.Bullets[0].tag,
            bulletSpawnPoint.transform.position, Quaternion.identity);

        //give velocity to bullet
        bullet.GetComponent<Rigidbody2D>().velocity = touch.Tap * bulletForce;
            //Vector2.Lerp(bulletSpawnPoint.transform.position, touch.Tap, bulletForce);
    }    
}
