using UnityEngine;
using UnityEngine.UI;

public class Touch_Controls : MonoBehaviour
{
    public enum Swipe { None, Tap, Up, Down, Left, Right };

    Vector2 _tapPos;
    Vector2 _currentswipe;

    public float dashTime;

    public float startDashTime = 0.1f;

    readonly float maxTapLength = 20f;

    public static Swipe swipeDirection;

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

    public Text m_Text;
    string message;

    private void Start()
    {
        dashTime = startDashTime;
    }

    private void Update()
    {
        if(m_Text != null)
        {
            m_Text.text = message + " " + CurrentSwipe;
        }

        //Touch has occured
        if(Input.touchCount > 0)
        {
            //get and save the first touch in a temp variable
            Touch t = Input.GetTouch(0);

            //get tap position and save for gun
            Tap = Camera.main.ScreenToWorldPoint(t.position);

            //check the status of the Touch
            if(t.phase == TouchPhase.Moved) DetectSwipe(t);
        }
        else
        {
            swipeDirection = Swipe.None;
            dashTime = startDashTime;
        }

    }

    void DetectSwipe(Touch t)
    {
        Debug.Log(t.deltaPosition.magnitude);
        if (t.deltaPosition.magnitude < maxTapLength)
        {
            swipeDirection = Swipe.Tap;
            CurrentSwipe = t.deltaPosition;
            message = "Tap";
        }
        else
        {
            if (t.deltaPosition.y > 0)
            {
                if (t.deltaPosition.x > 0)
                {
                    if (t.deltaPosition.y < t.deltaPosition.x)
                    {
                        //swipe right
                        swipeDirection = Swipe.Right;
                        CurrentSwipe = t.deltaPosition;
                        message = "Swipe Right";
                    }
                    else
                    {
                        //swipe up
                        swipeDirection = Swipe.Up;
                        CurrentSwipe = t.deltaPosition;
                        message = "Swipe Up";
                    }
                }
                else if (t.deltaPosition.x < 0)
                {
                    if (t.deltaPosition.y < Mathf.Abs(t.deltaPosition.x))
                    {
                        //swipe left
                        swipeDirection = Swipe.Left;
                        CurrentSwipe = t.deltaPosition;
                        message = "Swipe Left";
                    }
                    else
                    {
                        //swipe up
                        swipeDirection = Swipe.Up;
                        CurrentSwipe = t.deltaPosition;
                        message = "Swipe Up";
                    }

                }
                else
                {
                    //swipe up
                    swipeDirection = Swipe.Up;
                    CurrentSwipe = t.deltaPosition;
                    message = "Swipe Up";
                }
            }
            else if (t.deltaPosition.y < 0)
            {
                if (t.deltaPosition.x > 0)
                {
                    if (Mathf.Abs(t.deltaPosition.y) < t.deltaPosition.x)
                    {
                        //swipe right
                        swipeDirection = Swipe.Right;
                        CurrentSwipe = t.deltaPosition;
                        message = "Swipe Right";
                    }
                    else
                    {
                        //swipe down
                        swipeDirection = Swipe.Down;
                        CurrentSwipe = t.deltaPosition;
                        message = "Swipe Down";
                    }
                }
                else if (t.deltaPosition.x < 0)
                {
                    if (Mathf.Abs(t.deltaPosition.y) < Mathf.Abs(t.deltaPosition.x))
                    {
                        //swipe left
                        swipeDirection = Swipe.Left;
                        CurrentSwipe = t.deltaPosition;
                        message = "Swipe Left";
                    }
                    else
                    {
                        //swipe up
                        swipeDirection = Swipe.Down;
                        CurrentSwipe = t.deltaPosition;
                        message = "Swipe Down";
                    }

                }
                else
                {
                    //swipe up
                    swipeDirection = Swipe.Down;
                    CurrentSwipe = t.deltaPosition;
                    message = "Swipe Down";
                }
            }
            else
            {
                if (t.deltaPosition.x > 0)
                {
                    //swipe right
                    swipeDirection = Swipe.Right;
                    CurrentSwipe = t.deltaPosition;
                    message = "Swipe Right";
                }
                else if (t.deltaPosition.x < 0)
                {
                    //swipe left
                    swipeDirection = Swipe.Left;
                    CurrentSwipe = t.deltaPosition;
                    message = "Swipe Left";

                }
                else
                {
                    //swipe null
                    swipeDirection = Swipe.None;
                    CurrentSwipe = t.deltaPosition;
                    message = "Swipe None";
                }
            }
        }
    }
}
