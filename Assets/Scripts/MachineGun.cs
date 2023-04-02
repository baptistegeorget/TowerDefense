using UnityEngine;
using System.Linq;

public class MachineGun: MonoBehaviour
{
    public int level;
    public float range;
    public float fireRate;
    public float damage;

    public Transform firePoint;
    public GameObject bulletPrefab;
    public Transform partToRotate;

    private string[] enemiesTags = { "Boulepic", "Slime", "Centaure", "Chaman", "Chauve-souris", "Dragon", "Ghost", "Healer", "Invocateur", "Lapinou", "Ninja", "Rainette", "Serpent", "Tank", "Victime"};
    private Transform target;
    private float fireCountDown;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void Update()
    {
        if (target == null)
        {
            return;
        }
        Rotation();
        if (fireCountDown <= 0f)
        {
            Shoot();
            fireCountDown = 1f / fireRate;
        }
        fireCountDown -= Time.deltaTime;
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = {};
        foreach (string enemyTag in enemiesTags)
        {
            GameObject[] temp = GameObject.FindGameObjectsWithTag(enemyTag);
            enemies = enemies.Concat(temp).ToArray();
        }
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
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

    private void Shoot()
    {
        Bullet bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation).GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.damage = damage;
            bullet.SetTarget(target);
        }
    }

    private void Rotation()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * 10).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
