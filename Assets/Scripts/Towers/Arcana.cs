using UnityEngine;
using System.Linq;

public class Arcana : MonoBehaviour
{
    [Header("Tower")]
    [SerializeField]
    private float range;

    [SerializeField]
    private float damage;

    [SerializeField]
    private Transform firePoints;

    [SerializeField]
    private string[] enemiesTags;

    private LineRenderer lineRenderer;

    private Transform target;

    private float fireCountDown = 1;

    private float timeTarget;

    private float startDamage;

    private void Start()
    {
        UpdateTarget();
        lineRenderer = transform.GetComponent<LineRenderer>();
        startDamage = damage;
    }

    private void Update()
    {
        if (target)
        {
            if (Vector3.Distance(transform.position, target.position) >= range)
            {
                target = null;
                return;
            }
            if (lineRenderer.enabled == false)
            {
                lineRenderer.enabled = true;
            }
            Shoot();
            timeTarget += Time.deltaTime;
        }
        else
        {
            timeTarget = 0;
            if (lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
            }
            UpdateTarget();
            return;
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
        lineRenderer.SetPosition(0, firePoints.position);
        lineRenderer.SetPosition(1, target.position);
        if (fireCountDown <= 0)
        {
            target.GetComponent<Enemy>().Damage(damage * timeTarget);
            fireCountDown = 1;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public float GetStartDamage()
    {
        return startDamage;
    }

    public float GetDamage()
    {
        return damage;
    }
}
