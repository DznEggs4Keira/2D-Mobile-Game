using UnityEngine;
using UnityEngine.UI;

public class Touch_Controls : MonoBehaviour
{
    Vector2 beginSwipePos;
    Vector2 endSwipePos;
    Vector2 _tapPos;
    Vector2 _currentswipe;
    readonly float maxTapLength = 125f;
    public enum Swipe { None, Tap, Up, Down, Left, Right };

    public Vector2 CurrentSwipe
    {
        get => _currentswipe;
        set => _currentswipe = value;
    }
    public Vector2 Tap
    {
        get => _tapPos;
        set => _tapPos = value;
    }

    //has to be static as the enum is declared within in the class so needs to be static in order to be accessed globally
    public static Swipe swipeDirection;

    public Text m_Text;
    string message;

    private void Update()
    {
        m_Text.text = message + " " + CurrentSwipe;

        //Touch has occured
        if(Input.touchCount > 0)
        {
            //get and save the first touch in a temp variable
            Touch t = Input.GetTouch(0);

            //get tap position and save for gun
            Tap = Camera.main.ScreenToWorldPoint(t.position);

            //check the status of the Touch
            switch(t.phase)
            {
                case TouchPhase.Began:
                    //if just started, save the first pos
                    beginSwipePos = t.position;
                    break;

                case TouchPhase.Ended:
                    //if touch has ended, save the second position
                    endSwipePos = t.position;

                    //the difference of the two pos is the direction of the current swipe
                    CurrentSwipe = endSwipePos - beginSwipePos;

                    //if the magnitude is less than the defined max tap length then it is a tap, else a swipe
                    if (CurrentSwipe.magnitude < maxTapLength)
                    {
                        swipeDirection = Swipe.Tap;
                        message = "Tap";
                        //exit because some motion has been registered
                        return;
                    }

                    break;
            }

            CurrentSwipe.Normalize();

            if (Mathf.Abs(CurrentSwipe.y) > Mathf.Abs(CurrentSwipe.x))
            {
                // Swipe up
                if (CurrentSwipe.y > 0)
                {
                    swipeDirection = Swipe.Up;
                    message = "Swipe Up";
                    return;
                }
                // Swipe down
                else
                {
                    swipeDirection = Swipe.Down;
                    message = "Swipe Down";
                    return;
                }
            }

            else
            {
                // Swipe left
                if (CurrentSwipe.x < 0)
                {
                    swipeDirection = Swipe.Left;
                    message = "Swipe Left";
                    return;
                }

                // Swipe right
                else
                {
                    swipeDirection = Swipe.Right;
                    message = "Swipe Right";
                    return;
                }
            }
        }
        else
        {
            swipeDirection = Swipe.None;
        }

    }
}
