using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private static Vector3 positionOffset = new Vector3(0, 1.2f, 0);

    public static void BuildTurret(GameObject prefab,  string cout)
    {
        //convertion du label du menuEntry en int
        int price;
        int.TryParse(cout, out price);
        Debug.Log("prix de la tourelle"+ price);
        //end convertion

        if (Player.money < price)
        {
            Debug.Log("Pas assez d'argent pour cela");
            return;
        }
        // payment de la tourelle 
        Player.money -= price;
        Instantiate(prefab, RadialMenu.node.transform.position + positionOffset, Quaternion.Euler(new Vector3(0, 0, 0)));
        RadialMenu.node.hasTurret = true;
        Debug.Log("Il vous reste" + Player.money);
    }
}
