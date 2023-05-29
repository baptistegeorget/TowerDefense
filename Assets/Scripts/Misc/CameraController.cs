using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed = 30f;
    public float border = 10f;
    public float zoom = 5f;
    public float dezoom = 20f;
    public float top = 20f;
    public float bottom = 20f;
    public float left = 20f;
    public float right = 20f;
    
    public static bool cameraLock;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        cameraLock = false;
    }
    
    void Update()
    {
        if (cameraLock)
        {
            return;
        }
        if (Input.GetKey(KeyCode.Z) || Input.mousePosition.y >= Screen.height - border)
        {
            transform.Translate(Vector3.forward * cameraSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= border)
        {
            transform.Translate(Vector3.back * cameraSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.Q) || Input.mousePosition.x <= border)
        {
            transform.Translate(Vector3.left * cameraSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - border)
        {
            transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime, Space.World);
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 newPosition = transform.position;
        newPosition.y -= scroll * 100 * cameraSpeed * Time.deltaTime;
        newPosition.y = Mathf.Clamp(newPosition.y, zoom, dezoom);
        newPosition.x = Mathf.Clamp(newPosition.x, startPosition.x - left, startPosition.x + right);
        newPosition.z = Mathf.Clamp(newPosition.z, startPosition.z - bottom, startPosition.z + top);
        transform.position = newPosition;
    }
}
