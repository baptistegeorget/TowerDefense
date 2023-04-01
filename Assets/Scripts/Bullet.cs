using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 7f;
    public float damage;

    public GameObject effect;

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

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    void HitTarget()
    {
        Enemy enemy = target.GetComponent<Enemy>();
        enemy.health -= damage * enemy.resistance;
        GameObject effect = Instantiate(this.effect, transform.position, transform.rotation);
        Destroy(effect, 2f);
        Destroy(gameObject);
    }
}
