using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float speed = 1f;
    public float health = 10f;
    public float resistance = 1f;
    public int money = 0;
    public Image healthbar;

    private float startHealth;

    private void Start()
    {
        startHealth = health;
    }

    void Update()
    {
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        GameManager.gameManager.players[0].money += money;
        Destroy(gameObject);
        WaveSpawner.EnemiesAlives--;
    }

    public void Damage(float damage)
    {
        health -= damage * resistance;
        healthbar.fillAmount = health / startHealth;
    }
}
