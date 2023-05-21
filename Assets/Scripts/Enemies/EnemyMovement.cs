using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int waypointIndex = 0;
    private Enemy enemy;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.waypoints[waypointIndex];
    }

    private void Update()
    {
        Move();
        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            GetNextWaypoint();
        }
    }

    private void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.waypoints.Length - 1)
        {
            WaveSpawner.enemiesAlives--;
            if (GameManager.gameManager.players[0].pv > 0)
            {
                GameManager.gameManager.players[0].pv--;
            }
            Destroy(gameObject);
            return;
        }
        waypointIndex++;
        target = Waypoints.waypoints[waypointIndex];
    }

    public void SetWaypoint(int index)
    {
        this.waypointIndex = index;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public int GetWaypoint()
    {
        return this.waypointIndex;
    }

    public Transform GetTarget()
    {
        return this.target;
    }

    private void Move()
    {
        Vector3 direction = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        transform.Translate(direction.normalized * enemy.speed * Time.deltaTime, Space.World);
    }
}
