using UnityEngine;

public class Slime : MonoBehaviour
{
    // Les attributs du monstre
    public int speed = 1;
    public int health = 10;
    private int money;

    // Le prochain waypoint
    private Transform target;
    private int waypointIndex = 0;

    void Awake()
    {
        money = (int) Mathf.Round(health * 0.1f);
    }

    void Start()
    {
        target = Waypoints.waypoints[0];
    }

    private void Update()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target.position) <= 0.001f)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.waypoints.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        waypointIndex++;
        target = Waypoints.waypoints[waypointIndex];
    }
}
