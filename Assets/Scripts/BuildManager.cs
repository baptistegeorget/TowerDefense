using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject turretToBuild;


    #region Singleton
    public static BuildManager instance;

    public bool SetUp = false;

    public void SetTurretToBuild(GameObject pTurretToBuild)
    {
        turretToBuild = pTurretToBuild;
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("erreur, il a deja un build manager dans la scène");
            return;
        }
        instance = this;
    }

    #endregion


}
