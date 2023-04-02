using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private static Vector3 positionOffset = new Vector3(0, 1.2f, 0);

    public static void BuildTurret(GameObject prefab)
    {
        Instantiate(prefab, RadialMenu.node.transform.position + positionOffset, Quaternion.Euler(new Vector3(0, 0, 0)));
        RadialMenu.node.hasTurret = true;
    }
}
