using UnityEngine;
using System.Linq;

public class MortarBullet : MonoBehaviour
{
    private float speed;

    private float damage;

    private float range;

    private GameObject effect;

    private Transform target;

    private string[] enemiesTags;
    
    private Vector3 startPosition;

    private float stepScale;

    private float progress;

    private float arcHeight = 0.5f;

    private Transform[] targets = {};

    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = target.position;
        startPosition = transform.position;
        float distance = Vector3.Distance(startPosition, targetPosition);
        stepScale = speed / distance;
        arcHeight = arcHeight * distance;
    }

    void Update()
    {
        progress = Mathf.Min(progress + Time.deltaTime * stepScale, 1.0f);
        float parable = 1.0f - 4.0f * (progress - 0.5f) * (progress - 0.5f);
        Vector3 nextPos = Vector3.Lerp(startPosition, targetPosition, progress);
        nextPos.y += parable * arcHeight;
        transform.LookAt(nextPos, transform.forward);
        transform.position = nextPos;
        if (progress == 1.0f)
        {
            HitTarget();
        }
    }

    void HitTarget()
    {
        UpdateTarget();
        foreach (Transform target in targets)
        {
            Enemy enemy = target.GetComponent<Enemy>();
            enemy.Damage(damage);
        }
        GameObject effect = Instantiate(this.effect, transform.position, this.effect.transform.rotation);
        effect.transform.SetParent(transform.parent);
        Destroy(effect, 2f);
        Destroy(gameObject);
    }

    private void UpdateTarget()
    {
        GameObject[] enemies = {};
        foreach (string enemyTag in enemiesTags)
        {
            GameObject[] temp = GameObject.FindGameObjectsWithTag(enemyTag);
            enemies = enemies.Concat(temp).ToArray();
        }
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy <= range && targets.Contains(enemy.transform) == false)
            {
                Transform[] temp = { enemy.transform };
                targets = targets.Concat(temp).ToArray();
            }
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
     
    public void SetRange(float range)
    {
        this.range = range;
    }

    public void SetEffect(GameObject effect)
    {
        this.effect = effect;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetEnemiesTags(string[] enemiesTags)
    {
        this.enemiesTags = enemiesTags;
    }
}
