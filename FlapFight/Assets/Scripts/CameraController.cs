using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public Transform player1;
    public Transform player2;

    public Vector3 offset;

    private Vector3 velocity;

    public float minZoom = 4f;
    public float maxZoom = 10f;
    public float zoomLimiter = 30f; 

    public float smoothSpeed = 0.125f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called after Update() Method
    void LateUpdate()
    {
        if(player1 == null && player2 == null)
        {
            return;
        }

        Move();
        Zoom();
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothSpeed);
    }

    void Zoom()
    {
        var bounds = new Bounds(player1.position, Vector3.zero);
        bounds.Encapsulate(player2.position);
        float distance = bounds.size.x;

        cam.orthographicSize = Mathf.Lerp(minZoom, maxZoom, distance / zoomLimiter);

    }

    Vector3 GetCenterPoint()
    {
        if(player1 == null)
        {
            return player2.position;
        }
        else if(player2 == null)
        {
            return player1.position;
        }

        var bounds = new Bounds(player1.position, Vector3.zero);
        bounds.Encapsulate(player2.position);

        return bounds.center;
    }
}
