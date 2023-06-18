using Photon.Pun;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float cameraSpeed = 30f;

    [SerializeField] private float border = 10f;

    [SerializeField] private float zoom = 5f;

    [SerializeField] private float dezoom = 20f;

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
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 newPosition = transform.position;
        newPosition.y -= scroll * 100 * cameraSpeed * Time.deltaTime;
        newPosition.y = Mathf.Clamp(newPosition.y, zoom, dezoom);
        newPosition.x = Mathf.Clamp(newPosition.x, startPosition.x - left, startPosition.x + right);
        newPosition.z = Mathf.Clamp(newPosition.z, startPosition.z - bottom, startPosition.z + top);
        transform.position = newPosition;
    }
}
