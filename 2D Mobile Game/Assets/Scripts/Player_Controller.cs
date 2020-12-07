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
        switch(Touch_Controls.swipeDirection)
        {
            case Touch_Controls.Swipe.None:
                animator.SetFloat("Speed", touch.currentSwipe.sqrMagnitude);
                break;

            //Dashing
            case Touch_Controls.Swipe.Up:

                //Dash animation
                animator.SetFloat("Vertical", touch.currentSwipe.y);
                animator.SetFloat("Horizontal", touch.currentSwipe.x);
                animator.SetFloat("Speed", touch.currentSwipe.sqrMagnitude);

                //run up
                animator.SetFloat("Run", 0);

                break;

            case Touch_Controls.Swipe.Down:

                //Dash animation
                animator.SetFloat("Vertical", touch.currentSwipe.y);
                animator.SetFloat("Horizontal", touch.currentSwipe.x);
                animator.SetFloat("Speed", touch.currentSwipe.sqrMagnitude);

                //run up
                animator.SetFloat("Run", 1);

                break;

            //Shifting
            case Touch_Controls.Swipe.Left:
                Debug.Log("Shift to the Left");

                //Shift animation
                animator.SetFloat("Vertical", touch.currentSwipe.y);
                animator.SetFloat("Horizontal", touch.currentSwipe.x);
                animator.SetFloat("Speed", touch.currentSwipe.sqrMagnitude);

                break;

            case Touch_Controls.Swipe.Right:
                Debug.Log("Shift to the Right");

                //Shift animation
                animator.SetFloat("Vertical", touch.currentSwipe.y);
                animator.SetFloat("Horizontal", touch.currentSwipe.x);
                animator.SetFloat("Speed", touch.currentSwipe.sqrMagnitude);

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
                    Debug.Log("Dash Forwards");

                    //Dash animation
                    //run up

                    rb.velocity = Vector2.up * dashSpeed * Time.deltaTime;

                    break;

                case Touch_Controls.Swipe.Down:
                    Debug.Log("Dash Backwards");

                    rb.velocity = Vector2.down * dashSpeed * Time.deltaTime;

                    break;

                //Shifting
                case Touch_Controls.Swipe.Left:
                    Debug.Log("Shift to the Left");

                    rb.velocity = Vector2.left * dashSpeed * 4 * Time.deltaTime;

                    break;

                case Touch_Controls.Swipe.Right:
                    Debug.Log("Shift to the Right");

                    rb.velocity = Vector2.right * dashSpeed * 4 * Time.deltaTime;

                    break;
            }
        }
    }
}
