using UnityEngine;

public class Touch_Controls : MonoBehaviour
{
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 _tapPos;

    public enum Swipe { None, Tap, Up, Down, Left, Right };
    public float minSwipeLength = 500f;
    public Vector2 currentSwipe;
    public Vector2 Tap
    {
        get => _tapPos;
        set => _tapPos = value;
    }
    public static Swipe swipeDirection;

    void Update()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);

            Tap = Camera.main.ScreenToWorldPoint(t.position);

            if (t.phase == TouchPhase.Began)
            {
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }

            if (t.phase == TouchPhase.Ended)
            {
                secondPressPos = new Vector2(t.position.x, t.position.y);
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                // Make sure it was a legit swipe, not a tap
                if (currentSwipe.sqrMagnitude < minSwipeLength)
                {
                    swipeDirection = Swipe.Tap;
                    return;
                }

                currentSwipe.Normalize();

                // Swipe up
                if (currentSwipe.y > 0 && (currentSwipe.x > -0.5f && currentSwipe.x < 0.5f))
                    swipeDirection = Swipe.Up;

                // Swipe down
                else if (currentSwipe.y < 0 && (currentSwipe.x > -0.5f && currentSwipe.x < 0.5f))
                    swipeDirection = Swipe.Down;

                // Swipe left
                else if (currentSwipe.x < 0 && (currentSwipe.y > -0.5f && currentSwipe.y < 0.5f))
                    swipeDirection = Swipe.Left;

                // Swipe right
                else if (currentSwipe.x > 0 && (currentSwipe.y > -0.5f && currentSwipe.y < 0.5f))
                    swipeDirection = Swipe.Right;
            }
        }
        else
        {
            swipeDirection = Swipe.None;
        }
    }
}
