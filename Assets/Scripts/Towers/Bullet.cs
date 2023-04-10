using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float damage;
    public GameObject effect;
    public Transform target;

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
        GameObject effect = Instantiate(this.effect, transform.position, transform.rotation);
        effect.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        Destroy(effect, 2f);
        Destroy(gameObject);
    }
}
