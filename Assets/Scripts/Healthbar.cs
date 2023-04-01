using UnityEngine;

public class Healthbar : MonoBehaviour
{
    public GameObject cameraPrefab;

    private void Awake()
    {
        cameraPrefab = GameObject.FindGameObjectsWithTag("MainCamera")[0];
    }

    private void Update()
    {
        Vector3 direction = cameraPrefab.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f).eulerAngles;
        transform.rotation = Quaternion.Euler(rotation.x, rotation.y, rotation.z);
    }
}
