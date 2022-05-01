using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
class MoveControl
{
    public Transform T;
    public Rigidbody2D Rb;
    public float MoveSpeed;
    public float TouchRadius;
    public float rotationSpeed;

    private TouchControl touch;
    private bool movingWithTouch;

    private Vector2 pos => (Vector2)T.position;

    public void SetCamera(Camera camera)
    {
        touch.camera = camera;
    }

    public MoveControl()
    {
        touch = new TouchControl();
        touch.camera = null;
        movingWithTouch = false;
    }

    private bool GetMovingWithTouch()
    {
        return (((Vector2)Rb.position - touch.position).sqrMagnitude <= (TouchRadius * TouchRadius));
    }

    public void LookAt(Vector2 point)
    {
        T.up = Vector2.MoveTowards(T.up, point - pos, Time.deltaTime * rotationSpeed);
    }

    public void MoveRB()
    {
        touch.UpdateTouchPosition();
        switch (touch.phase)
        {
            case TouchPhase.Began:
                {
                    movingWithTouch = GetMovingWithTouch();
                    break;
                }
            case TouchPhase.Moved:
                {
                    if (movingWithTouch)
                    {
                        Rb.MovePosition(touch.position);
                    }
                    else
                    {
                        Rb.MovePosition(Vector2.MoveTowards(Rb.position, touch.position, Time.fixedDeltaTime * MoveSpeed));
                        movingWithTouch = GetMovingWithTouch();
                    }
                    break;
                }
            default:
                {
                    movingWithTouch = false;
                    break;
                }
        }
    }
}

