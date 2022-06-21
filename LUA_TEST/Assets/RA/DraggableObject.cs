using RA.Inputs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider2D))]
public class DraggableObject : MonoBehaviour, IDrageable
{
    [SerializeField] private float speed = 10f;

    public bool CanDrag()
    {
        return true;
    }

    public float DragSpeed()
    {
        return speed;
    }
}

