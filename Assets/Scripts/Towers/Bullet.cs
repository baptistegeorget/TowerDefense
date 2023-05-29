using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed;

    private float damage;

    private GameObject effect;

    private Transform target;

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget()
    {
        Enemy enemy = target.GetComponent<Enemy>();
        enemy.Damage(damage);
        GameObject effect = Instantiate(this.effect, transform.position, this.effect.transform.rotation);
        effect.transform.SetParent(transform.parent);
        Destroy(effect, 2f);
        Destroy(gameObject);
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    public void SetEffect(GameObject effect)
    {
        this.effect = effect;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}
