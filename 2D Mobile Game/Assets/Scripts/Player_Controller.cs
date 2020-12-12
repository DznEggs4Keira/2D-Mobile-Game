using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public Touch_Controls touch;
    public SpriteRenderer sr;

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
            if (Touch_Controls.swipeDirection == Touch_Controls.Swipe.None)
            {
                animator.SetBool("Moving", false);
            }
            else
            {
                animator.SetBool("Moving", true);
            }
        }

        {
            if (dashTime <= 0)
            {
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
            }

            else
            {
                dashTime -= Time.deltaTime;

                switch (Touch_Controls.swipeDirection)
                {
                    //Dashing
                    case Touch_Controls.Swipe.Up:

                        //run up
                        animator.SetFloat("Run", 0);
                        break;

                    case Touch_Controls.Swipe.Down:

                        //run up
                        animator.SetFloat("Run", 1);
                        break;

                    //Shifting
                    case Touch_Controls.Swipe.Left:

                        sr.flipY = true;
                        break;

                    case Touch_Controls.Swipe.Right:

                        sr.flipY = false;
                        break;
                }
            }
        }
    }

    //When dealing with physics, it's better to use Fixed Updates
    private void FixedUpdate()
    {
        if(dashTime <= 0)
        {
            dashTime = startDashTime;
            rb.velocity = Vector2.zero;
        }

        else
        {
            dashTime -= Time.deltaTime;

            switch (Touch_Controls.swipeDirection)
            {
                case Touch_Controls.Swipe.None:
                    dashTime = startDashTime;
                    rb.velocity = Vector2.zero;
                    break;

                //Dashing
                case Touch_Controls.Swipe.Up:
                    rb.velocity = Vector2.up * dashSpeed;
                    break;

                case Touch_Controls.Swipe.Down:
                    rb.velocity = Vector2.down * dashSpeed;
                    break;

                //Shifting
                case Touch_Controls.Swipe.Left:
                    rb.velocity = Vector2.left * dashSpeed * 4;
                    break;

                case Touch_Controls.Swipe.Right:
                    rb.velocity = Vector2.right * dashSpeed * 4;
                    break;
            }
        }
    }
}
