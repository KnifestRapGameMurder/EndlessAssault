using System;
using UnityEngine;

class TouchControl
{
    public Camera camera;
    //public bool isTouching => Input.touchCount > 0;
    public bool isTouching => (Input.GetMouseButton(0));
    public TouchPhase phase { get; private set; }
    public Vector2 position { get; private set; }
    public Vector2 previousPosition { get; private set; }

    public void UpdateTouchPosition()
    {
        previousPosition = position;
        phase
            = Input.GetMouseButtonDown(0) ? TouchPhase.Began
            : Input.GetMouseButton(0) ? TouchPhase.Moved
            : TouchPhase.Ended;
        if (!isTouching)
            return;
        //phase = t.phase;
        //var t = Input.GetTouch(0);
        //position = camera.ScreenToWorldPoint(t.position);
        position = camera.ScreenToWorldPoint(Input.mousePosition);
        
    }
}
