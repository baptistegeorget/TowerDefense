using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;
    public float health = 10f;
    public float resistance = 1f;
    public float money = 0f;

    void Update()
    {
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        WaveSpawner.EnemiesAlives--;
    }
}
