using UnityEngine;

public class Player : MonoBehaviour
{
    public static int pv;

    public static void Damage(int damage)
    {
        pv -= damage;
    }
}
