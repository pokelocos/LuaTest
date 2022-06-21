using RA.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class DraggableObject : MonoBehaviour, IDrageable
{
    [SerializeField] private float speed = 10f;

    private static bool snapPosition = false;

    public bool CanDrag()
    {
        return true;
    }

    public float DragSpeed()
    {
        return speed;
    }

    public void SetPosition(Vector3 position)
    {
       

        if(!snapPosition)
        {
            var pos = Vector2.Lerp(transform.position, position, speed * Time.unscaledDeltaTime);
            transform.position = pos;
        }
        else
        {
            var pos = position;
            pos.x = Mathf.CeilToInt(pos.x) - 0.5f;
            pos.y = Mathf.CeilToInt(pos.y) - 0.5f;
            transform.position = pos;
        }
    }
}

