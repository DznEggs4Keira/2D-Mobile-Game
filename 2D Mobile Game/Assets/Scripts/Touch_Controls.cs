using UnityEngine;

public class Touch_Controls : MonoBehaviour
{
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 _tapPos;
    Vector2 _currentswipe;

    public enum Swipe { None, Tap, Up, Down, Left, Right };
    readonly float minSwipeLength = 200f; //this is the length in pixels

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

    public static Swipe swipeDirection;

    void Update()
    {
        if (Input.touches.Length > 0) //then some touch has happened and cannot be none
        {
            Touch t = Input.GetTouch(0);

            //save the position of the first touch as a tap for gun
            Tap = Camera.main.ScreenToWorldPoint(t.position);

            //process if a tap or swipe has happened
            if (t.phase == TouchPhase.Began)
            {
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }

            if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
            {
                secondPressPos = new Vector2(t.position.x, t.position.y);
                CurrentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                // Make sure it was a legit swipe, not a tap
                if (CurrentSwipe.sqrMagnitude < minSwipeLength)
                {
                    swipeDirection = Swipe.Tap;
                    return;
                }

                CurrentSwipe.Normalize();

                if(Mathf.Abs(CurrentSwipe.y) > Mathf.Abs(CurrentSwipe.x))
                {
                    // Swipe up
                    if (CurrentSwipe.y > 0)
                        swipeDirection = Swipe.Up;

                    // Swipe down
                    else
                        swipeDirection = Swipe.Down;
                }

                else
                {
                     // Swipe left
                    if (CurrentSwipe.x < 0)
                        swipeDirection = Swipe.Left;

                    // Swipe right
                    else
                        swipeDirection = Swipe.Right;
                }
            }
        }
        else
        {
            swipeDirection = Swipe.None;
        }
    }
}
