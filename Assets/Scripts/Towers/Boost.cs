using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Boost : MonoBehaviour
{
    [Header("Tower")]
    [SerializeField]
    private float range;

    [SerializeField]
    private GameObject effect;

    [Header("Machine Gun")]
    [SerializeField]
    private float machineGunDamage;

    [SerializeField]
    private float machineGunFireRate;

    [Header("Arcana")]
    [SerializeField]
    private float arcanaDamage;

    [Header("Tesla")]
    [SerializeField]
    private float teslaDamage;

    [Header("Crossbow")]
    [SerializeField]
    private float crossbowDamage;

    [SerializeField]
    private float crossbowFireRate;

    [Header("Mortar")]
    [SerializeField]
    private float mortarDamage;

    [SerializeField]
    private float mortarFireRate;

    private string[] towerTags = {"MachineGun1", "MachineGun2", "MachineGun3", "Arcana1", "Arcana2", "Arcana3", "Tesla1", "Tesla2", "Tesla3", "Crossbow1", "Crossbow2", "Crossbow3", "Mortar1", "Mortar2", "Mortar3"};

    private List<GameObject> targets = new List<GameObject>();

    private List<GameObject> effects = new List<GameObject>();

    private void Update()
    {
        UpdateTarget();
    }

    private void UpdateTarget()
    {
        foreach (GameObject target in targets.ToList())
        {
            if (target == null)
            {
                targets.Remove(target);
            }
        }
        foreach (GameObject effect in effects.ToList())
        {
            bool remove = true;
            foreach (GameObject target in targets)
            {
                if (target.transform.position + new Vector3(0f, 0.02f, 0f) == effect.transform.position)
                {
                    remove = false;
                }
            }
            if (remove)
            {
                effects.Remove(effect);
                Destroy(effect);
            }
        }
        GameObject[] towers = {};
        foreach (string towerTag in towerTags)
        {
            GameObject[] temp = GameObject.FindGameObjectsWithTag(towerTag);
            towers = towers.Concat(temp).ToArray();
        }
        foreach (GameObject tower in towers)
        {
            float distanceToTower = Vector3.Distance(transform.position, tower.transform.position);
            if (distanceToTower <= range && !targets.Find(delegate (GameObject gameObject) { return gameObject == tower; }))
            {
                targets.Add(tower);
                BoostTower(tower);
                GameObject effectTemp = Instantiate(effect, tower.transform.position + new Vector3(0f, 0.02f, 0f), effect.transform.rotation);
                effects.Add(effectTemp);
                effectTemp.transform.SetParent(transform);
            }
        }
    }

    private void BoostTower(GameObject tower)
    {
        if (tower.tag == towerTags[0] || tower.tag == towerTags[1] || tower.tag == towerTags[2])
        {
            MachineGun machineGun = tower.GetComponent<MachineGun>();
            machineGun.SetDamage(machineGun.GetDamage() + (machineGun.GetStartDamage() * machineGunDamage - machineGun.GetStartDamage()));
            machineGun.SetFireRate(machineGun.GetFireRate() + (machineGun.GetStartFireRate() * machineGunFireRate - machineGun.GetStartFireRate()));
        }
        else if(tower.tag == towerTags[3] || tower.tag == towerTags[4] || tower.tag == towerTags[5])
        {
            Arcana arcana = tower.GetComponent<Arcana>();
            arcana.SetDamage(arcana.GetDamage() + (arcana.GetStartDamage() * arcanaDamage - arcana.GetStartDamage()));
        }
        else if (tower.tag == towerTags[6] || tower.tag == towerTags[7] || tower.tag == towerTags[8])
        {
            Tesla tesla = tower.GetComponent<Tesla>();
            tesla.SetDamage(tesla.GetDamage() + (tesla.GetStartDamage() * teslaDamage - tesla.GetStartDamage()));
        }
        else if (tower.tag == towerTags[9] || tower.tag == towerTags[10] || tower.tag == towerTags[11])
        {
            MachineGun crossbow = tower.GetComponent<MachineGun>();
            crossbow.SetDamage(crossbow.GetDamage() + (crossbow.GetStartDamage() * crossbowDamage - crossbow.GetStartDamage()));
            crossbow.SetFireRate(crossbow.GetFireRate() + (crossbow.GetStartFireRate() * crossbowFireRate - crossbow.GetStartFireRate()));
        }
        else if (tower.tag == towerTags[12] || tower.tag == towerTags[13] || tower.tag == towerTags[14])
        {
            Mortar mortar = tower.GetComponent<Mortar>();
            mortar.SetFireRate(mortar.GetFireRate() + (mortar.GetStartFireRate() * mortarFireRate + mortar.GetStartFireRate()));
            mortar.SetDamage(mortar.GetDamage() + (mortar.GetStartDamage() * mortarDamage - mortar.GetStartDamage()));
        }
    }

    private void StopBoost(GameObject tower)
    {
        if (tower != null)
        {
            if (tower.tag == towerTags[0] || tower.tag == towerTags[1] || tower.tag == towerTags[2])
            {
                MachineGun machineGun = tower.GetComponent<MachineGun>();
                machineGun.SetDamage(machineGun.GetDamage() - (machineGun.GetStartDamage() * machineGunDamage - machineGun.GetStartDamage()));
                machineGun.SetFireRate(machineGun.GetFireRate() - (machineGun.GetStartFireRate() * machineGunFireRate - machineGun.GetStartFireRate()));
            }
            else if (tower.tag == towerTags[3] || tower.tag == towerTags[4] || tower.tag == towerTags[5])
            {
                Arcana arcana = tower.GetComponent<Arcana>();
                arcana.SetDamage(arcana.GetDamage() - (arcana.GetStartDamage() * arcanaDamage - arcana.GetStartDamage()));
            }
            else if (tower.tag == towerTags[6] || tower.tag == towerTags[7] || tower.tag == towerTags[8])
            {
                Tesla tesla = tower.GetComponent<Tesla>();
                tesla.SetDamage(tesla.GetDamage() - (tesla.GetStartDamage() * teslaDamage - tesla.GetStartDamage()));
            }
            else if (tower.tag == towerTags[9] || tower.tag == towerTags[10] || tower.tag == towerTags[11])
            {
                MachineGun crossbow = tower.GetComponent<MachineGun>();
                crossbow.SetDamage(crossbow.GetDamage() - (crossbow.GetStartDamage() * crossbowDamage - crossbow.GetStartDamage()));
                crossbow.SetFireRate(crossbow.GetFireRate() - (crossbow.GetStartFireRate() * crossbowFireRate - crossbow.GetStartFireRate()));
            }
            else if (tower.tag == towerTags[12] || tower.tag == towerTags[13] || tower.tag == towerTags[14])
            {
                Mortar mortar = tower.GetComponent<Mortar>();
                mortar.SetFireRate(mortar.GetFireRate() - (mortar.GetStartFireRate() * mortarFireRate + mortar.GetStartFireRate()));
                mortar.SetDamage(mortar.GetDamage() - (mortar.GetStartDamage() * mortarDamage - mortar.GetStartDamage()));
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void OnDestroy()
    {
        foreach (GameObject target in targets)
        {
            StopBoost(target);
        }
    }
}