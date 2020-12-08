using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    Animator animator;
    [SerializeField]
    Touch_Controls touch;
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

        if(Touch_Controls.swipeDirection != Touch_Controls.Swipe.None)
        {
            animator.SetFloat("Speed", touch.currentSwipe.sqrMagnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }
        

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
        }
    }

    //When dealing with physics, it's better to use Fixed Updates
    private void FixedUpdate()
    {
        if(dashTime <= 0)
        {
            Touch_Controls.swipeDirection = Touch_Controls.Swipe.None;
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
