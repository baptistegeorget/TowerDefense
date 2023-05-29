using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Tesla : MonoBehaviour
{
    [Header("Tower")]
    [SerializeField]
    private float range;

    [SerializeField]
    private float damage;

    [SerializeField]
    private float freezeTime;

    [SerializeField]
    private float fireRate;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private int maxEnemies;

    [SerializeField]
    private GameObject effect;

    [SerializeField]
    private string[] enemiesTags;

    private LineRenderer lineRenderer;

    private Transform[] targets = {};

    private List<LineRenderer> lineRenderers = new List<LineRenderer>();

    private float fireCountDown;

    private float startDamage;

    private void Start()
    {
        fireCountDown = 1 / fireRate;
        lineRenderer = transform.GetComponent<LineRenderer>();
        startDamage = damage;
    }

    private void Update()
    {
        UpdateTarget();
        if (targets.Length > 0)
        {
            fireCountDown -= Time.deltaTime;
            for (int i = 0; i < targets.Length; i++)
            {
                if (lineRenderers[i].enabled == false)
                {
                    lineRenderers[i].enabled = true;
                }
                lineRenderers[i].GetComponent<LineRenderer>().SetPosition(0, firePoint.position);
                lineRenderers[i].GetComponent<LineRenderer>().SetPosition(1, targets[i].position);
            }
            if (fireCountDown <= 0)
            {
                Shoot();
                fireCountDown = 1 / fireRate;
            }
        }
        else
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (lineRenderers[i].enabled)
                {
                    lineRenderers[i].enabled = false;
                }
            }
            return;
        }
    }

    private void UpdateTarget()
    {
        foreach (Transform target in targets)
        {
            if (target == null)
            {
                targets = targets.Where(e => e != target).ToArray();
                Destroy(lineRenderers[0].gameObject);
                lineRenderers.RemoveAt(0);
            }
            else
            {
                float distanceToEnemy = Vector3.Distance(transform.position, target.position);
                if (distanceToEnemy > range)
                {
                    targets = targets.Where(e => e != target).ToArray();
                    Destroy(lineRenderers[0].gameObject);
                    lineRenderers.RemoveAt(0);
                }
            }
        }
        GameObject[] enemies = {};
        foreach (string enemyTag in enemiesTags)
        {
            GameObject[] temp = GameObject.FindGameObjectsWithTag(enemyTag);
            enemies = enemies.Concat(temp).ToArray();
        }
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy <= range && targets.Contains(enemy.transform) == false && targets.Length < maxEnemies)
            {
                Transform[] temp = { enemy.transform };
                targets = targets.Concat(temp).ToArray();
                LineRenderer lr = new GameObject().AddComponent<LineRenderer>();
                lr.gameObject.transform.SetParent(transform, false);
                lr.gameObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
                lr.material = lineRenderer.material;
                lr.colorGradient = lineRenderer.colorGradient;
                lr.widthCurve = lineRenderer.widthCurve;
                lineRenderers.Add(lr);
            }
        }
    }

    private void Shoot()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].GetComponent<Enemy>().Damage(damage);
            targets[i].GetComponent<Enemy>().Freeze(freezeTime);
            GameObject effect = Instantiate(this.effect, targets[i].position, this.effect.transform.rotation);
            effect.transform.SetParent(transform);
            Destroy(effect, 2f);
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
