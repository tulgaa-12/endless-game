using UnityEngine;
using System.Collections;

public class SwipeController : MonoBehaviour
{
    public static bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDragging = false;
    private Vector2 startTouch, swipeDelta;

    public float minSwipeDistance = 50f; 

    void Update()
    {
      
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        #region Mouse Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDragging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            DetectSwipe();
            ResetSwipe();
        }
        #endregion

        #region Touch Inputs
        if (Input.touches.Length > 0)
        {
            Touch t = Input.touches[0];
            if (t.phase == TouchPhase.Began)
            {
                tap = true;
                isDragging = true;
                startTouch = t.position;
            }
            else if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
            {
                isDragging = false;
                DetectSwipe();
                ResetSwipe();
            }
        }
        #endregion

       
        swipeDelta = Vector2.zero;
        if (isDragging)
        {
            if (Input.touches.Length > 0)
                swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }
    }

    private void DetectSwipe()
    {
        if (swipeDelta.magnitude > minSwipeDistance)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                if (x < 0) swipeLeft = true;
                else swipeRight = true;
            }
            else
            {
                if (y < 0) swipeDown = true;
                else swipeUp = true;
            }
        }
    }

    private void ResetSwipe()
    {
        startTouch = swipeDelta = Vector2.zero;
    }
}
