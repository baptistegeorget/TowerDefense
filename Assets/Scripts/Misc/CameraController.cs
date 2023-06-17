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
        if (PhotonNetwork.IsConnectedAndReady && GetComponentInParent<PhotonView>().IsMine)
        {
            transform.GetComponent<Camera>().enabled = true;
        }
    }

    void Update()
    {
        if ((PhotonNetwork.IsConnectedAndReady && GetComponentInParent<PhotonView>().IsMine) || !PhotonNetwork.IsConnectedAndReady)
        {
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
}
