using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    Touch_Controls touch;

    //When dealing with physics, it's better to use Fixed Updates
    private void FixedUpdate()
    {
        switch(Touch_Controls.swipeDirection)
        {
            case Touch_Controls.Swipe.None:
            default:
                break;

            //Dashing
            case Touch_Controls.Swipe.Up:
                Debug.Log("Dash Forwards");
                break;

            case Touch_Controls.Swipe.Down:
                Debug.Log("Dash Backwards");
                break;

            //Shifting
            case Touch_Controls.Swipe.Left:
                Debug.Log("Shift to the Left");
                break;

            case Touch_Controls.Swipe.Right:
                Debug.Log("Shift to the Right");
                break;
        }
    }
}
