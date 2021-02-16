using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public GameObject bulletSpawnPoint;
    public SpriteRenderer sr;
    public Touch_Controls touch;

    readonly float force = 300f;
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
        animator.SetFloat("Vertical", touch.currentSwipe.y);
        animator.SetFloat("Horizontal", touch.currentSwipe.x);

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
                //Dashing animation
                case Touch_Controls.Swipe.Up:
                    //run up
                    animator.SetFloat("Run", 0);
                    break;

                case Touch_Controls.Swipe.Down:
                    //run up
                    animator.SetFloat("Run", 1);
                    break;

                //Shifting animation
                case Touch_Controls.Swipe.Left:
                    sr.flipY = true;
                    break;

                case Touch_Controls.Swipe.Right:
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

        if (dashTime <= 0)
        {
            dashTime = startDashTime;
            Debug.Log("dash reset");
            rb.velocity = Vector2.zero;
        }

        else
        {
            dashTime -= Time.deltaTime;

            switch (Touch_Controls.swipeDirection)
            {
                case Touch_Controls.Swipe.None:
                default:
                    velocity = Vector2.zero;
                    //Debug.Log("No swipe");
                    break;

                //Dashing movement
                case Touch_Controls.Swipe.Up:
                    velocity = Vector2.up * dashSpeed;
                    Debug.Log("Up swipe");
                    break;

                case Touch_Controls.Swipe.Down:
                    velocity = Vector2.down * dashSpeed;
                    Debug.Log("Down swipe");
                    break;

                //Shifting movement
                case Touch_Controls.Swipe.Left:
                    velocity = Vector2.left * dashSpeed * 4;
                    Debug.Log("Left swipe");
                    break;

                case Touch_Controls.Swipe.Right:
                    velocity = Vector2.right * dashSpeed * 4;
                    Debug.Log("right swipe");
                    break;
            }

            rb.velocity = velocity;
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
        //spawn bullet at bullet spawn point
        var bullet = Object_Pooler.Instance.SpawnBullets(Object_Pooler.Instance.Bullets[0].tag,
            bulletSpawnPoint.transform.position, Quaternion.identity);

        //give velocity to bullet
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.Lerp(bulletSpawnPoint.transform.position, touch.Tap, force);
    }    
}
