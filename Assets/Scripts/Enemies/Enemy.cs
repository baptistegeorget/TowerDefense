using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private float health = 10f;

    [SerializeField]
    private float resistance = 1f;

    [SerializeField]
    private int money;

    [SerializeField]
    private Image healthbar;

    [Header("Chaman")]
    [SerializeField]
    private float chamanResistance = 1.15f;

    [Header("Lapinou")]
    [SerializeField]
    private float lapinouSpeed = 1.20f;

    [Header("Healer")]
    [SerializeField]
    public float healerPowerInterval = 1;

    [SerializeField]
    public float healerHeal = 2;

    [Header("Invocateur")]
    [SerializeField]
    private float invocateurPowerInterval = 12;

    [SerializeField]
    private float delayInvocation = 2;

    [SerializeField]
    public GameObject enemy1;

    [SerializeField]
    public GameObject enemy2;

    [SerializeField]
    public GameObject enemy3;

    private float startHealth;

    private float startSpeed;

    private float startResistance;

    private float freezeTime;

    private string[] enemiesTags = { "Boulepic", "Slime", "Centaure", "Chaman", "Chauve-souris", "Dragon", "Ghost", "Healer", "Invocateur", "Lapinou", "Ninja", "Rainette", "Serpent", "Tank", "Victime" };

    private bool powerApplied = false;

    private GameObject[] enemyPrefabs;

    private float healerPowerTimer = 0f;

    private float invocateurPowerTimer = 0f;

    private void Start()
    {
        startSpeed = speed;
        startHealth = health;
        startSpeed = speed;
        startResistance = resistance;
        enemyPrefabs = new GameObject[] { enemy1, enemy2, enemy3 };
        money = (int)Mathf.Round(health * 0.18f);
    }

    private void Update()
    {
        freezeTime -= Time.deltaTime;
        if (freezeTime > 0)
        {
            speed = 0;
        }
        else
        {
            speed = startSpeed;
        }
        if (health <= 0f)
        {
            Die();
        }
        Power();
    }

    private void Die()
    {
        GameManager.gameManager.GetPlayers()[0].SetMoney(GameManager.gameManager.GetPlayers()[0].GetMoney() + money);
        Destroy(gameObject);
        WaveSpawner.waveSpawner.SetEnemiesAlives(WaveSpawner.waveSpawner.GetEnemiesAlives() - 1);
    }

    public void Damage(float damage)
    {
        health -= damage * resistance;
        healthbar.fillAmount = health / startHealth;
    }

    public void Freeze(float time)
    {
        freezeTime = time;
    }

    private IEnumerator SpawnEnemiesWithDelay()
    {
        int enemyIndex = 0;
        while (enemyIndex < enemyPrefabs.Length)
        {
            GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], transform.position, transform.rotation);
            enemy.transform.SetParent(WaveSpawner.waveSpawner.gameObject.transform);
            enemy.GetComponent<EnemyMovement>().SetTarget(GetComponent<EnemyMovement>().GetTarget());
            enemy.GetComponent<EnemyMovement>().SetWaypoint(GetComponent<EnemyMovement>().GetWaypoint());
            WaveSpawner.waveSpawner.SetEnemiesAlives(WaveSpawner.waveSpawner.GetEnemiesAlives() + 1);
            enemyIndex++;
            yield return new WaitForSeconds(delayInvocation);
        }
    }

    private void Power()
    {
        GameObject[] enemies = { };
        foreach (string enemyTag in enemiesTags)
        {
            GameObject[] temp = GameObject.FindGameObjectsWithTag(enemyTag);
            enemies = enemies.Concat(temp).ToArray();
        }
        switch (gameObject.GetComponent<Enemy>().tag)
        {
            case "Victime":
                if (!powerApplied)
                {
                    if (health <= startHealth / 2)
                    {
                        speed *= 2f;
                        startSpeed *= 2f;
                        powerApplied = !powerApplied;
                    }
                }
                break;

            case "Chaman":
                foreach (GameObject enemy in enemies)
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distanceToEnemy < 2.5)
                    {
                        if (enemy.GetComponent<Enemy>().tag != "Chaman")
                        {
                            if (enemy.GetComponent<Enemy>().resistance == enemy.GetComponent<Enemy>().startResistance)
                            {
                                enemy.GetComponent<Enemy>().resistance *= chamanResistance;
                            }
                        }
                    }
                    else
                    {
                        enemy.GetComponent<Enemy>().resistance = enemy.GetComponent<Enemy>().startResistance;
                    }
                }
                break;

            case "Lapinou":
                foreach (GameObject enemy in enemies)
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distanceToEnemy < 2.5)
                    {
                        if (enemy.GetComponent<Enemy>().tag != "Lapinou")
                        {
                            if (enemy.GetComponent<Enemy>().speed == enemy.GetComponent<Enemy>().startSpeed)
                            {
                                enemy.GetComponent<Enemy>().speed = enemy.GetComponent<Enemy>().speed * lapinouSpeed;
                            }
                        }
                    }
                    else
                    {
                        enemy.GetComponent<Enemy>().speed = enemy.GetComponent<Enemy>().startSpeed;
                    }
                }
                break;

            case "Invocateur":
                invocateurPowerTimer += Time.deltaTime;
                if (invocateurPowerTimer >= invocateurPowerInterval)
                {
                    StartCoroutine(SpawnEnemiesWithDelay());
                    invocateurPowerTimer = 0;
                }
                break;

            case "Healer":
                healerPowerTimer += Time.deltaTime;
                if (healerPowerTimer >= healerPowerInterval)
                {
                    foreach (GameObject enemy in enemies)
                    {
                        float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
                        if (distanceToEnemy < 2.5)
                        {
                            if (enemy.GetComponent<Enemy>().tag != "Healer")
                            {
                                if (enemy.GetComponent<Enemy>().health < enemy.GetComponent<Enemy>().startHealth - healerHeal)
                                {
                                    enemy.GetComponent<Enemy>().health += healerHeal;
                                    enemy.GetComponent<Enemy>().Damage(0);
                                }
                                else
                                {
                                    enemy.GetComponent<Enemy>().health = enemy.GetComponent<Enemy>().startHealth;
                                }
                            }
                        }
                    }
                    healerPowerTimer = 0f;
                }
                break;
        }
    }

    public float GetSpeed()
    {
        return speed;
    }
}
