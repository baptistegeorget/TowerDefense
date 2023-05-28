using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("stat de chaque ennemi")]
    public float speed = 1f;
    public float health = 10f;
    public float resistance = 1f;
    private int money;
    public Image healthbar;

    [Header("stockage des stats de base de chaque ennemi")]
    private float startHealth;
    private float startSpeed;
    private float startResistance;
    private float freezeTime;

    [Header("liste des tags des ennemis")]
    public string[] enemiesTags = { "Boulepic", "Slime", "Centaure", "Chaman", "Chauve-souris", "Dragon", "Ghost", "Healer", "Invocateur", "Lapinou", "Ninja", "Rainette", "Serpent", "Tank", "Victime" };

    [Header("condition du pouvoir de Victime")]
    private bool powerApplied = false;

    [Header("chaman's settings")]
    public float chamanResistance = 1.15f;

    [Header("lapinou's settings")]
    public float lapinouSpeed = 1.20f;

    [Header("healer's settings")]
    public int healerPowerInterval = 1;
    private float healerPowerTimer = 0f;
    public int healerHeal = 2;

    [Header("invocateur's settings")]
    private int invocateurPowerInterval = 12;
    private float invocateurPowerTimer = 4f;
    private float delayInvocation = 2;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject[] enemyPrefabs;

    private void Start()
    {
        startHealth = health;
        startSpeed = speed;
        startResistance = resistance;
        enemyPrefabs = new GameObject[] { enemy1, enemy2, enemy3 };
        money = (int)Mathf.Round(health * 0.18f);
    }

    private void Update() // FAIRE BELEK CA CASSE LA CONDITION DU MONSTRE VICTIME
    {
       // freezeTime -= Time.deltaTime;
       // if (freezeTime > 0)
       // {
       //     speed = 0;
       // }
       // else
       // {
       //     speed = startSpeed;
       // }
        if (health <= 0f)
        {
            Die();
        }
        Power();
    }

    private void Die()
    {
        GameManager.gameManager.players[0].money += money;
        Destroy(gameObject);
        WaveSpawner.enemiesAlives--;
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

    private IEnumerator SpawnEnemiesWithDelay(int enemyIndex)
    {
        while (enemyIndex < enemyPrefabs.Length)
        {
            GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], transform.position, transform.rotation);
            enemy.GetComponent<EnemyMovement>().SetTarget(GetComponent<EnemyMovement>().GetTarget());
            enemy.GetComponent<EnemyMovement>().SetWaypoint(GetComponent<EnemyMovement>().GetWaypoint());

            WaveSpawner.enemiesAlives++;

            yield return new WaitForSeconds(delayInvocation);

            enemyIndex++;
        }
    }

    private void Power()
    {
        // Récupère tout les ennemis sur le terrain
        GameObject[] enemies = { };
        foreach (string enemyTag in enemiesTags)
        {
            GameObject[] temp = GameObject.FindGameObjectsWithTag(enemyTag);
            enemies = enemies.Concat(temp).ToArray();
        }
        //Récupère le tag de l'ennemi courant
        //switch -> différents pouvoirs
        switch (gameObject.GetComponent<Enemy>().tag)
        {
            case "Victime":
                // double sa vitesse quand il arrive à la moitié de sa vie
                if (!powerApplied)
                {
                    if (health <= startHealth / 2)
                    {
                        speed *= 2f;
                        powerApplied = !powerApplied;
                    }
                }
                break;
            case "Chaman":
                // augmente la résistance de x% aux ennemis se trouvant autour de lui
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
                    //retour à la résistance de base si l'ennemi sort du cercle d'action
                    {
                        enemy.GetComponent<Enemy>().resistance = enemy.GetComponent<Enemy>().startResistance;
                    }
                }
                break;
            case "Lapinou":
                // augmente la vitesse de x% aux ennemis se trouvant autour de lui
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
                    //retour à la vitesse de base si l'ennemi sort du cercle d'action
                    {
                        enemy.GetComponent<Enemy>().speed = enemy.GetComponent<Enemy>().startSpeed;
                    }
                }
                break;
            case "Invocateur":
                // invoque 3 monstres à la position où il se trouve
                invocateurPowerTimer += Time.deltaTime;
                if (invocateurPowerTimer >= invocateurPowerInterval)
                {
                    invocateurPowerTimer += Time.deltaTime;
                    if (invocateurPowerTimer >= invocateurPowerInterval)
                    {
                        int enemyIndex = 0;
                        StartCoroutine(SpawnEnemiesWithDelay(enemyIndex));
                        invocateurPowerTimer = 0;
                    }
                    invocateurPowerTimer = 0;
                }
                break;
            case "Healer":
                // régénère de x% aux ennemis se trouvant autour de lui
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
    
}

