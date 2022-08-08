using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHandler : MonoBehaviour
{
    private IDrageable current;

    private float timeDeltaDrag = 0.2f;
    private float lastTime;
    private float distDeltaDrag = 0.2f;
    private Vector3 lastPos;

    private void LateUpdate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
        var input = 0; // leftClick
        if (Input.GetMouseButtonDown(input))
        {
            Collider2D target = Physics2D.OverlapPoint(mousePosition);
            if(target != null)
            {
                var drageable = target.gameObject.GetComponent<IDrageable>();
                if (drageable != null)
                {
                    current = drageable;
                    lastTime = Time.unscaledTime;
                    lastPos = mousePosition;
                }
            }
        }
        else if (Input.GetMouseButton(input))
        {
            if (current == null)
                return;

            if(lastTime + timeDeltaDrag <= Time.unscaledTime || distDeltaDrag <= Vector3.Distance(lastPos,mousePosition))
            {
                current.SetPosition(mousePosition);
            }
        }
        else if (Input.GetMouseButtonUp(input))
        {
            current = null;
            lastTime = Time.unscaledTime;
            lastPos = mousePosition;
        }
    }
}

interface IDrageable
{
    public float DragSpeed();
    public bool CanDrag();
    public void SetPosition(Vector3 position);
}