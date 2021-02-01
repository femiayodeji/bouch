using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    public float power = 10f;
    public float maxDrag = 5f;

    public Rigidbody2D rigidBody;
    public LineRenderer lineRenderer;

    private Vector3 dragStartPosition;
    private Touch touch;

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                DragStart();
            }
            
            if (touch.phase == TouchPhase.Moved)
            {
                Dragging();
            }
            
            if (touch.phase == TouchPhase.Ended)
            {
                DragRelease();
            }
            
        }
    }

    private void DragStart()
    {
        dragStartPosition = Camera.main.ScreenToWorldPoint(touch.position);
        dragStartPosition.z = 0f;
        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, dragStartPosition);
    }    
    private void Dragging()
    {
        Vector3 draggingPosition = Camera.main.ScreenToWorldPoint(touch.position);
        draggingPosition.z = 0f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(1, draggingPosition);        
    }    
    private void DragRelease()
    {
        lineRenderer.positionCount = 0;

        Vector3 dragReleasePosition = Camera.main.ScreenToWorldPoint(touch.position);
        dragReleasePosition.z = 0f;

        Vector3 force = dragStartPosition - dragReleasePosition;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;

        rigidBody.AddForce(clampedForce, ForceMode2D.Impulse);
    }    
}
 