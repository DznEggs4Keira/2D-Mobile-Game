using System.Collections;
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

    public float dashSpeed;
    public float dashTime;

    private void Start()
    {
        //by default looking forward
        animator.SetFloat("Run", 0);
    }

    //Called every frame
    //Animation
    //Particle Effects
    //Gun Control
    private void Update()
    {
        animator.SetFloat("Vertical", touch.CurrentSwipe.normalized.y);
        animator.SetFloat("Horizontal", touch.CurrentSwipe.normalized.x);

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

                    Instantiate(dashParticle, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().Play();

                    //run up
                    animator.SetFloat("Run", 0);
                    break;

                case Touch_Controls.Swipe.Down:

                    Instantiate(dashParticle, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().Play();

                    //run down
                    animator.SetFloat("Run", 1);
                    break;

                //Shifting 
                
                //animation and effects
                case Touch_Controls.Swipe.Left:

                    Instantiate(shiftParticle, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().Play();

                    sr.flipY = true;
                    break;

                case Touch_Controls.Swipe.Right:

                    Instantiate(shiftParticle, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().Play();

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
    //This will handle the movement of the player
    private void FixedUpdate()
    {
        var
        velocity = Touch_Controls.swipeDirection switch
        {
            Touch_Controls.Swipe.Up => Vector2.up,//Dash upwards
            Touch_Controls.Swipe.Down => Vector2.down,//Dash downwards
            Touch_Controls.Swipe.Left => Vector2.left,//Shift Left
            Touch_Controls.Swipe.Right => Vector2.right,//Shift Right
            _ => Vector2.zero,//no swipe registered so no movement
        };

        //move player
        StartCoroutine(Move(velocity));
    }

    IEnumerator Move(Vector2 velocity)
    {
        rb.velocity = velocity * dashSpeed;

        yield return new WaitForSeconds(dashTime);

        rb.velocity = velocity * 0f;
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
    }    
}
