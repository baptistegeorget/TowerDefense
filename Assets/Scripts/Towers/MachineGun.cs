using UnityEngine;
using System.Linq;

public class MachineGun: MonoBehaviour
{
    [Header("Tower")]
    [SerializeField]
    private float range;

    [SerializeField]
    private float fireRate;

    [SerializeField]
    private float damage;

    [SerializeField]
    private Transform partToRotate;

    [SerializeField]
    private Transform[] firePoints;

    [SerializeField]
    private string[] enemiesTags;

    [Header("Bullet")]
    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private GameObject bulletEffect;

    private Transform target;

    private float fireCountDown;

    private float startDamage;

    private float startFireRate;

    private void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        startDamage = damage;
        startFireRate = fireRate;
    }

    private void Update()
    {
        if (target == null)
        {
            return;
        }
        if (partToRotate != null)
        {
            Rotation();
        }
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
        for (int i = 0; i < firePoints.Length; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab, firePoints[i].position, firePoints[i].rotation).GetComponent<Bullet>();
            bullet.transform.SetParent(transform);
            bullet.SetDamage(damage);
            bullet.SetEffect(bulletEffect);
            bullet.SetSpeed(bulletSpeed);
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

    public float GetStartFireRate()
    {
        return startFireRate;
    }

    public float GetStartDamage()
    {
        return startDamage;
    }

    public float GetFireRate()
    {
        return fireRate;
    }

    public float GetDamage()
    {
        return damage;
    }

    public void SetFireRate(float fireRate)
    {
        this.fireRate = fireRate;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
}
