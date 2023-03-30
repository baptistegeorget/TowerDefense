using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int speed = 1;
    public int health = 10;
    public int armor = 0;
    public int money = 0;

    void Update()
    {
        if (health <= 0 && armor <= 0)
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
