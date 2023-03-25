using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // Start is called before the first frame update

    #region Singleton
    public static BuildManager instance;

    public GameObject getTurretToBuild()
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

    private GameObject turretToBuild;

    public GameObject StandardTurretPrefab;

    private void Start()
    {
        turretToBuild = StandardTurretPrefab;
    }

    // Update is called once per frame
    void Update()
    {

    }
}