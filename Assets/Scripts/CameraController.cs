using UnityEngine;

public class CameraController : MonoBehaviour
{
    public int cameraSpeed = 30;
    public int border = 10;
    public int zoom = 5;
    public int dezoom = 20;
    public int top = 100;
    public int bottom = 10;
    public int left = 10;
    public int right = 10;

    private Vector3 cameraPosition;

    private void Start()
    {
        cameraPosition = transform.position;
    }

    void Update()
    {
        // Déplacement de la caméra vers l'avant
        if ((Input.GetKey(KeyCode.Z) || Input.mousePosition.y >= Screen.height - border) && cameraPosition.z <= cameraPosition.z + top)
        {
            transform.Translate(Vector3.forward * cameraSpeed * Time.deltaTime, Space.World);
        }

        // Déplacement de la caméra vers l'arrière
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= border)
        {
            transform.Translate(Vector3.back * cameraSpeed * Time.deltaTime, Space.World);
        }

        // Déplacement de la caméra vers la gauche
        if (Input.GetKey(KeyCode.Q) || Input.mousePosition.x <= border)
        {
            transform.Translate(Vector3.left * cameraSpeed * Time.deltaTime, Space.World);
        }

        // Déplacement de la caméra vers la droite
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - border)
        {
            transform.Translate(Vector3.right * cameraSpeed * Time.deltaTime, Space.World);
        }

        // Zoom et dézoom de la caméra
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 positionCamera = transform.position;
        positionCamera.y -= scroll * 100 * cameraSpeed * Time.deltaTime;
        positionCamera.y = Mathf.Clamp(positionCamera.y, zoom, dezoom);
        transform.position = positionCamera;
    }
}
