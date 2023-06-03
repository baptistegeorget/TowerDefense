using UnityEngine;
using System.Linq;

public class Mortar : MonoBehaviour
{
    [Header("Tower")]
    [SerializeField]
    private float range;

    [SerializeField]
    private float fireRate;

    [SerializeField]
    private float damage;

    [SerializeField]
    private Transform[] firePoints;

    [SerializeField]
    private float damageMultiplicator;

    [SerializeField]
    private string[] enemiesTags;

    [Header("Bullet")]
    [SerializeField]
    private GameObject bulletPrefab;
    
    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float bulletRange;

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
            MortarBullet bullet = Instantiate(bulletPrefab, firePoints[i].position, firePoints[i].rotation).GetComponent<MortarBullet>();
            bullet.transform.SetParent(transform);
            bullet.SetDamage(damage);
            bullet.SetDamageMultiplicator(damageMultiplicator);
            bullet.SetEffect(bulletEffect);
            bullet.SetSpeed(bulletSpeed);
            bullet.SetRange(bulletRange);
            bullet.SetTarget(target);
            bullet.SetEnemiesTags(enemiesTags);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public float GetFireRate()
    {
        return fireRate;
    }

    public float GetDamage()
    {
        return damage;
    }

    public float GetStartFireRate()
    {
        return startFireRate;
    }

    public float GetStartDamage()
    {
        return startDamage;
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
