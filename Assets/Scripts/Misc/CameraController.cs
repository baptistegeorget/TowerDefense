using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float cameraSpeed = 30f;

    [SerializeField] private float top = 20f;

    [SerializeField] private float bottom = 20f;

    [SerializeField] private float left = 20f;

    [SerializeField] private float right = 20f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            transform.Translate(Vector3.forward * cameraSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * cameraSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(Vector3.left * cameraSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime, Space.World);
        }
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Clamp(newPosition.x, startPosition.x - left, startPosition.x + right);
        newPosition.z = Mathf.Clamp(newPosition.z, startPosition.z - bottom, startPosition.z + top);
        transform.position = newPosition;
    }
}
