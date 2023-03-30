using UnityEngine;

public class Mitrailleuse : MonoBehaviour
{
    // Les attributs de la tourelle
    public int level;
    public int range;
    public int fireRate;
    public float fireCountDown;

    // Parties de la tourelle
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Transform partToRotate;

    // Cibles
    private Transform target;
    public string enemyTag = "Boulepic";

    private void Awake()
    {
        switch (level)
        {
            case 1:
                range = 10;
                fireRate = 1;
                break;
            case 2:
                range = 10;
                fireRate = 1;
                break;
            case 3:
                range = 10;
                fireRate = 1;
                break;
            default:
                break;
        }
    }

    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] ennemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in ennemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void Update()
    {
        
        if (target == null)
        {
            return;
        }
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * 10).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        if (fireCountDown <= 0f)
        {
            Shoot();
            fireCountDown = 1 / fireRate;
        }
        fireCountDown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject bulletGO = (GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
