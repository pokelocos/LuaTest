using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private Camera cameraFollow;

    private Vector3 cameraFollowPosition;
    private bool edgeScrolling;
    private float cameraZoom = 8;

    [SerializeField] private Vector3 cameraStartPosition = new Vector3(0, 0, -10);

    [Header("Variables de velocidad")]
    [SerializeField] private float cameraMoveSpeed = 3f;
    [SerializeField] private float cameraZoomSpeed = 10f;
    [SerializeField] private float moveAmount = 10f;
    [SerializeField] private float edgeSize = 30f;
    [SerializeField] private float zoomChangeAmount = 80f;

    [Header("Boundries")]
    [SerializeField] private Vector2 zoomBoundries = new Vector2(5, 20);
    [SerializeField] private Rect movementBoundries = new Rect(new Vector2(0, 0), new Vector2(50, 50));
    bool drag = false;
    bool lastDrag = false;
    bool hitNode = false;
    Vector3 difference;
    Vector3 origen;

    [Space, Header("Sounds")]
    public AudioSource source;
    public AudioClip startGrabSound;
    public AudioClip endGrabSound;


    void Start()
    {
        cameraFollow.transform.position = cameraStartPosition;
    }

    // Update is called once per frame
    void Update()
    {
        HandleManualMovement();
        HandleEdgeScrolling();

        ZoomMovementFunction();
    }

    void LateUpdate()
    {
        HandleMovement();
        HandleZoom();
        MoveByDrag();
    }

    void HandleMovement()
    {
        cameraFollowPosition.z = transform.position.z;
        Vector3 cameraMoveDir = (cameraFollowPosition - transform.position).normalized;
        float distance = Vector3.Distance(cameraFollowPosition, transform.position);

        if (distance > 0)
        {
            Vector3 newCameraPosition = transform.position + (cameraMoveDir * distance * cameraMoveSpeed * Time.unscaledDeltaTime);

            float x = Mathf.Clamp(newCameraPosition.x, movementBoundries.x - movementBoundries.width, movementBoundries.x + movementBoundries.width);
            float y = Mathf.Clamp(newCameraPosition.y, movementBoundries.y - movementBoundries.height, movementBoundries.y + movementBoundries.height);

            newCameraPosition = new Vector3(x, y, newCameraPosition.z);

            float distanceAfterMoving = Vector3.Distance(newCameraPosition, cameraFollowPosition);

            if (distanceAfterMoving > distance)
            {
                // Overshot the target
                newCameraPosition = cameraFollowPosition;
            }

            transform.position = newCameraPosition;
        }
    }

    void HandleZoom()
    {
        float cameraZoomDifference = cameraZoom - cameraFollow.orthographicSize;

        cameraFollow.orthographicSize += cameraZoomDifference * cameraZoomSpeed * Time.unscaledDeltaTime;

        if (cameraZoomDifference > 0)
        {
            if (cameraFollow.orthographicSize > cameraZoom)
            {
                cameraFollow.orthographicSize = cameraZoom;
            }
        }
        else
        {
            if (cameraFollow.orthographicSize < cameraZoom)
            {
                cameraFollow.orthographicSize = cameraZoom;
            }
        }
    }

    private void HandleManualMovement()
    {
        return; // eleminar (!!!)

        if (Input.GetKey(KeyCode.W))
        {
            cameraFollowPosition.y += moveAmount * Time.unscaledDeltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            cameraFollowPosition.y -= moveAmount * Time.unscaledDeltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            cameraFollowPosition.x -= moveAmount * Time.unscaledDeltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            cameraFollowPosition.x += moveAmount * Time.unscaledDeltaTime;
        }
    }

    private void HandleEdgeScrolling()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            edgeScrolling = !edgeScrolling;
        }

        if (edgeScrolling)
        {
            if (Input.mousePosition.x > Screen.width - edgeSize)
            {
                cameraFollowPosition.x += moveAmount * Time.unscaledDeltaTime;
            }
            if (Input.mousePosition.x < edgeSize)
            {
                cameraFollowPosition.x -= moveAmount * Time.unscaledDeltaTime;
            }
            if (Input.mousePosition.y > Screen.height - edgeSize)
            {
                cameraFollowPosition.y += moveAmount * Time.unscaledDeltaTime;
            }
            if (Input.mousePosition.y < edgeSize)
            {
                cameraFollowPosition.y -= moveAmount * Time.unscaledDeltaTime;
            }
        }
    }

    private void ZoomMovementFunction()
    {
        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            cameraZoom -= zoomChangeAmount * Time.unscaledDeltaTime;
        }

        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            cameraZoom += zoomChangeAmount * Time.unscaledDeltaTime;
        }

        if (Input.mouseScrollDelta.y > 0)
        {
            cameraZoom -= zoomChangeAmount * Time.unscaledDeltaTime;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            cameraZoom += zoomChangeAmount * Time.unscaledDeltaTime;
        }
        cameraZoom = Mathf.Clamp(cameraZoom, zoomBoundries.x, zoomBoundries.y);
    }

    private void MoveByDrag()
    {
        Vector3 newCameraPosition = Vector3.zero;

       
        if(Input.GetMouseButtonDown(0))
        {
            if (drag == false)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
                Collider2D target = Physics2D.OverlapPoint(mousePosition);
                if (target != null)
                    return;

                drag = true;
                origen = cameraFollow.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else if (Input.GetMouseButton(0))
        {
            difference = (cameraFollow.ScreenToWorldPoint(Input.mousePosition)) - cameraFollow.transform.position;
            if (drag == true)
            {
                float x = Mathf.Clamp((origen - difference).x, movementBoundries.x - movementBoundries.width, movementBoundries.x + movementBoundries.width);
                float y = Mathf.Clamp((origen - difference).y, movementBoundries.y - movementBoundries.height, movementBoundries.y + movementBoundries.height);

                newCameraPosition = new Vector3(x, y, (origen - difference).z);

                transform.position = newCameraPosition;
                cameraFollowPosition = transform.position;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            drag = false;
        }


    }
}
