using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;
    float dashTime;

    public float dashSpeed;
    public float startDashTime;

    private void Start()
    {
        dashTime = startDashTime;
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

                    rb.velocity = Vector2.up * dashSpeed;

                    break;

                case Touch_Controls.Swipe.Down:
                    Debug.Log("Dash Backwards");

                    rb.velocity = Vector2.down * dashSpeed;

                    break;

                //Shifting
                case Touch_Controls.Swipe.Left:
                    Debug.Log("Shift to the Left");

                    rb.velocity = Vector2.left * dashSpeed * 4;

                    break;

                case Touch_Controls.Swipe.Right:
                    Debug.Log("Shift to the Right");

                    rb.velocity = Vector2.right * dashSpeed * 4;

                    break;
            }
        }
    }
}
